﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Net;

using Ionic.Zip;

namespace CaseMaker
{
    public partial class MainForm : Form
    {
        bool validData;

        //Thread getImageThread;

        BackgroundWorker bw = new BackgroundWorker();

        UploadMIRCDialog uploadDialog;
        DialogConflict dialogConflict = new DialogConflict();
        BrowserPreview browserPreview = new BrowserPreview();

        string dragFilename = "";
        List<CaseImage> caseImages = new List<CaseImage>();
        int currentImage = 0;
        Image nextImage;
        int lastX = 0;
        int lastY = 0;
        bool isDirty = false;

        Dictionary<string, string> dictLocation;
        Dictionary<CheckBox, string> dictCategory;


        public MainForm()
        {
            InitializeComponent();
            updateImageLabels();
            saveCaseDialog.InitialDirectory = Application.StartupPath;
            openCaseDialog.InitialDirectory = Application.StartupPath;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseWheel);
            assignHandlers(this);

            string config_file= Path.Combine(Application.StartupPath, "casemaker_config.xml");

            List<string> urlList = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(config_file);
                XmlNodeList nodelist = doc.SelectNodes("//location");
                dictLocation = new Dictionary<string, string>();
                Debug.WriteLine("Reading locations");
                foreach (XmlNode node in nodelist)
                {
                    string matchstring = node.Attributes["matchstring"].Value;
                    string site = node.Attributes["site"].Value;
                    dictLocation.Add(matchstring, site);
                }
                Debug.WriteLine("Reading upload URLs");
                nodelist = doc.SelectNodes("//site");
                foreach (XmlNode node in nodelist)
                {
                    string url = node.Attributes["url"].Value;
                    urlList.Add(url);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Config XML error: "+e.Message);
                dictLocation = new Dictionary<string, string>() {
                    {"https://keckimaging.usc.edu","Keck/Norris"},
                    {"https://keckimaging.med.usc.edu","Keck/Norris"},
                    {"datasource=https%253A%252F%252Fkeckimaging.usc.edu","Keck/Norris"},
                    {"datasource=https%253A%252F%252Fkeckimaging.med.usc.edu","Keck/Norris"},
                    {"datasource=https%253A%252F%252Fexternal.synapse.uscuh.com", "UH/Norris"},
                    {"datasource=http%253A%252F%252Fsynapse.uscuh.com", "UH/Norris"},
                    {"datasource=https%253A%252F%252Ffujipacs.hsc.usc.edu","HCC2"},
                    {"datasource=http%253A%252F%252Fhcc2synvweb","HCC2"},
                    {"datasource=http%253A%252F%252Flacsynapse","LACUSC"},
                    {"dhssynapse","LACUSC"},
                    {"datasource=http%253A%252F%252Fdhssynapse","LACUSC"},
                    {"datasource=http%253A%252F%252F160uscpacdbpw02","LACUSC"}
                };
                urlList = new List<string>()
                {
                    "http://207.151.6.168:8080/submit/ss1",
                    "http://192.168.0.101:8080/submit/ss1",
                    "http://mirc.usc.edu/submit/ss1"
                };
            }
            uploadDialog = new UploadMIRCDialog(urlList);

            dictCategory = new Dictionary<CheckBox, string>()
            {
                {checkBoxPulm,"Pulmonary"},
                {checkBoxCV,"Cardiovascular"},
                {checkBoxGI,"Gastrointestinal"},
                {checkBoxGU,"Genitourinary"},
                {checkBoxIR,"Vascular|Interventional"},
                {checkBoxMammo,"Breast"},
                {checkBoxMSK,"Musculoskeletal"},
                {checkBoxNeuro,"Neuro"},
                {checkBoxUltrasound,"Ultrasound"},
                {checkBoxPeds,"Pediatric"},
                {checkBoxNucs,"Nuclear"}
            };
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;  // activate TLS 1.2
            ServicePointManager.Expect100Continue = false;

            // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            bw.WorkerReportsProgress = true;
            bw.DoWork += sendMIRC;
            bw.ProgressChanged += sendMIRC_Progress;
            bw.RunWorkerCompleted += sendMIRC_Completed;
        }

        void clearBoxes(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = "";
                }
                if (ctrl is CheckBox)
                    ((CheckBox)(ctrl)).Checked = false;
                if (ctrl.HasChildren)
                {
                    clearBoxes(ctrl);
                }
            }
        }

        void assignHandlers(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).TextChanged += new EventHandler(caseChanged);
                }
                if (ctrl is CheckBox)
                {
                    (ctrl as CheckBox).CheckedChanged += new EventHandler(caseChanged);
                }
                if (ctrl.HasChildren)
                {
                    assignHandlers(ctrl);
                }
            }
        }

        void caseChanged(object sender, EventArgs e)
        {
            setDirty(true);
            toolStripStatusLabel.Text = "";
        }

        void setDirty(bool isDirty)
        {
            this.isDirty = isDirty;
            if (isDirty)
                this.Text = "CaseMaker*";
            else
                this.Text = "CaseMaker";

        }


        private void newCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (!manageDirtyCase()) return;
            clearBoxes(this);
            caseImages.Clear();
            pb.Image = null;
            currentImage = 0;
            updateImageLabels();
            setDirty(false);
        }

        private void imagePanel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (e.Delta > 0) btnLeft_Click(sender, e);
            if (e.Delta < 0) btnRight_Click(sender, e);
        }

        private void imagePanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            thumbnail.Image = null;
            thumbnail.Visible = false;

            validData = false;

            if (e.Data.GetData("CaseMaker") == this) return;

            if (GetFilename(out dragFilename, e))
            {
                validData = true;
                nextImage = LoadUnlockImage(dragFilename);
            } else if (Clipboard.ContainsImage())
            {
                validData = true;
                nextImage = Clipboard.GetImage();
            }
            if (validData) { 
                if (nextImage != null)
                {
                    AssignImage();
                    e.Effect = DragDropEffects.Copy;
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

        private void imagePanel_DragLeave(object sender, EventArgs e)
        {
            thumbnail.Visible = false;
        }

        private void imagePanel_DragDrop(object sender, DragEventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (validData)
            {
                thumbnail.Visible = false;

                if (pb.Image != nextImage)
                {
                    string imageURL = "";
                    string imageUID = "";
                    string studyUID = "";
                    string studyURL = "";
                    string accession = "";
                    string mrn = "";

                    if (e.Data.GetDataPresent("Text"))
                    {
                        imageURL = (String)e.Data.GetData("Text");
                        System.Collections.Specialized.NameValueCollection qscoll = HttpUtility.ParseQueryString(imageURL);
                        mrn = qscoll.Get("DDsrcPatientMRN");
                        imageUID = qscoll.Get("objectUID");
                        studyUID = qscoll.Get("studyUID");
                    }
                    MemoryStream ms = e.Data.GetData("Synapse.FujiOffset") as MemoryStream;
                    if (ms != null)
                    {
                        StreamReader sr = new StreamReader(ms, Encoding.Unicode);
                        mrn = sr.ReadToEnd().TrimEnd('\0');
                    }

                    if (mrn!="") {
                        DialogResult result = DialogResult.Yes;
                        if ((textMRN.Text != "") && (textMRN.Text != mrn))
                        {
                            dialogConflict.Owner = this;
                            result = dialogConflict.ShowDialog();
                            if (result == DialogResult.Cancel) return;

                        }
                        if (result != DialogResult.No)
                        {
                            ms = e.Data.GetData("Synapse.TC") as MemoryStream;
                            if (ms != null)
                            {
                                StreamReader sr = new StreamReader(ms, Encoding.Unicode);
                                string[] tcString = sr.ReadToEnd().Trim('\0').Split(',');
                                foreach (string s in tcString)
                                {
                                    if (s.StartsWith("U="))
                                        studyURL = s.Substring(2);
                                    if (s.StartsWith("A="))
                                        accession = s.Substring(2);
                                }

                            }
                            ms = e.Data.GetData("UniformResourceLocator") as MemoryStream;
                            textLoc.Text = "";
                            if ((ms != null) && (studyUID == ""))
                            {
                                StreamReader sr = new StreamReader(ms);
                                imageURL = sr.ReadToEnd().TrimEnd('\0');

                                int epath_loc = imageURL.IndexOf("epath=");
                                if (epath_loc != -1)
                                {
                                    string epath = DecodeFrom64(imageURL.Substring(epath_loc + 6));
                                    epath = epath.Substring(0, epath.LastIndexOf("&") + 1);
                                    epath = epath.Replace("&", "%26");
                                    epath = epath.Replace("%3A", "%253A");
                                    epath = epath.Replace("%2F", "%252F");
                                    imageURL = imageURL.Substring(0, epath_loc) + "path=" + epath;
                                }

                                studyUID = Regex.Match(imageURL, @"studyuid=(\d*)").Groups[1].Value;
                                imageUID = Regex.Match(imageURL, @"imageuid=(\d*)").Groups[1].Value;
                            }
                            if (imageURL != "")
                            {
                                textLoc.Text = mapToLocation(imageURL);
                            }
                            textMRN.Text = mrn;
                            //getCacheDemographics(mrn, accession);
                        }
                    }
                    CaseImage caseImage = new CaseImage(nextImage);
                    caseImage.imageURL = imageURL;
                    caseImage.studyURL = studyURL;
                    caseImage.studyAccession = accession;
                    caseImage.studyUID = studyUID;
                    caseImage.imageUID = imageUID;
                    caseImages.Add(caseImage);
                    pb.Image = nextImage;
                    currentImage = caseImages.Count;
                    updateImageLabels();
                    setDirty(true);
                }
            }
        }

        private string DecodeFrom64(string base64)
        {
            if (base64.Length % 4 == 1) base64 = base64.Substring(0, base64.Length - 1);
            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(base64);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        string mapToLocation(string URL)
        // map URL to physical location
        {
            foreach (string key in dictLocation.Keys)
            {
                if (URL.Contains(key)) return dictLocation[key];
            }
            return "";
        }

        void updateImageLabels()
        {
            bool tempDirty = isDirty;
            countLabel.Text = currentImage + " / " + caseImages.Count;

            btnLeft.Enabled = (currentImage > 1);
            btnRight.Enabled = (currentImage < caseImages.Count);

            btnDelete.Enabled = (currentImage > 0);
            buttonReorder.Enabled = (currentImage > 0);

            if (pb.Image == null)
            {
                textCaption.Text = "";
                textCaption.Enabled = false;
                pb.ContextMenuStrip = null;
            }
            else
            {
                textCaption.Text = caseImages[currentImage - 1].caption;
                textCaption.Enabled = true;
                pb.ContextMenuStrip = pbContextMenuStrip;
            }
            isDirty = tempDirty; // updating labels should not change the dirty bit
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (currentImage > 1)
            {
                currentImage--;
                pb.Image = caseImages[currentImage - 1].image;
                updateImageLabels();
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (currentImage < caseImages.Count)
            {
                currentImage++;
                pb.Image = caseImages[currentImage - 1].image;
                updateImageLabels();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            caseImages.RemoveAt(currentImage - 1);
            if (currentImage > caseImages.Count)
            {
                currentImage--;
            }
            if (caseImages.Count > 0)
            {
                pb.Image = caseImages[currentImage - 1].image;
            }
            else
            {
                pb.Image = null;
            }
            updateImageLabels();
            setDirty(true);
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
                        ret = true;
                    }
                }
            }
            return ret;
        }



        public delegate void AssignImageDlgt();

        Image LoadUnlockImage(string asFile)
        {
            // This is necessary because Image constructor ordinarily
            // keeps file locked after opening
            Stream BitmapStream = File.Open(asFile, FileMode.Open);
            try
            {
                Image img = Image.FromStream(BitmapStream);
                return new Bitmap(img);
            }
            catch
            {
                return null;
            }
            finally
            {
                BitmapStream.Close();
            }
        }

        void LoadImage()
        {
            nextImage = LoadUnlockImage(dragFilename);
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



        string convertDate(string strDate)
        {
            if (strDate.Length == 8)
            {
                //reformat to MM-DD-YYYY
                strDate = strDate.Substring(4, 2) + "/" +
                    strDate.Substring(6, 2) + "/" +
                    strDate.Substring(0, 4);
            }
            return strDate;
        }

        void getCacheDemographics(string medrecnum, string accession)
        {
            // search for file patterns of "/1.2...)"
            ArrayList results = WebCacheTool.WinInetAPI.FindUrlCacheEntries(@"/\d\.\d.*\)$");
            ArrayList prefixes = new ArrayList();
            SortedList<DateTime, String> slist = new SortedList<DateTime, String>();

            foreach (WebCacheTool.WinInetAPI.INTERNET_CACHE_ENTRY_INFO entry in results)
            {
                DateTime dt = WebCacheTool.Win32API.FromFileTime(entry.LastAccessTime);
                if ((DateTime.Now - dt) > TimeSpan.FromHours(24)) continue;
                string fname = entry.lpszLocalFileName;
                int index = fname.IndexOf('(');
                if (index > 0)
                {
                    string prefix = fname.Substring(0, index);
                    if (prefixes.Contains(prefix) || slist.ContainsKey(dt)) continue;
                    prefixes.Add(prefix);
                    slist.Add(dt, fname);
                }
            }

            byte[] readBuffer = new byte[4096];
            int bytesRead;

            byte[] mrn = { 0x10, 0x00, 0x20, 0x00, 0x4c, 0x4f };
            byte[] acc = { 0x08, 0x00, 0x50, 0x00, 0x53, 0x48 };
            string accnum = "";
            string dicom_mrn = "";
            string fname_match = "";

            if (accession != "")
            {
                for (int i = slist.Count - 1; i >= 0; i--)
                {
                    fname_match = slist.Values[i];
                    using (Stream s = new FileStream(fname_match, FileMode.Open, FileAccess.Read))
                    {
                        bytesRead = s.Read(readBuffer, 0, readBuffer.Length);
                        accnum = getDicomString(readBuffer, acc, bytesRead);
                        if (accnum == accession) break;
                    }
                }
                if (accnum != accession)
                {
                    fname_match = "";
                    accession = "";
                }
            }
            if (fname_match == "")
            {
                for (int i = slist.Count - 1; i >= 0; i--)
                {
                    fname_match = slist.Values[i];
                    using (Stream s = new FileStream(fname_match, FileMode.Open, FileAccess.Read))
                    {
                        bytesRead = s.Read(readBuffer, 0, readBuffer.Length);
                        dicom_mrn = getDicomString(readBuffer, mrn, bytesRead);
                        if (dicom_mrn == medrecnum) break;
                    }
                }
                if (dicom_mrn != medrecnum)
                {
                    return;
                }
            }

            Stream fstream = new FileStream(fname_match, FileMode.Open, FileAccess.Read);

            bytesRead = fstream.Read(readBuffer, 0, readBuffer.Length);
            byte[] pn = { 0x10, 0x00, 0x10, 0x00, 0x50, 0x4e };
            byte[] dob = { 0x10, 0x00, 0x30, 0x00, 0x44, 0x41 };
            byte[] mf = { 0x10, 0x00, 0x40, 0x00, 0x43, 0x53 };
            //byte[] acc = { 0x08, 0x00, 0x50, 0x00, 0x53, 0x48 };
            //byte[] date = { 0x08, 0x00, 0x20, 0x00, 0x44, 0x41 };
            byte[] loc = { 0x08, 0x00, 0x80, 0x00, 0x4c, 0x4f };

            dicom_mrn = getDicomString(readBuffer, mrn, bytesRead);
            if ((dicom_mrn == medrecnum) || (dicom_mrn == ""))
            {
                textMRN.Text = medrecnum;
            }
            else
            {
                return;  // we've got the wrong file...
            }

            string rawText = getDicomString(readBuffer, pn, bytesRead);
            //textName.Text = rawText.Replace("^", " ");

            rawText = getDicomString(readBuffer, dob, bytesRead);
            //textDOB.Text = convertDate(rawText);
            //textGender.Text = getDicomString(readBuffer, mf, bytesRead);

            //string dicom_acc = getDicomString(readBuffer, acc, bytesRead);
            //if (dicom_acc == accession)
            //{
            //    string strDate = getDicomString(readBuffer, date, bytesRead);
            //}
            if (textLoc.Text == "")
            {
                textLoc.Text = getDicomString(readBuffer, loc, bytesRead);
            }
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
                    fieldLength = 256 * readBuffer[i + target.Length + 1] + readBuffer[i + target.Length];
                    byte[] temp = new byte[fieldLength];
                    Array.Copy(readBuffer, i + target.Length + 2, temp, 0, fieldLength);
                    return enc.GetString(temp).Trim().TrimEnd('\0');
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
                string src = node.Attributes["src"].Value;
                string imgsource = Path.Combine(sourcedir, src);
                nextImage = LoadUnlockImage(imgsource);
                CaseImage caseImage = new CaseImage(nextImage);
                XmlNode captionNode = node.SelectSingleNode("./image-caption");
                if (captionNode != null)
                {
                    caseImage.caption = captionNode.InnerText;
                }
                XmlNode urlNode = node.SelectSingleNode("./pacs-image");
                if (urlNode != null)
                {
                    caseImage.imageURL = urlNode.Attributes["src"].Value;
                    caseImage.studyUID = urlNode.Attributes["studyUID"].Value;
                    caseImage.imageUID = urlNode.Attributes["imageUID"].Value;
                    XmlNode testNode = urlNode.Attributes["studyURL"];
                    if (testNode != null)
                        caseImage.studyURL = testNode.Value;
                    testNode = urlNode.Attributes["studyAccession"];
                    if (testNode != null)
                        caseImage.studyAccession = testNode.Value;
                }
                caseImages.Add(caseImage);
            }
            if (caseImages.Count > 0)
            {
                pb.Image = caseImages[0].image;
                currentImage = 1;
            }
            else
            {
                pb.Image = null;
                currentImage = 0;
            }
            updateImageLabels();
        }

        bool manageDirtyCase()
        {
            if (!isDirty) return true;
            switch (MessageBox.Show("You have made changes to the current case.  Do you want to save it?", "CaseMaker", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
            {
                case DialogResult.Yes:
                    if (!saveCase()) return false;
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    return false;
            }
            return true;

        }

        private void openCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            if (!manageDirtyCase()) return;

            DialogResult result = openCaseDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fname = openCaseDialog.FileName;
                if (fname.ToLower().EndsWith(".xml"))
                {
                    openCase(fname);
                }
                else if (fname.ToLower().EndsWith(".zip"))
                {
                    string tempFolder = createTempFolder();
                    string xmlfile = "";
                    using (ZipFile zip = ZipFile.Read(fname))
                    {
                        foreach (ZipEntry ze in zip)
                        {
                            ze.Extract(tempFolder, ExtractExistingFileAction.OverwriteSilently);
                            if (ze.FileName.ToLower().EndsWith(".xml"))
                                xmlfile = ze.FileName;
                        }
                    }
                    if (xmlfile != "")
                        openCase(Path.Combine(tempFolder, xmlfile));
                    Directory.Delete(tempFolder, true);
                }
            }

        }


        void openCase(string fname)
        {
            string prefix = Path.GetFileNameWithoutExtension(fname);
            string sourcedir = Path.GetDirectoryName(fname);
            XmlDocument doc = new XmlDocument();
            doc.Load(fname);

            clearBoxes(this);
            caseImages.Clear();
            pb.Image = null;

            textKeywords.Text = getElement(doc, "keywords");
            //textName.Text = getElement(doc, "pt-name");
            textMRN.Text = getElement(doc, "pt-mrn");
            //textGender.Text = getElement(doc, "pt-sex");
            //textDOB.Text = getElement(doc, "pt-dob");
            textHistory.Text = getSectionElement(doc, "History");
            textFindings.Text = getSectionElement(doc, "Findings");
            textDiagnosis.Text = getSectionElement(doc, "Diagnosis");
            textDdx.Text = getSectionElement(doc, "Ddx");
            textDiscussion.Text = getSectionElement(doc, "Discussion");
            textLoc.Text = getSectionElement(doc, "Location");

            string categories = getElement(doc, "category");
            foreach (CheckBox box in dictCategory.Keys)
            {
                if (categories.Contains(dictCategory[box]))
                    box.Checked = true;
                else
                    box.Checked = false;
            }

            getImages(doc, sourcedir);
            saveCaseDialog.InitialDirectory = sourcedir;
            setDirty(false);

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

                writer.WriteStartElement("creator");
                writer.WriteString("CaseMaker - version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
                writer.WriteEndElement();

                //if (authorName != "")
                //{
                writer.WriteStartElement("author");
                writer.WriteStartElement("name");
                //writer.WriteString(authorName);
                writer.WriteEndElement();
                writer.WriteEndElement();
                //}

                string categories = "";

                foreach (CheckBox box in dictCategory.Keys)
                {
                    if (box.Checked)
                    {
                        if (categories != "") categories += " ";
                        categories += dictCategory[box];
                    }
                }

                writer.WriteElementString("title", categories + " " + DateTime.Now.ToString());

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

                    if (caseImage.imageURL != "")
                    {
                        writer.WriteStartElement("pacs-image");
                        writer.WriteAttributeString("src", caseImage.imageURL);
                        writer.WriteAttributeString("studyUID", caseImage.studyUID);
                        writer.WriteAttributeString("imageUID", caseImage.imageUID);
                        if (caseImage.studyAccession != "")
                            writer.WriteAttributeString("studyAccession", caseImage.studyAccession);
                        if (caseImage.studyURL != "")
                            writer.WriteAttributeString("studyURL", caseImage.studyURL);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Patient");
                writer.WriteStartElement("patient");
                //writer.WriteElementString("pt-name", textName.Text);
                writer.WriteElementString("pt-mrn", textMRN.Text);
                //writer.WriteElementString("pt-sex", textGender.Text);
                //writer.WriteElementString("pt-dob", textDOB.Text);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("section");
                writer.WriteAttributeString("heading", "Location");
                writer.WriteElementString("p", textLoc.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("category");
                writer.WriteString(categories);
                writer.WriteEndElement();

                writer.WriteStartElement("authorization");
                writer.WriteStartElement("read");
                if (uploadDialog.privateCase)
                {
                    writer.WriteString("admin");
                }
                else
                {
                    writer.WriteString("department");
                }
                writer.WriteEndElement();
                writer.WriteStartElement("update");
                writer.WriteString("admin");
                writer.WriteEndElement();

                writer.WriteStartElement("owner");
                //writer.WriteString(authorName);
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        string createTempFolder()
        {
            string tempFolder;
            do
            {
                tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            } while (Directory.Exists(tempFolder));
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        private void btnSaveDisk_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            saveCase();
        }

        bool saveCase()
        {
            DialogResult result = saveCaseDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fname = saveCaseDialog.FileName;
                string prefix = Path.GetFileNameWithoutExtension(fname);
                string targetdir = Path.GetDirectoryName(fname);

                string tempFolder = createTempFolder();
                //saveAll(prefix, targetdir, false);
                saveFiles(prefix, tempFolder);
                saveZip(prefix, tempFolder, targetdir);
                Directory.Delete(tempFolder, true);
                openCaseDialog.InitialDirectory = targetdir;
                setDirty(false);
                return true;
            }
            else return false;
        }

        private void btnMIRC_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            uploadDialog.privateCase = false;
            if (uploadDialog.ShowDialog() == DialogResult.OK)
            {
                string tempFolder = createTempFolder();
                saveFiles("case", tempFolder);
                bw.RunWorkerAsync(tempFolder);

            }
        }

        private void sendMIRC(object sender, DoWorkEventArgs e)
        {
            string tempFolder = e.Argument as string;
            string result = sendZip(tempFolder);
            bw.ReportProgress(100, result);
            Directory.Delete(tempFolder, true);
        }

        private void sendMIRC_Progress(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Visible = true;
            toolStripProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel.Text = e.UserState as string;
        }

        private void sendMIRC_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                toolStripStatusLabel.Text = e.Error.Message;
            }
            toolStripProgressBar.Visible = false;
        }

        void transformXML(string xmlPath, string htmlPath)
        {
            //Save an HTML in the format of MIRC teaching file           
            XslCompiledTransform transform = new XslCompiledTransform();

            StringReader xsltInput = new StringReader(CaseMaker.Properties.Resources.MIRCdocument);
            XmlTextReader xsltReader = new XmlTextReader(xsltInput);
            transform.Load(xsltReader);

            XsltArgumentList xslArg = new XsltArgumentList();
            xslArg.AddParam("display", "", "mstf");
            xslArg.AddParam("context-path", "", ".");
            xslArg.AddParam("user-is-owner", "", "yes");

            using (FileStream htmlStream = File.Create(htmlPath))
            using (XmlWriter w = XmlWriter.Create(htmlStream, transform.OutputSettings))
            {
                transform.Transform(xmlPath, xslArg, w);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tempFolder = createTempFolder();
            saveFiles("case", tempFolder);
            string xmlPath = Path.Combine(tempFolder, "case.xml");
            string htmlPath = Path.Combine(tempFolder, "case.html");
            transformXML(xmlPath, htmlPath);

            browserPreview.Navigate(htmlPath);
            browserPreview.ShowDialog();
            browserPreview.Navigate("about:blank");
            Directory.Delete(tempFolder, true);
        }

        void saveFiles(string prefix, string targetdir)
        {
            for (int i = 0; i < caseImages.Count; i++)
            {
                string num = "00" + (i + 1);
                num = num.Substring(num.Length - 3);
                string fname = Path.ChangeExtension(prefix + "_" + num, ".png");
                caseImages[i].filename = fname;
                string imgpath = Path.Combine(targetdir, fname);
                caseImages[i].image.Save(imgpath, System.Drawing.Imaging.ImageFormat.Png);
            }
            string xmlfname = Path.ChangeExtension(prefix, ".xml");
            string xmlPath = Path.Combine(targetdir, xmlfname);
            saveXML(xmlPath);
        }

        void saveZip(string prefix, string sourcedir, string targetdir)
        {

            string zipname = Path.ChangeExtension(prefix, ".zip");
            string[] files = Directory.GetFiles(sourcedir);
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(files, "");
                zip.Save(Path.Combine(targetdir, zipname));
            }
        }


        void sendZip_Progress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                if (e.TotalBytesToTransfer > 0)
                {
                    bw.ReportProgress((int)(e.BytesTransferred / (0.01 * e.TotalBytesToTransfer)), "Packing and uploading " + e.CurrentEntry.FileName + " ...");
                }
            }
            if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                bw.ReportProgress(100, "Finished upload, awaiting confirmation from MIRC server...");
            }
        }

        string sendZip(string sourcedir)
        {
            string return_message = "";
            string[] files = Directory.GetFiles(sourcedir);

            using (ZipFile zip = new ZipFile())
            {
                zip.SaveProgress += sendZip_Progress;
                zip.AddFiles(files, "");
                //zip.Save(Path.Combine(targetdir, zipname));
                Uri mircURI = new Uri(uploadDialog.urlMIRC);
                HttpWebRequest mircWebRequest = (HttpWebRequest)WebRequest.Create(mircURI);
                //mircWebRequest.SendChunked = true;  // new MIRC does not accept chunked data 

                CredentialCache mircCredentialCache = new CredentialCache();
                mircCredentialCache.Add(mircURI, "Basic", new NetworkCredential(uploadDialog.username, uploadDialog.password));
                mircWebRequest.Credentials = mircCredentialCache;

                mircWebRequest.ContentType = "application/x-zip-compressed";
                mircWebRequest.Method = "POST";

                using (Stream requestStream = mircWebRequest.GetRequestStream())
                {
                    zip.Save(requestStream);
                }
                WebResponse webResponse = mircWebRequest.GetResponse();
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                string message = (sr.ReadToEnd());
                string[] responses = { "The file was received and queued for processing.",
                                               "Unable to create the processing thread for the submission."};

                foreach (string response in responses)
                {
                    if (message.Contains(response))
                        return_message = response;
                    break;
                }
            }
            return return_message;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void textCaption_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            caseImages[currentImage - 1].caption = textCaption.Text;
        }


        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (pb.Image != null)
            {
                // allow the picturebox to redirect focus away from the multiline text fields, 
                // so that mouse scrollwheel events are captured by the form
                btnDelete.Select();

                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    string num = "00" + currentImage;
                    num = num.Substring(num.Length - 3);
                    string tempfile = Path.Combine(Path.GetTempPath(), "case_" + num + ".png");
                    string[] filelist = new string[] { tempfile };
                    pb.Image.Save(tempfile, System.Drawing.Imaging.ImageFormat.Png);

                    //MemoryStream ms1 = new MemoryStream();
                    //MemoryStream ms2 = new MemoryStream();
                    //pb.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
                    //byte[] b = ms1.GetBuffer();
                    //ms2.Write(b, 14, (int)ms1.Length - 14);
                    //ms1.Position = 0;

                    DataObject obj = new DataObject();
                    obj.SetData("CaseMaker", this);

                    //obj.SetData(DataFormats.Dib, ms2);
                    //obj.SetData(DataFormats.Bitmap, pb.Image);

                    obj.SetData(DataFormats.FileDrop, filelist);

                    DoDragDrop(obj, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }

        }

        private void pb_DoubleClick(object sender, EventArgs e)
        {
            // This is a local feature to allow double-clicking on an image to launch
            // Synapse in an IE window

            if (currentImage == 0) return;
            string imageURL = caseImages[currentImage - 1].imageURL;
            if (imageURL == "") return;

            string studyUID = caseImages[currentImage - 1].studyUID;
            string url = "";
            switch (mapToLocation(imageURL))
            {
                case "UH/Norris":
                case "Keck/Norris":
                case "HCC2":
                    url = "https://keckimaging.med.usc.edu/explore.asp?path=//commandclassname=Synapse%26datasource=https%253A%252F%252Fkeckimaging.usc.edu";
                    url += "%26/commandclassname=StudyListTemplateFolder%26folderuid=33%26/commandclassname=StudyTemplateFolder%26studyuid=" + studyUID;
                    break;
                case "LACUSC":
                    try
                    {
                        Dns.GetHostEntry("lacsynapse");
                        url = "http://lacsynapse/synapse.asp?path=//commandclassname=Synapse%26datasource=http%253A%252F%252Flacsynapse%26/commandclassname=StudyListTemplateFolder%26folderuid=29%26/commandclassname=StudyTemplateFolder%26studyuid=" + studyUID;
                    }
                    catch
                    {
                        url = "";
                    }
                    break;
            }
            if (url != "")
                Process.Start("IExplore.exe", url);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !(manageDirtyCase());
        }

        private void buttonReorder_Click(object sender, EventArgs e)
        {
            DialogReorder dr = new DialogReorder();
            dr.populate(caseImages);
            dr.ShowDialog();
            caseImages = dr.caseImages;

            pb.Image = caseImages[currentImage - 1].image;
            updateImageLabels();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBoxProperties pbp = new PictureBoxProperties();
            CaseImage cs = caseImages[currentImage - 1];
            pbp.AddProp("Width", cs.image.Width.ToString());
            pbp.AddProp("Height", cs.image.Height.ToString());
            pbp.AddProp("Image URL", cs.imageURL);
            pbp.AddProp("Study URL", cs.studyURL);
            pbp.AddProp("Study Accession", cs.studyAccession);

            pbp.ShowDialog();

        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaseImage cs = caseImages[currentImage - 1];
            Clipboard.SetImage(cs.image);
        }
    }
}
