using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;

namespace CaseMaker
{
    public partial class UploadMIRCDialog : Form
    {
        private bool boolPrivateCase;
        public bool privateCase
        {
            get
            { return boolPrivateCase; }
            set
            {
                boolPrivateCase = value;
                checkBoxAccess.Checked = value;
            }
        }

        private string strURLMIRC;
        public string urlMIRC
        {
            get
            { return strURLMIRC; }
            set
            {
                strURLMIRC = value;
                comboBoxURL.Text = value;
            }
        }

        private string strUsername;
        public string username
        {
            get
            { return strUsername; }
            set
            {
                strUsername = value;
                textBoxUsername.Text = value;
            }
        }

        private string strPassword;
        public string password
        {
            get
            {
                return strPassword;
            }
            set
            {
                strPassword = value;
                textBoxPassword.Text = value;
            }
        }

        public UploadMIRCDialog()
        {
        }

        public UploadMIRCDialog(List<string> urlList)
        {
            InitializeComponent();
            foreach (string url in urlList)
            {
                comboBoxURL.Items.Add(url);
            }
            //comboBoxURL.Items.Add(@"http://10.131.12.41:8080/submit/ss1");
            //comboBoxURL.Items.Add(@"http://192.168.0.101:8080/submit/ss1");
            //comboBoxURL.Items.Add(@"http://mirc.usc.edu/USCRadStorage/submit/doc");
            comboBoxURL.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            urlMIRC = comboBoxURL.Text;
            username = textBoxUsername.Text;
            password = textBoxPassword.Text;
            privateCase = checkBoxAccess.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }

    }
}
