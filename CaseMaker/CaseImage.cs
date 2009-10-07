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
        public string imageURL;
        public string studyUID;
        public string imageUID;
        public string filename;

        public CaseImage(Image image)
        {
            this.image = image;
            this.imageURL = string.Empty;
        }
    }
}
