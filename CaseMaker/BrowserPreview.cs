using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaseMaker
{
    public partial class BrowserPreview : Form
    {
        public BrowserPreview()
        {
            InitializeComponent();
        }

        public void Navigate(string URL)
        {
            webBrowser.Navigate(URL);
        }
    }
}
