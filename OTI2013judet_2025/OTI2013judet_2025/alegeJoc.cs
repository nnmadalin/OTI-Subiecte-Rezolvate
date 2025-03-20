using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2013judet_2025
{
    public partial class alegeJoc : Form
    {

        public static string locationImage = "", tipPatrat = "4", numeJucator = "";
        public alegeJoc()
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

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tipPatrat = "9";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tipPatrat = "4";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(locationImage == "")
            {
                MessageBox.Show("Nu ai ales o imagine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(numeJucator == "")
            {
                MessageBox.Show("Nu ai ales un nume!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                joc frm = new joc();
                frm.Show();
                this.Hide();
            }
        }

        private void alegeJoc_Load(object sender, EventArgs e)
        {
            numeJucator = "";
            tipPatrat = "4";
            locationImage = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            numeJucator = textBox1.Text.Trim();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "/Resurse/Img/";
            openFileDialog1.DefaultExt = "Images (*.png, *.jpg, *jpeg) | *.png;*.jpg;*.jpeg";
            var dialog = openFileDialog1.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                locationImage = openFileDialog1.FileName;

                label4.Text = "Fisier ales: " + locationImage;
            }
            
        }
    }
}
