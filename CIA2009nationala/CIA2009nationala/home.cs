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
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void prelucrareSiruriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new prelucrare_siruri();
            frm.Show();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rotireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new rotire();
            frm.Show();
        }

        private void bazaDeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new db();
            frm.Show();
        }
    }
}
