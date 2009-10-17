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
        int minSquareSize = 100;

        int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        public List<PictureBox> pbs = new List<PictureBox>();

        public List<CaseImage> caseImages = new List<CaseImage>();
        public List<CaseImage> newList;

        public DialogReorder()
        {
            InitializeComponent();
        }

        public void updateOrder()
        {
            newList = new List<CaseImage>();
            foreach (Control c in flp.Controls)
            {
                for (int i = 0; i < pbs.Count; i++)
                {
                    if (pbs[i] == c)
                    {
                        newList.Add(caseImages[i]);
                        break;
                    }
                }
            }
        }


        public void populate(List<CaseImage> caseImages)
        {
            this.caseImages = caseImages;
            this.newList = caseImages;
            int squareSize=maxSquareSize(caseImages.Count,minSquareSize,flp.Width-scrollBarWidth,flp.Height-scrollBarWidth);

            foreach (CaseImage caseImage in caseImages)
            {
                PictureBox pb = new PictureBox();
                pbs.Add(pb);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
               
                pb.Size = new Size(squareSize - flp.Margin.Left - flp.Margin.Right, squareSize - flp.Margin.Top - flp.Margin.Bottom);
                pb.Image = caseImage.image;
                pb.MouseDown += new MouseEventHandler(pbox_MouseDown);
                pb.DragEnter += new DragEventHandler(pbox_DragEnter);
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

        void pbox_DragEnter(object sender, DragEventArgs e)
        {
            base.OnDragEnter(e);
            
            if (e.Data.GetData(typeof(PictureBox)) != null)
            {
                FlowLayoutPanel flp = (FlowLayoutPanel)(sender as PictureBox).Parent;
                //Current Position             
                int myIndex = flp.Controls.GetChildIndex((sender as PictureBox));

                //Dragged to control to location of next picturebox
                PictureBox q = (PictureBox)e.Data.GetData(typeof(PictureBox));
                flp.Controls.SetChildIndex(q, myIndex);
                updateOrder();
                e.Effect = DragDropEffects.Move;
            }
        }


        void pbox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //(sender as PictureBox).BorderStyle = BorderStyle.Fixed3D;
            DoDragDrop(sender, DragDropEffects.All);
        }

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            int squareSize = maxSquareSize(caseImages.Count, minSquareSize, flp.Width - scrollBarWidth, flp.Height - scrollBarWidth);
            
            foreach (PictureBox pb in pbs)
            {
                pb.Size = new Size(squareSize - flp.Margin.Left - flp.Margin.Right, squareSize - flp.Margin.Top - flp.Margin.Bottom);
            }

        }

    }
}
