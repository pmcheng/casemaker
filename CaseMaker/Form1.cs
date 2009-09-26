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
        List<Image> images = new List<Image>();
        int currentImage = 0;
        Image nextImage;
        int lastX = 0;
        int lastY = 0;

        public MainForm()
        {
            InitializeComponent();
            updateCountLabel();
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
            images.Clear();
            pb.Image = null;
            updateCountLabel();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearBoxes(this);
            images.Clear();
            pb.Image = null;
            updateCountLabel();

        }

        private void imagePanel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0) btnLeft_Click(sender, e);
            if (e.Delta < 0) btnRight_Click(sender, e);
        }

        private void imagePanel_DragDrop(object sender, DragEventArgs e)
        {
            string filename = "";
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
                    images.Add(nextImage);
                    currentImage = images.Count;
                    updateCountLabel();

                    GetFilename(out filename, e);
                    filename = Path.GetFileName(filename);

                    if (Regex.IsMatch(filename, @"^\d\.\d\."))
                    {
                        getCacheDemographics();
                    }
                }
            }
        }

        void updateCountLabel()
        {
            countLabel.Text = currentImage + " / " + images.Count;
            btnDelete.Enabled = (currentImage>0);
            btnLeft.Enabled= (currentImage > 1);
            btnRight.Enabled= (currentImage < images.Count);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentImage > 1)
            {
                currentImage--;
                pb.Image = images[currentImage - 1];
                AdjustView();
                updateCountLabel();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentImage < images.Count)
            {
                currentImage++;
                pb.Image = images[currentImage - 1];
                AdjustView();
                updateCountLabel();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            images.RemoveAt(currentImage-1);
            if (currentImage > images.Count)
            {
                currentImage--;
            }
            if (images.Count > 0)
            {
                pb.Image = images[currentImage - 1];
                AdjustView();
            }
            else
            {
                pb.Image = null;
            }
            updateCountLabel();
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
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
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
            // 100    iWidth
            // ---- = ------
            // tHeight  iHeight
            thumbnail.Height = nextImage.Height * 100 / nextImage.Width;
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

                float fw = imagePanel.Width - 2;
                float fh = imagePanel.Height - 2;
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

        void getCacheDemographics()
        {
            ArrayList results = WebCacheTool.WinInetAPI.FindUrlCacheEntries(@"/\d\.\d\.");
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
            byte[] pn = { 0x10, 0x00, 0x10, 0x00, 0x50, 0x4e };
            byte[] mrn = { 0x10, 0x00, 0x20, 0x00, 0x4c, 0x4f };
            byte[] dob = { 0x10, 0x00, 0x30, 0x00, 0x44, 0x41 };
            byte[] mf = { 0x10, 0x00, 0x40, 0x00, 0x43, 0x53 };
            byte[] loc = { 0x08, 0x00, 0x80, 0x00, 0x4c, 0x4f };
            string rawText = getDicomString(readBuffer, pn, bytesRead);
            textName.Text = rawText.Replace("^", " ");
            textMRN.Text = getDicomString(readBuffer, mrn, bytesRead);

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
            textLoc.Text = getDicomString(readBuffer, loc, bytesRead);
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
                    return enc.GetString(temp);
                }
            }
            return string.Empty;
        }

        string getSectionElement(XmlDocument doc, string tag)
        {
            XmlNode node = doc.SelectSingleNode(@"//section[@heading='"+tag+@"']");
            try
            {
                return node.ChildNodes[0].ChildNodes[0].Value;
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        string getElement(XmlDocument doc, string tag)
        {
            XmlNode node = doc.SelectSingleNode("//"+tag);
            
            if (node!=null)
            {
                return node.ChildNodes[0].Value;
            }
            else
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
                images.Add(nextImage);
            }
            pb.Image = nextImage;
            currentImage = images.Count;
            AdjustView();
            updateCountLabel();
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
                images.Clear();
                pb.Image = null;

                textKeywords.Text = getElement(doc,"keywords");
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

        private void saveXML(string fname, List<string> imgNames)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            using (XmlWriter writer = XmlWriter.Create(fname, settings))
            {
                writer.WriteStartElement("MIRCdocument");
                writer.WriteAttributeString("display", "mstf");

                writer.WriteElementString("title",DateTime.Now.ToString());

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
                foreach (string imgname in imgNames)
                {
                    writer.WriteStartElement("image");
                    writer.WriteAttributeString("src", imgname);
                    writer.WriteElementString("format", "png");
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
                for (int i = 0; i < images.Count; i++)
                {
                    fname = Path.ChangeExtension(prefix + "_" + i, ".png");
                    imgNames.Add(fname);
                    imgpath = Path.Combine(targetdir, fname);
                    images[i].Save(imgpath, System.Drawing.Imaging.ImageFormat.Png);
                    zip.AddFile(imgpath, "");
                }
                
                string xmlPath=Path.Combine(targetdir, xmlfname);
                saveXML(xmlPath, imgNames);
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


    }
}
