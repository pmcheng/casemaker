using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaseMaker
{
    public partial class DialogReorder : Form
    {
        private int minSquareSize = 100;
        private int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;

        public List<PictureBox> pbList = new List<PictureBox>();
        public List<CaseImage> caseImages = new List<CaseImage>();


        public DialogReorder()
        {
            InitializeComponent();
        }

        public void updateOrder()
        {
            List<CaseImage> newList = new List<CaseImage>();
            foreach (Control c in flp.Controls)
            {
                for (int i = 0; i < pbList.Count; i++)
                {
                    if (pbList[i] == c)
                    {
                        newList.Add(caseImages[i]);
                        break;
                    }
                }
            }
            caseImages = newList;
        }


        public void populate(List<CaseImage> caseImages)
        {
            this.caseImages = caseImages;
            int squareSize=maxSquareSize(caseImages.Count,minSquareSize,flp.Width-scrollBarWidth,flp.Height-scrollBarWidth);

            foreach (CaseImage caseImage in caseImages)
            {
                PictureBox pb = new PictureBox();
                pbList.Add(pb);

                pb.SizeMode = PictureBoxSizeMode.Zoom;
                               
                pb.Size = new Size(squareSize, squareSize);
                pb.Image = caseImage.image;
                
                pb.MouseDown += new MouseEventHandler(pb_MouseDown);
                pb.DragEnter += new DragEventHandler(pb_DragEnter);
                pb.AllowDrop = true;
                flp.Controls.Add(pb);

            }
        }

        public int maxSquareSize(int numSquares, int minSize, int width, int height)
        {
            int h = 1;
            int w = 1;
            int size = minSize;
            while (numSquares > h * w)
            {
                if ((height / (h + 1)) >= (width / (w + 1))) h++;
                else w++;
                size = Math.Min(width/w, height/h);
                if (size < minSize)
                {
                    size = minSize;
                    break;
                }
            }
            return size;
        }

        void pb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(PictureBox)) != null)
            {
                int myIndex = flp.Controls.GetChildIndex((sender as PictureBox));

                //Dragged to control to location of next picturebox
                PictureBox q = (PictureBox)e.Data.GetData(typeof(PictureBox));
                flp.Controls.SetChildIndex(q, myIndex);
                e.Effect = DragDropEffects.Move;
            }
        }

        void pb_MouseDown(object sender, MouseEventArgs e)
        {
            //pbSelected = (sender as PictureBox);
            //pbSelected.BorderStyle = BorderStyle.Fixed3D;
            DoDragDrop(sender, DragDropEffects.All);
        }


        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            int squareSize = maxSquareSize(caseImages.Count, minSquareSize, flp.Width - scrollBarWidth, flp.Height - scrollBarWidth);
            
            foreach (PictureBox pb in pbList)
            {
                pb.Size = new Size(squareSize,squareSize);
            }

        }

        private void DialogReorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateOrder();
        }
    }
}
