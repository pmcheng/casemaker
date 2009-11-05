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
using System.Reflection;
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

        BackgroundWorker bw = new BackgroundWorker();

        UploadMIRCDialog uploadDialog = new UploadMIRCDialog();
        DialogConflict dialogConflict = new DialogConflict();
        BrowserPreview browserPreview = new BrowserPreview();

        string lastFilename = "";
        List<CaseImage> caseImages = new List<CaseImage>();
        int currentImage = 0;
        Image nextImage;
        int lastX = 0;
        int lastY = 0;
        bool isDirty = false;

        Dictionary<string, string> dictLocation = new Dictionary<string, string>()
        {
            {"datasource=https%253A%252F%252Fexternal.synapse.uscuh.com", "UH/Norris"},
            {"datasource=http%253A%252F%252Fsynapse.uscuh.com", "UH/Norris"},
            {"datasource=https%253A%252F%252Ffujipacs.hsc.usc.edu","HCC2"},
            {"datasource=http%253A%252F%252Fhcc2synvweb","HCC2"},
            {"datasource=http%253A%252F%252Flacsynapse","LACUSC"}
        };

        Dictionary<CheckBox, string> dictCategory;


        public MainForm()
        {
            InitializeComponent();
            updateImageLabels();
            saveCaseDialog.InitialDirectory = Application.StartupPath;
            openCaseDialog.InitialDirectory = Application.StartupPath;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseWheel);
            assignHandlers(this);

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

        private void imagePanel_DragDrop(object sender, DragEventArgs e)
        {
            toolStripStatusLabel.Text = "";
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
                    string imageURL = "";
                    string imageUID = "";
                    string studyUID = "";
                    MemoryStream ms = e.Data.GetData("Synapse.FujiOffset") as MemoryStream;
                    if (ms != null)
                    {
                        StreamReader sr = new StreamReader(ms, Encoding.Unicode);
                        string mrn = sr.ReadToEnd().TrimEnd('\0');
                        DialogResult result = DialogResult.Yes;
                        if ((textMRN.Text != "") && (textMRN.Text != mrn))
                        {
                            dialogConflict.Owner = this;
                            result = dialogConflict.ShowDialog();
                            if (result == DialogResult.Cancel) return;

                        }
                        if (result != DialogResult.No)
                        {
                            ms = e.Data.GetData("UniformResourceLocator") as MemoryStream;
                            if (ms != null)
                            {
                                sr = new StreamReader(ms);
                                imageURL = sr.ReadToEnd().TrimEnd('\0');

                                studyUID = Regex.Match(imageURL, @"studyuid=(\d*)").Groups[1].Value;
                                imageUID = Regex.Match(imageURL, @"imageuid=(\d*)").Groups[1].Value;

                                textLoc.Text = mapToLocation(imageURL);

                            }
                            getCacheDemographics(mrn);
                        }
                    }
                    CaseImage caseImage = new CaseImage(nextImage);
                    caseImage.imageURL = imageURL;
                    caseImage.studyUID = studyUID;
                    caseImage.imageUID = imageUID;
                    caseImages.Add(caseImage);
                    pb.Image = nextImage;
                    AdjustView();
                    currentImage = caseImages.Count;
                    updateImageLabels();
                    setDirty(true);
                }
            }
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
            btnDelete.Enabled = (currentImage > 0);
            btnLeft.Enabled = (currentImage > 1);
            btnRight.Enabled = (currentImage < caseImages.Count);
            buttonReorder.Enabled = (currentImage > 0);
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
            isDirty = tempDirty; // updating labels should not change the dirty bit
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
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
            toolStripStatusLabel.Text = "";
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
            toolStripStatusLabel.Text = "";
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
            setDirty(true);
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

            string dicom_mrn = getDicomString(readBuffer, mrn, bytesRead);
            if (dicom_mrn == medrecnum)
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
                string src = node.Attributes.GetNamedItem("src").Value;
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
                    caseImage.imageURL = urlNode.Attributes.GetNamedItem("src").Value;
                    caseImage.studyUID = urlNode.Attributes.GetNamedItem("studyUID").Value;
                    caseImage.imageUID = urlNode.Attributes.GetNamedItem("imageUID").Value;
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

        private void saveXML(string fname, string authorName)
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

                if (authorName != "")
                {
                    writer.WriteStartElement("author");
                    writer.WriteStartElement("name");
                    writer.WriteString(authorName);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

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
                        writer.WriteEndElement();
                    }

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

                writer.WriteStartElement("category");
                writer.WriteString(categories);
                writer.WriteEndElement();

                writer.WriteStartElement("authorization");
                writer.WriteStartElement("read");
                writer.WriteString("department");
                writer.WriteEndElement();
                writer.WriteStartElement("update");
                writer.WriteString("admin");
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
                saveFiles(prefix, tempFolder, "");
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
            if (uploadDialog.ShowDialog() == DialogResult.OK)
            {
                bw.RunWorkerAsync();
            }
        }

        [DebuggerNonUserCode]
        private void sendMIRC(object sender, DoWorkEventArgs e)
        {
            string tempFolder = createTempFolder();
            bw.ReportProgress(0, "Acquiring author names...");
            string authorName = getAuthor();
            bw.ReportProgress(25, "Creating zip payload...");
            saveFiles("case", tempFolder, authorName);
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
            //setDirty(false);
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
            saveFiles("case", tempFolder, "");
            string xmlPath = Path.Combine(tempFolder, "case.xml");
            string htmlPath = Path.Combine(tempFolder, "case.html");
            transformXML(xmlPath, htmlPath);

            browserPreview.Navigate(htmlPath);
            browserPreview.ShowDialog();
            browserPreview.Navigate("about:blank");
            Directory.Delete(tempFolder, true);
        }

        void saveFiles(string prefix, string targetdir, string authorName)
        {
            string xmlfname = Path.ChangeExtension(prefix, ".xml");

            for (int i = 0; i < caseImages.Count; i++)
            {
                string num = "00" + (i+1);
                num = num.Substring(num.Length - 3);
                string fname = Path.ChangeExtension(prefix + "_" + num, ".png");
                caseImages[i].filename = fname;
                string imgpath = Path.Combine(targetdir, fname);
                caseImages[i].image.Save(imgpath, System.Drawing.Imaging.ImageFormat.Png);
            }
            string xmlPath = Path.Combine(targetdir, xmlfname);
            saveXML(xmlPath, authorName);
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

        [DebuggerNonUserCode]
        string getAuthor()
        {
            string authorName = "";
            Uri mircURI = new Uri(uploadDialog.urlMIRC);
            Uri authorURI = new Uri(mircURI, "../authors.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(authorURI.ToString());
            XmlNode node = doc.SelectSingleNode(@"//author[@user='" + uploadDialog.username + @"']");
            XmlNode nameNode = node.SelectSingleNode("./name");
            if (nameNode != null)
            {
                authorName = nameNode.InnerText;
            }
            return authorName;
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

        [DebuggerNonUserCode]
        string sendZip(string sourcedir)
        {
            string return_message = "";
            string[] files = Directory.GetFiles(sourcedir);
            using (ZipFile zip = new ZipFile())
            {
                zip.SaveProgress += sendZip_Progress;
                zip.AddFiles(files, "");

                Uri mircURI = new Uri(uploadDialog.urlMIRC);
                HttpWebRequest mircWebRequest = (HttpWebRequest)WebRequest.Create(mircURI);
                mircWebRequest.SendChunked = true;

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
                string[] responses = { "The zip file was received and unpacked successfully",
                                               "There was a problem unpacking the posted file"};

                foreach (string response in responses)
                {
                    if (message.Contains(response))
                        return_message = response + ".";
                    break;
                }
            }
            return return_message;
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
                    //string tempfile = Path.GetTempFileName() + ".png";
                    //string[] filelist = new string[] { tempfile };
                    //pb.Image.Save(tempfile, System.Drawing.Imaging.ImageFormat.Png);

                    MemoryStream ms1 = new MemoryStream();
                    MemoryStream ms2 = new MemoryStream();
                    pb.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] b = ms1.GetBuffer();
                    ms2.Write(b, 14, (int)ms1.Length - 14);
                    ms1.Position = 0;

                    DataObject obj = new DataObject();
                    obj.SetData(DataFormats.Dib, ms2);
                    //obj.SetData(DataFormats.Bitmap, pb.Image);

                    //obj.SetData(DataFormats.FileDrop, filelist);

                    DoDragDrop(obj, DragDropEffects.Copy);
                }
            }

        }

        private void pb_DoubleClick(object sender, EventArgs e)
        {
            if (currentImage == 0) return;
            string imageURL = caseImages[currentImage - 1].imageURL;
            if (imageURL == "") return;

            string studyUID = caseImages[currentImage - 1].studyUID;
            string url = "";
            switch (mapToLocation(imageURL))
            {
                case "UH/Norris":
                    try
                    {
                        Dns.GetHostEntry("synapse.uscuh.com");
                        url = "http://synapse.uscuh.com/explore.asp?path=//commandclassname=Synapse%26datasource=http%253A%252F%252Fsynapse.uscuh.com";

                    }
                    catch
                    {
                        url = "https://external.synapse.uscuh.com/synapse.asp?path=//commandclassname=Synapse%26datasource=https%253A%252F%252Fexternal.synapse.uscuh.com";

                    }
                    url += "%26/commandclassname=StudyListTemplateFolder%26folderuid=33%26/commandclassname=StudyTemplateFolder%26studyuid=" + studyUID;
                    break;
                case "HCC2":
                    try
                    {
                        Dns.GetHostEntry("synapse.uscuh.com");
                        url = "http://synapse.uscuh.com/explore.asp?path=//commandclassname=Synapse%26datasource=http%253A%252F%252Fhcc2synvweb";
                    }
                    catch
                    {
                        url = "https://external.synapse.uscuh.com/synapse.asp?path=//commandclassname=Synapse%26datasource=https%253A%252F%252Ffujipacs.hsc.usc.edu";
                    }
                    url += "%26/commandclassname=StudyListTemplateFolder%26folderuid=1000002%26/commandclassname=StudyTemplateFolder%26studyuid=" + studyUID;
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
            AdjustView();
            updateImageLabels();
        }
    }
}
