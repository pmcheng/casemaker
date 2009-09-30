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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imagePanel = new System.Windows.Forms.Panel();
            this.thumbnail = new System.Windows.Forms.PictureBox();
            this.pb = new System.Windows.Forms.PictureBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.textMRN = new System.Windows.Forms.TextBox();
            this.textDOB = new System.Windows.Forms.TextBox();
            this.textGender = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnXML = new System.Windows.Forms.Button();
            this.btnMIRC = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textHistory = new System.Windows.Forms.TextBox();
            this.textDiagnosis = new System.Windows.Forms.TextBox();
            this.textFindings = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textDiscussion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textDdx = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.textLoc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.saveXMLDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.openXMLDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textCaption = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textKeywords = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textDebug = new System.Windows.Forms.TextBox();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.imagePanel.Location = new System.Drawing.Point(317, 31);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(384, 384);
            this.imagePanel.TabIndex = 0;
            this.imagePanel.DragOver += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragOver);
            this.imagePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragDrop);
            this.imagePanel.Resize += new System.EventHandler(this.imagePanel_Resize);
            this.imagePanel.DragLeave += new System.EventHandler(this.imagePanel_DragLeave);
            this.imagePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagePanel_DragEnter);
            // 
            // thumbnail
            // 
            this.thumbnail.Location = new System.Drawing.Point(13, 12);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(100, 50);
            this.thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thumbnail.TabIndex = 1;
            this.thumbnail.TabStop = false;
            this.thumbnail.Visible = false;
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(-2, -2);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(304, 316);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.DoubleClick += new System.EventHandler(this.pb_DoubleClick);
            this.pb.Click += new System.EventHandler(this.pb_Click);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(63, 32);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(209, 20);
            this.textName.TabIndex = 4;
            // 
            // textMRN
            // 
            this.textMRN.Location = new System.Drawing.Point(176, 59);
            this.textMRN.Name = "textMRN";
            this.textMRN.Size = new System.Drawing.Size(96, 20);
            this.textMRN.TabIndex = 6;
            // 
            // textDOB
            // 
            this.textDOB.Location = new System.Drawing.Point(63, 59);
            this.textDOB.Name = "textDOB";
            this.textDOB.Size = new System.Drawing.Size(69, 20);
            this.textDOB.TabIndex = 5;
            // 
            // textGender
            // 
            this.textGender.Location = new System.Drawing.Point(63, 88);
            this.textGender.MaxLength = 2;
            this.textGender.Name = "textGender";
            this.textGender.Size = new System.Drawing.Size(23, 20);
            this.textGender.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "MRN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "DOB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gender";
            // 
            // btnXML
            // 
            this.btnXML.Image = global::CaseMaker.Properties.Resources.saveHS;
            this.btnXML.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXML.Location = new System.Drawing.Point(41, 465);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(105, 23);
            this.btnXML.TabIndex = 10;
            this.btnXML.Text = "Save to Disk";
            this.btnXML.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // btnMIRC
            // 
            this.btnMIRC.Image = global::CaseMaker.Properties.Resources.PublishToWebHS;
            this.btnMIRC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMIRC.Location = new System.Drawing.Point(167, 465);
            this.btnMIRC.Name = "btnMIRC";
            this.btnMIRC.Size = new System.Drawing.Size(105, 23);
            this.btnMIRC.TabIndex = 11;
            this.btnMIRC.Text = "Send to MIRC";
            this.btnMIRC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMIRC.UseVisualStyleBackColor = true;
            this.btnMIRC.Click += new System.EventHandler(this.btnMIRC_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textKeywords);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.textHistory);
            this.tabPage1.Controls.Add(this.textDiagnosis);
            this.tabPage1.Controls.Add(this.textFindings);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(283, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textHistory
            // 
            this.textHistory.Location = new System.Drawing.Point(61, 9);
            this.textHistory.Multiline = true;
            this.textHistory.Name = "textHistory";
            this.textHistory.Size = new System.Drawing.Size(209, 55);
            this.textHistory.TabIndex = 1;
            // 
            // textDiagnosis
            // 
            this.textDiagnosis.Location = new System.Drawing.Point(61, 199);
            this.textDiagnosis.Multiline = true;
            this.textDiagnosis.Name = "textDiagnosis";
            this.textDiagnosis.Size = new System.Drawing.Size(209, 36);
            this.textDiagnosis.TabIndex = 3;
            // 
            // textFindings
            // 
            this.textFindings.Location = new System.Drawing.Point(61, 78);
            this.textFindings.Multiline = true;
            this.textFindings.Name = "textFindings";
            this.textFindings.Size = new System.Drawing.Size(209, 106);
            this.textFindings.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "History";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Diagnosis";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Findings";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textDebug);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.textDiscussion);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textDdx);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 308);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Extra";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Discussion";
            // 
            // textDiscussion
            // 
            this.textDiscussion.Location = new System.Drawing.Point(61, 80);
            this.textDiscussion.Multiline = true;
            this.textDiscussion.Name = "textDiscussion";
            this.textDiscussion.Size = new System.Drawing.Size(209, 104);
            this.textDiscussion.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Ddx";
            // 
            // textDdx
            // 
            this.textDdx.Location = new System.Drawing.Point(61, 15);
            this.textDdx.Multiline = true;
            this.textDdx.Name = "textDdx";
            this.textDdx.Size = new System.Drawing.Size(209, 42);
            this.textDdx.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 114);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 334);
            this.tabControl1.TabIndex = 9;
            // 
            // textLoc
            // 
            this.textLoc.Location = new System.Drawing.Point(141, 88);
            this.textLoc.Name = "textLoc";
            this.textLoc.Size = new System.Drawing.Size(131, 20);
            this.textLoc.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Loc";
            // 
            // saveXMLDialog
            // 
            this.saveXMLDialog.DefaultExt = "xml";
            this.saveXMLDialog.Filter = "XML files (*.xml)|*.xml";
            this.saveXMLDialog.Title = "Select the file you want to save";
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLeft.Location = new System.Drawing.Point(396, 460);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 14;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRight.Location = new System.Drawing.Point(551, 460);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 15;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // countLabel
            // 
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(497, 465);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(30, 13);
            this.countLabel.TabIndex = 23;
            this.countLabel.Text = "0 / 0";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Image = global::CaseMaker.Properties.Resources.Delete;
            this.btnDelete.Location = new System.Drawing.Point(473, 489);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // openXMLDialog
            // 
            this.openXMLDialog.DefaultExt = "xml";
            this.openXMLDialog.Filter = "XML files|*.xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseToolStripMenuItem,
            this.openCaseToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(713, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newCaseToolStripMenuItem
            // 
            this.newCaseToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.NewDocumentHS;
            this.newCaseToolStripMenuItem.Name = "newCaseToolStripMenuItem";
            this.newCaseToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.newCaseToolStripMenuItem.Text = "New Case";
            this.newCaseToolStripMenuItem.Click += new System.EventHandler(this.newCaseToolStripMenuItem_Click);
            // 
            // openCaseToolStripMenuItem
            // 
            this.openCaseToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.OpenSelectedItemHS;
            this.openCaseToolStripMenuItem.Name = "openCaseToolStripMenuItem";
            this.openCaseToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.openCaseToolStripMenuItem.Text = "Open Case";
            this.openCaseToolStripMenuItem.Click += new System.EventHandler(this.openCaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Image = global::CaseMaker.Properties.Resources.Help;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // textCaption
            // 
            this.textCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textCaption.Enabled = false;
            this.textCaption.Location = new System.Drawing.Point(317, 424);
            this.textCaption.Multiline = true;
            this.textCaption.Name = "textCaption";
            this.textCaption.Size = new System.Drawing.Size(384, 24);
            this.textCaption.TabIndex = 27;
            this.textCaption.TextChanged += new System.EventHandler(this.textCaption_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Keywords";
            // 
            // textKeywords
            // 
            this.textKeywords.Location = new System.Drawing.Point(61, 256);
            this.textKeywords.Multiline = true;
            this.textKeywords.Name = "textKeywords";
            this.textKeywords.Size = new System.Drawing.Size(209, 34);
            this.textKeywords.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 198);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Debugging Output (for Phil)";
            // 
            // textDebug
            // 
            this.textDebug.Location = new System.Drawing.Point(12, 214);
            this.textDebug.Multiline = true;
            this.textDebug.Name = "textDebug";
            this.textDebug.Size = new System.Drawing.Size(258, 78);
            this.textDebug.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 527);
            this.Controls.Add(this.textCaption);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textLoc);
            this.Controls.Add(this.btnMIRC);
            this.Controls.Add(this.btnXML);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textGender);
            this.Controls.Add(this.textDOB);
            this.Controls.Add(this.textMRN);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(721, 554);
            this.Name = "MainForm";
            this.Text = "CaseMaker";
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.PictureBox thumbnail;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textMRN;
        private System.Windows.Forms.TextBox textDOB;
        private System.Windows.Forms.TextBox textGender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnXML;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog saveXMLDialog;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textDiscussion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textDdx;
        private System.Windows.Forms.OpenFileDialog openXMLDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox textCaption;
        private System.Windows.Forms.TextBox textKeywords;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textDebug;
        private System.Windows.Forms.Label label12;
    }
}

