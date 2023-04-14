using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014judet
{
    public partial class grafic : Form
    {
        public grafic()
        {
            InitializeComponent();
        }
        Bitmap bit = new Bitmap(1071, 654);
        

        void load_grafic()
        {
            bit = new Bitmap(1071, 654);
            Graphics g = Graphics.FromImage(bit);
            Pen red = new Pen(Color.Red, 2);
            Pen blue = new Pen(Color.Blue, 3);

            g.DrawLine(blue, 5, 5, 5, 654);
            g.DrawLine(blue, 0, 500, 1071 - 5, 500);
            g.FillPolygon(Brushes.Blue, new Point[] { new Point(0, 15), new Point(10, 15), new Point(5, 0) });
            g.FillPolygon(Brushes.Blue, new Point[] { new Point(1071 - 15, 500 - 10), new Point(1071 - 15, 500 + 10), new Point(1071, 500) });
            Font font = new Font(new FontFamily("Arial"), 13, FontStyle.Regular);
            g.DrawString("Valoare", font, Brushes.Black, new Point(10, 5));
            g.DrawString("Timp", font, Brushes.Black, new Point(1071 - 70, 500));

            int x = 5;
            Point[] points = new Point[actiunile_mele.lung+1];
            points[0] = new Point(x, 500);
            x += 5;
            for (int i = 0; i < actiunile_mele.lung; i++)
            {
                points[i + 1] = new Point(x, 500 - actiunile_mele.val_time[i]);
                x += 5;
            }


            if (actiunile_mele.lung != 0)
                g.DrawLines(red, points);

            pictureBox1.Image = bit;
        }

        private void grafic_Load(object sender, EventArgs e)
        {
            load_grafic();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            load_grafic();
            
        }

        private void actiunile_mele_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(actiunile_mele.lung);
            if (home.enable == true)
            {
                timer1.Interval = home.time;
                if (timer1.Enabled == false)
                    timer1.Start();
            }
            else
                timer1.Stop();
        }
    }
}
