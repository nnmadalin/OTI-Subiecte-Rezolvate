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

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "jucator" && textBox2.Text == "jucator")
            {
                var form = new normal();
                form.Show();
                this.Hide();
            }
            else if (textBox1.Text == "administrator" && textBox2.Text == "administrator")
            {
                var form = new admin();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Utilizator si/sau parola gresita!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox1.Text = "";
            }
        }
    }
}
