using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaseMaker
{
    public partial class UploadMIRCDialog : Form
    {
        private string strURLMIRC;
        public string urlMIRC
        {
            get
            { return strURLMIRC; }
            set
            {
                strURLMIRC = value;
                textBoxURL.Text = value;
            }
        }

        private string strUsername;
        public string username {
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
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            urlMIRC = textBoxURL.Text;
            username = textBoxUsername.Text;
            password = textBoxPassword.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
        }
    }
}
