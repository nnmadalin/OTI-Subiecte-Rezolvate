using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cia2009judet
{
    public partial class rotire : Form
    {
        public rotire()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new home();
            form.Show();
        }

        Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        int k = 0, p = 0;
        Bitmap img;
        private void rotire_Load(object sender, EventArgs e)
        {
            img = new Bitmap(pictureBox1.Image);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = RotateImage(img, k);
            if (p < 2)
                k++;
            else
                timer1.Stop();
            if (k > 360)
            {
                k = 0; p++;
                
            }
            GC.Collect();
        }
    }
}
