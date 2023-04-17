using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2013judet
{
    public partial class joc_nou : Form
    {
        public joc_nou()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string img_file = "";
        public static string number_img = "4";

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void joc_nou_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(img_file == "")
            {
                MessageBox.Show("Nu ai ales imagine", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var form = new joc();
                form.Show();
                this.Hide();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            number_img = "4";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            number_img = "9";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath + "/Resurse/Img";
            openFileDialog1.FileName = "";

            DialogResult dialog = openFileDialog1.ShowDialog();
            if(dialog == DialogResult.OK)
            {
                img_file = openFileDialog1.FileName;
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
