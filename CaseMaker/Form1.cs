using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.IO;
using System.Threading;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using System.Net;

using Ionic.Zip;

namespace CaseMaker
{
    public partial class MainForm : Form
    {
        bool validData;
        Thread getImageThread;
        UploadMIRCDialog uploadDialog = new UploadMIRCDialog();

        string lastFilename = String.Empty;
        List<CaseImage> caseImages = new List<CaseImage>();
        int currentImage = 0;
        Image nextImage;
        int lastX = 0;
        int lastY = 0;

        public MainForm()
        {
            InitializeComponent();
            updateImageLabels();
            saveXMLDialog.InitialDirectory = Application.StartupPath;
            openXMLDialog.InitialDirectory = Application.StartupPath;
            uploadDialog.urlMIRC = @"http://127.0.0.1:8080/storage/submit/doc";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseWheel);
        }

        void clearBoxes(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = "";
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        clearBoxes(ctrl);
                    }
                }
            }
        }


        private void newCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearBoxes(this);
            caseImages.Clear();
            pb.Image = null;
            updateImageLabels();
        }

        private void imagePanel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0) btnLeft_Click(sender, e);
            if (e.Delta < 0) btnRight_Click(sender, e);
        }

        private void imagePanel_DragDrop(object sender, DragEventArgs e)
        {

            if (validData)
            {
                while (getImageThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(0);
                }
                thumbnail.Visible = false;


                if (pb.Image != nextImage)
                {
                    pb.Image = nextImage;
                    AdjustView();
                    caseImages.Add(new CaseImage(nextImage));
                    currentImage = caseImages.Count;
                    updateImageLabels();

                    MemoryStream ms = e.Data.GetData("Synapse.FujiOffset") as MemoryStream;
                    if (ms != null)
                    {
                        StreamReader sr = new StreamReader(ms, Encoding.Unicode);
                        string mrn = sr.ReadToEnd();
                        mrn = mrn.Substring(0, mrn.Length - 1);

                        ms = e.Data.GetData("UniformResourceLocator") as MemoryStream;
                        if (ms != null)
                        {
                            sr = new StreamReader(ms);
                            string url = sr.ReadToEnd();

                            if (url.StartsWith("https://external.synapse.uscuh.com/synapse.asp?path=//commandclassname=Synapse%26datasource=https%253A%252F%252Fexternal.synapse.uscuh.com")) textLoc.Text = "USC Norris";
                            if (url.StartsWith("https://external.synapse.uscuh.com/synapse.asp?path=//commandclassname=Synapse%26datasource=https%253A%252F%252Ffujipacs.hsc.usc.edu")) textLoc.Text = "HCC2";
                            if (url.StartsWith("http://lacsynapse")) textLoc.Text = "LACUSC";
                        }
                        getCacheDemographics(mrn);
                    }
                }
            }
        }

        void updateImageLabels()
        {
            countLabel.Text = currentImage + " / " + caseImages.Count;
            btnDelete.Enabled = (currentImage > 0);
            btnLeft.Enabled = (currentImage > 1);
            btnRight.Enabled = (currentImage < caseImages.Count);
            if (pb.Image == null)
            {
                textCaption.Text = "";
                textCaption.Enabled = false;
            }
            else
            {
                textCaption.Text = caseImages[currentImage - 1].caption;
                textCaption.Enabled = true;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentImage > 1)
            {
                currentImage--;
                pb.Image = caseImages[currentImage - 1].image;
                AdjustView();
                updateImageLabels();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentImage < caseImages.Count)
            {
                currentImage++;
                pb.Image = caseImages[currentImage - 1].image;
                AdjustView();
                updateImageLabels();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            caseImages.RemoveAt(currentImage - 1);
            if (currentImage > caseImages.Count)
            {
                currentImage--;
            }
            if (caseImages.Count > 0)
            {
                pb.Image = caseImages[currentImage - 1].image;
                AdjustView();
            }
            else
            {
                pb.Image = null;
            }
            updateImageLabels();
        }

        private void imagePanel_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                if (lastFilename != filename)
                {
                    thumbnail.Image = null;
                    thumbnail.Visible = false;
                    lastFilename = filename;
                    getImageThread = new Thread(new ThreadStart(LoadImage));
                    getImageThread.Start();
                }
                else
                {
                    thumbnail.Visible = true;
                }
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = e.Data.GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp"))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private void imagePanel_DragLeave(object sender, EventArgs e)
        {
            thumbnail.Visible = false;
        }

        public delegate void AssignImageDlgt();

        Image LoadUnlockImage(string asFile)
        {
            // This is necessary because Image constructor ordinarily
            // keeps file locked after opening
            Stream BitmapStream = File.Open(asFile, FileMode.Open);
            Image img = Image.FromStream(BitmapStream);
            BitmapStream.Close();

            return new Bitmap(img);
        }

        void LoadImage()
        {
            nextImage = LoadUnlockImage(lastFilename);
            this.Invoke(new AssignImageDlgt(AssignImage));
        }

        void AssignImage()
        {
            thumbnail.Width = 100;
            thumbnail.Height = thumbnail.Width * nextImage.Height / nextImage.Width;
            SetThumbnailLocation(imagePanel.PointToClient(new Point(lastX, lastY)));
            thumbnail.Image = nextImage;
        }

        void SetThumbnailLocation(Point p)
        {
            if (thumbnail.Image == null)
            {
                thumbnail.Visible = false;
            }
            else
            {
                p.X -= thumbnail.Width / 2;
                p.Y -= thumbnail.Height / 2;
                thumbnail.Location = p;
                thumbnail.Visible = true;
            }
        }

        void AdjustView()
        {
            if (pb.Image != null)
            {

                float fw = imagePanel.Width;
                float fh = imagePanel.Height;
                float iw = pb.Image.Width;
                float ih = pb.Image.Height;

                // iw/fw > ih/fh, then iw/fw controls ih

                float rw = fw / iw;			// ratio of width
                float rh = fh / ih;			// ratio of height

                if (rw < rh)
                {
                    pb.Width = (int)fw;
                    pb.Height = (int)(ih * rw);
                    pb.Left = 0;
                    pb.Top = (int)((fh - pb.Height) / 2);
                }
                else
                {
                    pb.Width = (int)(iw * rh);
                    pb.Height = (int)fh;
                    pb.Left = (int)((fw - pb.Width) / 2);
                    pb.Top = 0;
                }
            }
        }

        private void imagePanel_DragOver(object sender, DragEventArgs e)
        {
            if (validData)
            {
                if ((e.X != lastX) || (e.Y != lastY))
                {
                    SetThumbnailLocation(imagePanel.PointToClient(new Point(e.X, e.Y)));
                    lastX = e.X;
                    lastY = e.Y;
                }
            }

        }

        void getCacheDemographics(string medrecnum)
        {
            // search for file patterns of "/1.2...)"
            ArrayList results = WebCacheTool.WinInetAPI.FindUrlCacheEntries(@"/\d\.\d\..*\)$");
            DateTime latest = DateTime.MinValue;
            DateTime current = DateTime.MinValue;
            string fname = string.Empty;
            foreach (WebCacheTool.WinInetAPI.INTERNET_CACHE_ENTRY_INFO entry in results)
            {
                current = WebCacheTool.Win32API.FromFileTime(entry.LastAccessTime);
                if (latest < current)
                {
                    fname = entry.lpszLocalFileName;
                    latest = current;
                }

            }
            if (fname == string.Empty)
                return;
            
            Stream s = new FileStream(fname, FileMode.Open, FileAccess.Read);

            byte[] readBuffer = new byte[4096];
            int bytesRead = s.Read(readBuffer, 0, readBuffer.Length);
            byte[] mrn = { 0x10, 0x00, 0x20, 0x00, 0x4c, 0x4f };
            byte[] pn = { 0x10, 0x00, 0x10, 0x00, 0x50, 0x4e };
            byte[] dob = { 0x10, 0x00, 0x30, 0x00, 0x44, 0x41 };
            byte[] mf = { 0x10, 0x00, 0x40, 0x00, 0x43, 0x53 };
            //byte[] loc = { 0x08, 0x00, 0x80, 0x00, 0x4c, 0x4f };

            if (getDicomString(readBuffer, mrn, bytesRead) == medrecnum)
            {
                textMRN.Text = medrecnum;
            }
            else
            {
                return;  // we've got the wrong file...
            }

            string rawText = getDicomString(readBuffer, pn, bytesRead);
            textName.Text = rawText.Replace("^", " ");

            rawText = getDicomString(readBuffer, dob, bytesRead);
            if (rawText.Length == 8)
            {
                //reformat to MM-DD-YYYY
                rawText = rawText.Substring(4, 2) + "/" +
                    rawText.Substring(6, 2) + "/" +
                    rawText.Substring(0, 4);
            }
            textDOB.Text = rawText;
            textGender.Text = getDicomString(readBuffer, mf, bytesRead);
            //textLoc.Text = getDicomString(readBuffer, loc, bytesRead);
        }

        string getDicomString(byte[] readBuffer, byte[] target, int bytes)
        {
            int fieldLength;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            for (int i = 0; i < bytes - target.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < target.Length; j++)
                    if (target[j] != readBuffer[i + j])
                    {
                        match = false;
                        break;
                    }
                if (match)
                {
                    fieldLength = 255 * readBuffer[i + target.Length + 1] + readBuffer[i + target.Length];
                    byte[] temp = new byte[fieldLength];
                    Array.Copy(readBuffer, i + target.Length + 2, temp, 0, fieldLength);
                    return enc.GetString(temp).Trim();
                }
            }
            return string.Empty;
        }

        string stripTags(string text)
        {
            if (text != null)
            {
                return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            }
            else
            {
                return "";
            }
        }

        string getSectionElement(XmlDocument doc, string tag)
        {
            XmlNode node = doc.SelectSingleNode(@"//section[@heading='" + tag + @"']");
            try
            {
                //return Strip(node.ChildNodes[0].Value);
                return stripTags(node.InnerText);
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        string getElement(XmlDocument doc, string tag)
        {
            XmlNode node = doc.SelectSingleNode("//" + tag);

            try
            {
                //return Strip(node.ChildNodes[0].Value);
                return stripTags(node.InnerText);
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        void getImages(XmlDocument doc, string sourcedir)
        {
            XmlNodeList nodelist = doc.SelectNodes("//image");
            foreach (XmlNode node in nodelist)
            {
                string src = node.Attributes.GetNamedItem("src").Value;
                string imgsource = Path.Combine(sourcedir, src);
                nextImage = LoadUnlockImage(imgsource);
                CaseImage caseImage = new CaseImage(nextImage);
                XmlNode captionNode = node.SelectSingleNode("./image-caption");
                if (captionNode != null)
                {
                    caseImage.caption = captionNode.InnerText;
                }
                caseImages.Add(caseImage);
            }
            if (caseImages.Count > 0)
            {
                pb.Image = caseImages[0].image;
                currentImage = 1;
                AdjustView();
            }
            else
            {
                pb.Image = null;
                currentImage = 0;
            }
            updateImageLabels();
        }

        private void openCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openXMLDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fname = openXMLDialog.FileName;
                string prefix = Path.GetFileNameWithoutExtension(fname);
                string sourcedir = Path.GetDirectoryName(fname);
                XmlDocument doc = new XmlDocument();
                doc.Load(fname);

                clearBoxes(this);
                caseImages.Clear();
                pb.Image = null;

                textKeywords.Text = getElement(doc, "keywords");
                textName.Text = getElement(doc, "pt-name");
                textMRN.Text = getElement(doc, "pt-mrn");
                textGender.Text = getElement(doc, "pt-sex");
                textDOB.Text = getElement(doc, "pt-dob");
                textHistory.Text = getSectionElement(doc, "History");
                textFindings.Text = getSectionElement(doc, "Findings");
                textDiagnosis.Text = getSectionElement(doc, "Diagnosis");
                textDdx.Text = getSectionElement(doc, "Ddx");
                textDiscussion.Text = getSectionElement(doc, "Discussion");
                textLoc.Text = getSectionElement(doc, "Location");

                getImages(doc, sourcedir);
                saveXMLDialog.InitialDirectory = sourcedir;
            }
        }

        private void saveXML(string fname)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            using (XmlWriter writer = XmlWriter.Create(fname, settings))
            {
                writer.WriteStartElement("MIRCdocument");
                writer.WriteAttributeString("display", "mstf");

                writer.WriteElementString("title", DateTime.Now.ToString());

                writer.WriteStartElement("abstract");
                writer.WriteElementString("p", textHistory.Text);
                writer.WriteEndElement();

                if (textKeywords.Text != "")
                {
                    writer.WriteElementString("keywords", textKeywords.Text);
                }

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "History");
                writer.WriteElementString("p", textHistory.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Findings");
                writer.WriteElementString("p", textFindings.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Diagnosis");
                writer.WriteElementString("p", textDiagnosis.Text);
                writer.WriteEndElement();

                if (textDdx.Text != "")
                {
                    writer.WriteStartElement("section");
                    writer.WriteAttributeString("heading", "Ddx");
                    writer.WriteElementString("p", textDdx.Text);
                    writer.WriteEndElement();
                }

                if (textDiscussion.Text != "")
                {
                    writer.WriteStartElement("section");
                    writer.WriteAttributeString("heading", "Discussion");
                    writer.WriteElementString("p", textDiscussion.Text);
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("image-section");
                foreach (CaseImage caseImage in caseImages)
                {
                    writer.WriteStartElement("image");
                    writer.WriteAttributeString("src", caseImage.filename);
                    writer.WriteElementString("format", "png");

                    writer.WriteStartElement("image-caption");
                    writer.WriteAttributeString("display", "always");
                    writer.WriteString(caseImage.caption);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Patient");
                writer.WriteStartElement("patient");
                writer.WriteElementString("pt-name", textName.Text);
                writer.WriteElementString("pt-mrn", textMRN.Text);
                writer.WriteElementString("pt-sex", textGender.Text);
                writer.WriteElementString("pt-dob", textDOB.Text);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Location");
                writer.WriteElementString("p", textLoc.Text);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            DialogResult result = saveXMLDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fname = saveXMLDialog.FileName;
                string prefix = Path.GetFileNameWithoutExtension(fname);
                string targetdir = Path.GetDirectoryName(fname);
                saveAll(prefix, targetdir, false);
                openXMLDialog.InitialDirectory = targetdir;
            }
        }

        private void btnMIRC_Click(object sender, EventArgs e)
        {
            saveAll("case", Path.GetTempPath(), true);
        }

        void saveAll(string prefix, string targetdir, bool sendNet)
        {
            string xmlfname = Path.ChangeExtension(prefix, ".xml");
            string zipname = Path.ChangeExtension(prefix, ".zip");
            string fname, imgpath;
            List<string> imgNames = new List<string>();

            using (ZipFile zip = new ZipFile())
            {
                for (int i = 0; i < caseImages.Count; i++)
                {
                    fname = Path.ChangeExtension(prefix + "_" + i, ".png");
                    caseImages[i].filename = fname;
                    imgpath = Path.Combine(targetdir, fname);
                    caseImages[i].image.Save(imgpath, System.Drawing.Imaging.ImageFormat.Png);
                    zip.AddFile(imgpath, "");
                }

                string xmlPath = Path.Combine(targetdir, xmlfname);
                saveXML(xmlPath);
                zip.AddFile(xmlPath, "");

                if (!sendNet)
                {
                    zip.Save(Path.Combine(targetdir, zipname));

                    //Save a nice HTML file too
                    string htmlPath = Path.ChangeExtension(xmlPath, ".htm");
                    XslCompiledTransform transform = new XslCompiledTransform();
                    StringReader xsltInput = new StringReader(CaseMaker.Properties.Resources.MIRCdocument);
                    XmlTextReader xsltReader = new XmlTextReader(xsltInput);
                    transform.Load(xsltReader);
                    XsltArgumentList xslArg = new XsltArgumentList();
                    xslArg.AddParam("display", "", "mstf");
                    xslArg.AddParam("context-path", "", ".");
                    xslArg.AddParam("user-is-owner", "", "yes");
                    using (XmlWriter w = XmlWriter.Create(htmlPath, transform.OutputSettings))
                    {
                        transform.Transform(xmlPath, xslArg, w);
                    }

                }
                else
                {
                    if (uploadDialog.ShowDialog() == DialogResult.OK)
                    {
                        Uri uri = new Uri(uploadDialog.urlMIRC);
                        WebRequest webRequest = WebRequest.Create(uri);
                        CredentialCache myCache = new CredentialCache();
                        myCache.Add(uri, "Basic", new NetworkCredential(uploadDialog.username, uploadDialog.password));
                        webRequest.Credentials = myCache;

                        webRequest.ContentType = "application/x-zip-compressed";
                        webRequest.Method = "POST";
                        Stream requestStream = webRequest.GetRequestStream();
                        zip.Save(requestStream);
                        requestStream.Close();

                        WebResponse webResponse = webRequest.GetResponse();
                        if (webResponse != null)
                        {
                            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                            Debug.Write(sr.ReadToEnd());
                        }
                    }
                }
            }
        }

        private void imagePanel_Resize(object sender, EventArgs e)
        {
            AdjustView();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void textCaption_TextChanged(object sender, EventArgs e)
        {
            caseImages[currentImage - 1].caption = textCaption.Text;
        }

        private void pb_Click(object sender, EventArgs e)
        {
            // this handler is a workaround to allow the picturebox to redirect
            // focus away from the multiline text fields, so that mouse scrollwheel
            // events are captured by the form
            if (pb.Image != null)
                btnDelete.Select();
        }


    }
}
