using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace CIA2010judet
{
    public partial class trigonometrie : Form
    {
        public trigonometrie()
        {
            InitializeComponent();
        }

        private void trigonometrie_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            var form = new home();
            form.Show();
            this.Hide();
        }

        Bitmap bit = new Bitmap(802, 404);

        void make_paint(string str)
        {

            Graphics graphics = Graphics.FromImage(bit);
            Pen black = new Pen(Color.Black, 2);
            Pen red = new Pen(Color.Red, 3);
            Pen blue = new Pen(Color.Blue, 3);

            graphics.DrawLine(black, 401, 0 + 10, 401, 404 - 10);
            graphics.FillPolygon(Brushes.Black, new Point[] { new Point(401 - 10, 25), new Point(401 + 10, 25), new Point(401, 5) });
            graphics.FillPolygon(Brushes.Black, new Point[] { new Point(401 - 10, 404 - 25), new Point(401 + 10, 404 - 25), new Point(401, 399) });


            graphics.DrawLine(black, 0 + 10, 202, 802 - 10, 202);
            graphics.FillPolygon(Brushes.Black, new Point[] { new Point(5, 202), new Point(25, 202 + 10), new Point(25, 202 - 10) });
            graphics.FillPolygon(Brushes.Black, new Point[] { new Point(797, 202), new Point(777, 202 + 10), new Point(777, 202 - 10) });



            float x = 401, y = 202;
            bool dir = true;

            if (str == "sin")
            {
                int cx = 700, cy = 300;
                PointF[] aptf = new PointF[cx];

                for (int i = 0; i < cx; i++)
                {
                    aptf[i].X = i + 50;
                    aptf[i].Y = cy / 2 * (1 - (float)Math.Sin(i * 2 * Math.PI / (cx - 1))) + 50;
                }
                graphics.DrawLines(red, aptf);

                //graphics.DrawCurve;
            }
            else if (str == "cos")
            {
                int cx = 700, cy = 300;
                PointF[] aptf = new PointF[cx];

                for (int i = 0; i < cx; i++)
                {
                    aptf[i].X = i + 50;
                    aptf[i].Y = cy / 2 * (1 - (float)Math.Cos(i * 2 * Math.PI / (cx - 1))) + 50;
                }
                graphics.DrawLines(blue, aptf);
            }
            else
            {
                bit = new Bitmap(802, 404);
                graphics = Graphics.FromImage(bit);
                graphics.DrawLine(black, 401, 0 + 10, 401, 404 - 10);
                graphics.FillPolygon(Brushes.Black, new Point[] { new Point(401 - 10, 25), new Point(401 + 10, 25), new Point(401, 5) });
                graphics.FillPolygon(Brushes.Black, new Point[] { new Point(401 - 10, 404 - 25), new Point(401 + 10, 404 - 25), new Point(401, 399) });


                graphics.DrawLine(black, 0 + 10, 202, 802 - 10, 202);
                graphics.FillPolygon(Brushes.Black, new Point[] { new Point(5, 202), new Point(25, 202 + 10), new Point(25, 202 - 10) });
                graphics.FillPolygon(Brushes.Black, new Point[] { new Point(797, 202), new Point(777, 202 + 10), new Point(777, 202 - 10) });
            }

        

    }

        private void trigonometrie_Load(object sender, EventArgs e)
        {
            make_paint("null");
            pictureBox1.Image = bit;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            make_paint("null");
            pictureBox1.Image = bit;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            make_paint("sin");
            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            make_paint("cos");
            pictureBox1.Image = bit;
        }
    }
}
