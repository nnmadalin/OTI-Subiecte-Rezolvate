using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2015judet
{
    public partial class auth : Form
    {
        public auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public static bool normal = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0 && textBox2.Text == "oti2015")
            {
                normal = false;
                var home = new home();
                home.Show();
                this.Hide();
            }
            else if(comboBox1.SelectedIndex == 1 && textBox2.Text == "agentie2015")
            {
                normal = true;
                var home = new home();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Parola saut utilizator gresit!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void auth_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
