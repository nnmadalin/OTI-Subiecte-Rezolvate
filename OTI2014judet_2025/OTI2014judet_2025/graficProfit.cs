using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014judet_2025
{
    public partial class graficProfit : Form
    {
        public graficProfit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        Bitmap bit = new Bitmap(988, 541);
        Point[] points = new Point[1000];
        int k = 12;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k == 985)
            {
                k = 11;
                points = new Point[1000];
            }
            points[k] = new Point(k, actiunileMele.suma / 100);
            k++;

            bitInitial();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(Form1.intervalOpen == 1)
            {
                timer1.Interval = Form1.interval;
                timer1.Start();
            }
            else
            {
                timer1.Interval = Form1.interval;
                timer1.Stop();
            }
        }

        void bitInitial()
        {
            bit = new Bitmap(988, 541);
            Graphics g = Graphics.FromImage(bit);
            g.DrawLine(Pens.Blue, new Point(0, 400), new Point(988, 400));
            g.DrawLine(Pens.Blue, new Point(10, 541), new Point(10, 0));

            g.DrawString("Timp", new Font(FontFamily.GenericSerif, 15, FontStyle.Regular), Brushes.Green, new Point(930, 400));
            g.DrawString("Valoare", new Font(FontFamily.GenericSerif, 15, FontStyle.Regular), Brushes.Green, new Point(10, 10));

            if (Form1.intervalOpen == 1)
            {

                Point[] newPoints = new Point[k - 11 + 2];
                
                for (int i = 11; i <= k; i++)
                {
                    newPoints[i - 11 + 1] = points[i];
                }
                
                g.DrawLines(Pens.Red, newPoints);

            }

            pictureBox1.Image = bit;
        }

        private void graficProfit_Load(object sender, EventArgs e)
        {
            bitInitial();
        }
    }
}
