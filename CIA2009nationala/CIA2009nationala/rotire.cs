using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIA2009nationala
{
    public partial class rotire : Form
    {
        public rotire()
        {
            InitializeComponent();
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
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        Bitmap bit;
        private void rotire_Load(object sender, EventArgs e)
        {
            bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bit);

            Pen red = new Pen(Brushes.White, 2);
            g.DrawLine(red, new Point(bit.Width / 2, bit.Height / 2), new Point(bit.Width, bit.Height / 2));

            pictureBox1.Image = bit;
        }

        int angle = 0;

        Bitmap rotate()
        {
            Bitmap temp = new Bitmap(bit.Width, bit.Width);
            temp.SetResolution(bit.HorizontalResolution, bit.VerticalResolution);

            Graphics g = Graphics.FromImage(temp);
            g.TranslateTransform(bit.Width / 2, bit.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-bit.Width / 2, -bit.Height / 2);
            g.DrawImage(bit, 0, 0);
            return temp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (angle >= 360)
                angle = 0;
            pictureBox1.Image = rotate();
            angle+= 2;
        }
    }
}
