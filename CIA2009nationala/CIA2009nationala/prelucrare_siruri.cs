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
    public partial class prelucrare_siruri : Form
    {
        public prelucrare_siruri()
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
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void prelucrare_siruri_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            string[] split = textBox1.Text.Split(' ');
            int k = 0;
            for(int i = 0; i < split.Length; i++)
            {
                if(split[i].Trim() != "")
                {
                    textBox2.Text += (split[i] + "\r\n");
                    k++;
                }
            }
            label4.Text = "Numar cuvinte: " + k;
        }
    }
}
