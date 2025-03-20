using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2015judet_2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0 && textBox1.Text == "")
            {
                Administrare frm = new Administrare();
                frm.Show();
                this.Hide();
            }
            else if(comboBox1.SelectedIndex == 1 && textBox1.Text == ""){
                
            }
            else
            {
                MessageBox.Show("Eroare", "Parola este gresita", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
