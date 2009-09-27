using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace CaseMaker
{
    public class CaseImage
    {
        public Image image;
        public string caption;
        public string url;
        public string filename;

        public CaseImage(Image image)
        {
            this.image = image;
            this.url = string.Empty;
        }
    }
}
