using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014judet_2025
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
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            numericUpDown1.Value = 500;

            actiunileMele frm = new actiunileMele();
            frm.Show();

            graficProfit frm2 = new graficProfit();
            frm2.Show();

            frm.Hide();
            frm2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;

            timer1.Start();
            intervalOpen = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = false;

            timer1.Stop();
            intervalOpen = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
            interval = Convert.ToInt32(numericUpDown1.Value);
        }

        public static int[] randomVal = {0, 0, 0, 0, 0};
        public static int interval = 500;
        public static int intervalOpen = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();

            for(int i = 0; i < 5; i++)
            {
                randomVal[i] = Convert.ToInt32(rand.Next(-5, 5));
            }
        }

        private void actiunileMeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actiunileMele frm = new actiunileMele();
            frm.Show();
        }

        private void graficProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graficProfit frm = new graficProfit();
            frm.Show();
        }
    }
}
