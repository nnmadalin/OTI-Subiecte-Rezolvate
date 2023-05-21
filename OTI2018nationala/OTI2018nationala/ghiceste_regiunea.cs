using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OTI2018nationala
{
    public partial class ghiceste_regiunea : Form
    {
        public ghiceste_regiunea()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.Location = this.Location + (Size)e.Location - (Size)mouse;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        void painte(string x)
        {
            Bitmap bit = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(bit);
            Pen pens = new Pen(Brushes.White, 3);
            using (StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/Harti/" + x +".txt"))
            {
                string row;
                Point[] pt = new Point[100];
                int k = 0;
                bool ok = false;
                while ((row = read.ReadLine()) != null)
                {
                    string[] split = row.Split('*');
                    if (ok == false)
                    {
                        ok = true;
                        TextBox text = new TextBox()
                        {
                            Tag = x,
                            Width = 70,
                            Height = 25,
                            Location = new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]))
                        };
                        pictureBox1.Controls.Add(text);
                    }
                    else
                        pt[k++] = new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));

                }
                Point[] pt2 = new Point[k];
                for (int i = 0; i < k; i++)
                    pt2[i] = pt[i];

                g.DrawLines(pens, pt2);
            };
            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            painte("Banat");
            painte("Basarabia");
            painte("Bucovina");
            painte("Crisana");
            painte("Dobrogea");
            painte("Maramures");
            painte("Moldova");
            painte("Muntenia");
            painte("Oltenia");
            painte("Transilvania");

            button3.Visible = false;
            button4.Visible = true;

        }

        private void ghiceste_regiunea_Load(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(581, 369);
            Graphics g = Graphics.FromImage(bit);
            using (StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/Harti/RomaniaMare.txt"))
            {
                string row;
                Point[] pt = new Point[76];
                int k = 0;
                while ((row = read.ReadLine()) != null)
                {
                    string[] split = row.Split('*');

                    pt[k++] = new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));

                }
                Pen pens = new Pen(Brushes.DarkGreen, 3);
                g.DrawLines(pens, pt);
            };
            pictureBox1.Image = bit;
        }
        public static int nota = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            button5.Visible = true;

            int k = 0;
            foreach(Control ctrl in pictureBox1.Controls)
            {
                ctrl.Enabled = false;
                if (ctrl.Text == ctrl.Tag.ToString())
                {
                    k++;
                }
            }
            nota = k;
            label2.Text = "Nota: " + k.ToString();
        }
    }
}
