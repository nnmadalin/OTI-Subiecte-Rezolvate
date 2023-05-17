using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014nationala
{
    public partial class Form1 : Form
    {
        public Form1()
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
        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Location = this.Location + (Size)e.Location - (Size)_mouse;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "medic" && textBox2.Text == "oti2014")
            {
                var frm = new medic();
                frm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Ceva nu e bine!");
        }
    }
}
