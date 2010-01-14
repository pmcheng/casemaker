using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaseMaker
{
    public partial class PictureBoxProperties : Form
    {
        public PictureBoxProperties()
        {
            InitializeComponent();
        }
        public void AddProp(string property, string value) 
        {
            textBox.Text += property + ": " + value + "\r\n\r\n";
        }
    }
}
