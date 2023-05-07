namespace CaseMaker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imagePanel = new System.Windows.Forms.Panel();
            this.thumbnail = new System.Windows.Forms.PictureBox();
            this.pb = new System.Windows.Forms.PictureBox();
            this.textMRN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveDisk = new System.Windows.Forms.Button();
            this.btnMIRC = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNucs = new System.Windows.Forms.CheckBox();
            this.checkBoxIR = new System.Windows.Forms.CheckBox();
            this.checkBoxUltrasound = new System.Windows.Forms.CheckBox();
            this.checkBoxNeuro = new System.Windows.Forms.CheckBox();
            this.checkBoxMSK = new System.Windows.Forms.CheckBox();
            this.checkBoxPeds = new System.Windows.Forms.CheckBox();
            this.checkBoxMammo = new System.Windows.Forms.CheckBox();
            this.checkBoxGU = new System.Windows.Forms.CheckBox();
            this.checkBoxGI = new System.Windows.Forms.CheckBox();
            this.checkBoxCV = new System.Windows.Forms.CheckBox();
            this.checkBoxPulm = new System.Windows.Forms.CheckBox();
            this.textHistory = new System.Windows.Forms.TextBox();
            this.textDiagnosis = new System.Windows.Forms.TextBox();
            this.textFindings = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textKeywords = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textDiscussion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textDdx = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.textLoc = new System.Windows.Forms.TextBox();
            this.saveCaseDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.openCaseDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textCaption = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.buttonReorder = new System.Windows.Forms.Button();
            this.pbContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pbContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagePanel
            // 
            this.imagePanel.AllowDrop = true;
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imagePanel.Controls.Add(this.thumbnail);
            this.imagePanel.Controls.Add(this.pb);
            this.imagePanel.Location = new System.Drawing.Point(476, 48);
            this.imagePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(574, 589);
            this.imagePanel.TabIndex = 0;
            this.imagePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragDrop);
            this.imagePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragEnter);
            this.imagePanel.DragOver += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragOver);
            this.imagePanel.DragLeave += new System.EventHandler(this.imagePanel_DragLeave);
            // 
            // thumbnail
            // 
            this.thumbnail.Location = new System.Drawing.Point(56, 45);
            this.thumbnail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(150, 77);
            this.thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumbnail.TabIndex = 1;
            this.thumbnail.TabStop = false;
            this.thumbnail.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(-3, -3);
            this.pb.Margin = new System.Windows.Forms.Padding(0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(576, 591);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.DoubleClick += new System.EventHandler(this.pb_DoubleClick);
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            // 
            // textMRN
            // 
            this.textMRN.Location = new System.Drawing.Point(78, 62);
            this.textMRN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textMRN.Name = "textMRN";
            this.textMRN.Size = new System.Drawing.Size(97, 26);
            this.textMRN.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "MRN";
            // 
            // btnSaveDisk
            // 
            this.btnSaveDisk.Image = global::CaseMaker.Properties.Resources.saveHS;
            this.btnSaveDisk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveDisk.Location = new System.Drawing.Point(56, 746);
            this.btnSaveDisk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveDisk.Name = "btnSaveDisk";
            this.btnSaveDisk.Size = new System.Drawing.Size(158, 35);
            this.btnSaveDisk.TabIndex = 6;
            this.btnSaveDisk.Text = "Save to Disk";
            this.btnSaveDisk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveDisk.UseVisualStyleBackColor = true;
            this.btnSaveDisk.Click += new System.EventHandler(this.btnSaveDisk_Click);
            // 
            // btnMIRC
            // 
            this.btnMIRC.Image = global::CaseMaker.Properties.Resources.PublishToWebHS;
            this.btnMIRC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMIRC.Location = new System.Drawing.Point(248, 746);
            this.btnMIRC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMIRC.Name = "btnMIRC";
            this.btnMIRC.Size = new System.Drawing.Size(158, 35);
            this.btnMIRC.TabIndex = 7;
            this.btnMIRC.Text = "Send to MIRC";
            this.btnMIRC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMIRC.UseVisualStyleBackColor = true;
            this.btnMIRC.Click += new System.EventHandler(this.btnMIRC_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.textHistory);
            this.tabPage1.Controls.Add(this.textDiagnosis);
            this.tabPage1.Controls.Add(this.textFindings);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(426, 578);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNucs);
            this.groupBox1.Controls.Add(this.checkBoxIR);
            this.groupBox1.Controls.Add(this.checkBoxUltrasound);
            this.groupBox1.Controls.Add(this.checkBoxNeuro);
            this.groupBox1.Controls.Add(this.checkBoxMSK);
            this.groupBox1.Controls.Add(this.checkBoxPeds);
            this.groupBox1.Controls.Add(this.checkBoxMammo);
            this.groupBox1.Controls.Add(this.checkBoxGU);
            this.groupBox1.Controls.Add(this.checkBoxGI);
            this.groupBox1.Controls.Add(this.checkBoxCV);
            this.groupBox1.Controls.Add(this.checkBoxPulm);
            this.groupBox1.Location = new System.Drawing.Point(16, 389);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(387, 162);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // checkBoxNucs
            // 
            this.checkBoxNucs.AutoSize = true;
            this.checkBoxNucs.Location = new System.Drawing.Point(146, 57);
            this.checkBoxNucs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxNucs.Name = "checkBoxNucs";
            this.checkBoxNucs.Size = new System.Drawing.Size(71, 24);
            this.checkBoxNucs.TabIndex = 5;
            this.checkBoxNucs.Text = "Nucs";
            this.checkBoxNucs.UseVisualStyleBackColor = true;
            // 
            // checkBoxIR
            // 
            this.checkBoxIR.AutoSize = true;
            this.checkBoxIR.Location = new System.Drawing.Point(146, 26);
            this.checkBoxIR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxIR.Name = "checkBoxIR";
            this.checkBoxIR.Size = new System.Drawing.Size(52, 24);
            this.checkBoxIR.TabIndex = 4;
            this.checkBoxIR.Text = "IR";
            this.checkBoxIR.UseVisualStyleBackColor = true;
            // 
            // checkBoxUltrasound
            // 
            this.checkBoxUltrasound.AutoSize = true;
            this.checkBoxUltrasound.Location = new System.Drawing.Point(146, 120);
            this.checkBoxUltrasound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxUltrasound.Name = "checkBoxUltrasound";
            this.checkBoxUltrasound.Size = new System.Drawing.Size(113, 24);
            this.checkBoxUltrasound.TabIndex = 7;
            this.checkBoxUltrasound.Text = "Ultrasound";
            this.checkBoxUltrasound.UseVisualStyleBackColor = true;
            // 
            // checkBoxNeuro
            // 
            this.checkBoxNeuro.AutoSize = true;
            this.checkBoxNeuro.Location = new System.Drawing.Point(268, 88);
            this.checkBoxNeuro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxNeuro.Name = "checkBoxNeuro";
            this.checkBoxNeuro.Size = new System.Drawing.Size(78, 24);
            this.checkBoxNeuro.TabIndex = 10;
            this.checkBoxNeuro.Text = "Neuro";
            this.checkBoxNeuro.UseVisualStyleBackColor = true;
            // 
            // checkBoxMSK
            // 
            this.checkBoxMSK.AutoSize = true;
            this.checkBoxMSK.Location = new System.Drawing.Point(268, 57);
            this.checkBoxMSK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMSK.Name = "checkBoxMSK";
            this.checkBoxMSK.Size = new System.Drawing.Size(69, 24);
            this.checkBoxMSK.TabIndex = 9;
            this.checkBoxMSK.Text = "MSK";
            this.checkBoxMSK.UseVisualStyleBackColor = true;
            // 
            // checkBoxPeds
            // 
            this.checkBoxPeds.AutoSize = true;
            this.checkBoxPeds.Location = new System.Drawing.Point(146, 88);
            this.checkBoxPeds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPeds.Name = "checkBoxPeds";
            this.checkBoxPeds.Size = new System.Drawing.Size(71, 24);
            this.checkBoxPeds.TabIndex = 6;
            this.checkBoxPeds.Text = "Peds";
            this.checkBoxPeds.UseVisualStyleBackColor = true;
            // 
            // checkBoxMammo
            // 
            this.checkBoxMammo.AutoSize = true;
            this.checkBoxMammo.Location = new System.Drawing.Point(268, 26);
            this.checkBoxMammo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMammo.Name = "checkBoxMammo";
            this.checkBoxMammo.Size = new System.Drawing.Size(92, 24);
            this.checkBoxMammo.TabIndex = 8;
            this.checkBoxMammo.Text = "Mammo";
            this.checkBoxMammo.UseVisualStyleBackColor = true;
            // 
            // checkBoxGU
            // 
            this.checkBoxGU.AutoSize = true;
            this.checkBoxGU.Location = new System.Drawing.Point(21, 120);
            this.checkBoxGU.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxGU.Name = "checkBoxGU";
            this.checkBoxGU.Size = new System.Drawing.Size(60, 24);
            this.checkBoxGU.TabIndex = 3;
            this.checkBoxGU.Text = "GU";
            this.checkBoxGU.UseVisualStyleBackColor = true;
            // 
            // checkBoxGI
            // 
            this.checkBoxGI.AutoSize = true;
            this.checkBoxGI.Location = new System.Drawing.Point(21, 88);
            this.checkBoxGI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxGI.Name = "checkBoxGI";
            this.checkBoxGI.Size = new System.Drawing.Size(53, 24);
            this.checkBoxGI.TabIndex = 2;
            this.checkBoxGI.Text = "GI";
            this.checkBoxGI.UseVisualStyleBackColor = true;
            // 
            // checkBoxCV
            // 
            this.checkBoxCV.AutoSize = true;
            this.checkBoxCV.Location = new System.Drawing.Point(21, 57);
            this.checkBoxCV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxCV.Name = "checkBoxCV";
            this.checkBoxCV.Size = new System.Drawing.Size(57, 24);
            this.checkBoxCV.TabIndex = 1;
            this.checkBoxCV.Text = "CV";
            this.checkBoxCV.UseVisualStyleBackColor = true;
            // 
            // checkBoxPulm
            // 
            this.checkBoxPulm.AutoSize = true;
            this.checkBoxPulm.Location = new System.Drawing.Point(21, 26);
            this.checkBoxPulm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPulm.Name = "checkBoxPulm";
            this.checkBoxPulm.Size = new System.Drawing.Size(109, 24);
            this.checkBoxPulm.TabIndex = 0;
            this.checkBoxPulm.Text = "Pulmonary";
            this.checkBoxPulm.UseVisualStyleBackColor = true;
            // 
            // textHistory
            // 
            this.textHistory.Location = new System.Drawing.Point(92, 14);
            this.textHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textHistory.Multiline = true;
            this.textHistory.Name = "textHistory";
            this.textHistory.Size = new System.Drawing.Size(312, 73);
            this.textHistory.TabIndex = 0;
            // 
            // textDiagnosis
            // 
            this.textDiagnosis.Location = new System.Drawing.Point(90, 318);
            this.textDiagnosis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDiagnosis.Multiline = true;
            this.textDiagnosis.Name = "textDiagnosis";
            this.textDiagnosis.Size = new System.Drawing.Size(312, 53);
            this.textDiagnosis.TabIndex = 2;
            // 
            // textFindings
            // 
            this.textFindings.Location = new System.Drawing.Point(92, 115);
            this.textFindings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textFindings.Multiline = true;
            this.textFindings.Name = "textFindings";
            this.textFindings.Size = new System.Drawing.Size(312, 178);
            this.textFindings.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "History";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 320);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Diagnosis";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Findings";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textKeywords);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.textDiscussion);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textDdx);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(426, 578);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Extra";
            // 
            // textKeywords
            // 
            this.textKeywords.Location = new System.Drawing.Point(92, 465);
            this.textKeywords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textKeywords.Multiline = true;
            this.textKeywords.Name = "textKeywords";
            this.textKeywords.Size = new System.Drawing.Size(312, 90);
            this.textKeywords.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 469);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 20);
            this.label11.TabIndex = 18;
            this.label11.Text = "Keywords";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 203);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Discussion";
            // 
            // textDiscussion
            // 
            this.textDiscussion.Location = new System.Drawing.Point(92, 198);
            this.textDiscussion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDiscussion.Multiline = true;
            this.textDiscussion.Name = "textDiscussion";
            this.textDiscussion.Size = new System.Drawing.Size(312, 236);
            this.textDiscussion.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(50, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Ddx";
            // 
            // textDdx
            // 
            this.textDdx.Location = new System.Drawing.Point(92, 23);
            this.textDdx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textDdx.Multiline = true;
            this.textDdx.Name = "textDdx";
            this.textDdx.Size = new System.Drawing.Size(312, 144);
            this.textDdx.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(10, 122);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 611);
            this.tabControl1.TabIndex = 5;
            // 
            // textLoc
            // 
            this.textLoc.Location = new System.Drawing.Point(243, 62);
            this.textLoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textLoc.Name = "textLoc";
            this.textLoc.Size = new System.Drawing.Size(194, 26);
            this.textLoc.TabIndex = 4;
            // 
            // saveCaseDialog
            // 
            this.saveCaseDialog.DefaultExt = "zip";
            this.saveCaseDialog.Filter = "ZIP files (*.zip)|*.zip";
            this.saveCaseDialog.Title = "Select the file you want to save";
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(594, 705);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(112, 35);
            this.btnLeft.TabIndex = 14;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRight.Location = new System.Drawing.Point(826, 705);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(112, 35);
            this.btnRight.TabIndex = 15;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // countLabel
            // 
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(746, 712);
            this.countLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(39, 20);
            this.countLabel.TabIndex = 23;
            this.countLabel.Text = "0 / 0";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Image = global::CaseMaker.Properties.Resources.Delete;
            this.btnDelete.Location = new System.Drawing.Point(710, 749);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 35);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // openCaseDialog
            // 
            this.openCaseDialog.DefaultExt = "zip";
            this.openCaseDialog.Filter = "ZIP files|*.zip|XML files|*.xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseToolStripMenuItem,
            this.openCaseToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.previewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1070, 35);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newCaseToolStripMenuItem
            // 
            this.newCaseToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.NewDocumentHS;
            this.newCaseToolStripMenuItem.Name = "newCaseToolStripMenuItem";
            this.newCaseToolStripMenuItem.Size = new System.Drawing.Size(129, 29);
            this.newCaseToolStripMenuItem.Text = "New Case";
            this.newCaseToolStripMenuItem.Click += new System.EventHandler(this.newCaseToolStripMenuItem_Click);
            // 
            // openCaseToolStripMenuItem
            // 
            this.openCaseToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.OpenSelectedItemHS;
            this.openCaseToolStripMenuItem.Name = "openCaseToolStripMenuItem";
            this.openCaseToolStripMenuItem.Size = new System.Drawing.Size(138, 29);
            this.openCaseToolStripMenuItem.Text = "Open Case";
            this.openCaseToolStripMenuItem.Click += new System.EventHandler(this.openCaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.Help;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.PrintPreviewHS;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(112, 29);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // textCaption
            // 
            this.textCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textCaption.Enabled = false;
            this.textCaption.Location = new System.Drawing.Point(476, 649);
            this.textCaption.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textCaption.Multiline = true;
            this.textCaption.Name = "textCaption";
            this.textCaption.Size = new System.Drawing.Size(574, 35);
            this.textCaption.TabIndex = 8;
            this.textCaption.TextChanged += new System.EventHandler(this.textCaption_TextChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 809);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1070, 22);
            this.statusStrip.TabIndex = 28;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(1047, 15);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 22);
            this.toolStripProgressBar.Visible = false;
            // 
            // buttonReorder
            // 
            this.buttonReorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReorder.Image = global::CaseMaker.Properties.Resources.ThumbnailView;
            this.buttonReorder.Location = new System.Drawing.Point(1004, 705);
            this.buttonReorder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonReorder.Name = "buttonReorder";
            this.buttonReorder.Size = new System.Drawing.Size(48, 48);
            this.buttonReorder.TabIndex = 29;
            this.buttonReorder.UseVisualStyleBackColor = true;
            this.buttonReorder.Click += new System.EventHandler(this.buttonReorder_Click);
            // 
            // pbContextMenuStrip
            // 
            this.pbContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.pbContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.pbContextMenuStrip.Name = "pbContextMenuStrip";
            this.pbContextMenuStrip.Size = new System.Drawing.Size(229, 68);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(228, 32);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(228, 32);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(196, 66);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Loc";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 831);
            this.Controls.Add(this.buttonReorder);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.textCaption);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnMIRC);
            this.Controls.Add(this.btnSaveDisk);
            this.Controls.Add(this.textLoc);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.textMRN);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1070, 842);
            this.Name = "MainForm";
            this.Text = "CaseMaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pbContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TextBox textMRN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveDisk;
        private System.Windows.Forms.Button btnMIRC;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textHistory;
        private System.Windows.Forms.TextBox textDiagnosis;
        private System.Windows.Forms.TextBox textFindings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox textLoc;
        private System.Windows.Forms.SaveFileDialog saveCaseDialog;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textDiscussion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textDdx;
        private System.Windows.Forms.OpenFileDialog openCaseDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox textCaption;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNucs;
        private System.Windows.Forms.CheckBox checkBoxIR;
        private System.Windows.Forms.CheckBox checkBoxUltrasound;
        private System.Windows.Forms.CheckBox checkBoxNeuro;
        private System.Windows.Forms.CheckBox checkBoxMSK;
        private System.Windows.Forms.CheckBox checkBoxPeds;
        private System.Windows.Forms.CheckBox checkBoxMammo;
        private System.Windows.Forms.CheckBox checkBoxGU;
        private System.Windows.Forms.CheckBox checkBoxGI;
        private System.Windows.Forms.CheckBox checkBoxCV;
        private System.Windows.Forms.CheckBox checkBoxPulm;
        private System.Windows.Forms.TextBox textKeywords;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonReorder;
        private System.Windows.Forms.PictureBox thumbnail;
        private System.Windows.Forms.ContextMenuStrip pbContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.Label label8;
    }
}

