using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIA2012judet
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void inchidereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lansareTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "candidat" && textBox4.Text == "cia2012")
            {
                this.Hide();
                var test = new test();
                test.Show();
            }
            else
            {
                MessageBox.Show("Nume utilizator sau parolă gresită!! Vă rugăm reluati!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
