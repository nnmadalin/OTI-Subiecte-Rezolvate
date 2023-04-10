using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cia2009judet
{
    public partial class calculator : Form
    {
        public calculator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new home();
            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new home();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "0";
        }
        long a = 0, b = 0;
        bool check_number()
        {
            
            bool bool_a = long.TryParse(textBox1.Text, out a);
            bool bool_b = long.TryParse(textBox2.Text, out b);

            if(bool_a == true && bool_b == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Introdu doar cifre!");
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                textBox3.Text = (a - b).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                textBox3.Text = (a * b).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                if (textBox2.Text == "0")
                    MessageBox.Show("Nu se imparte la 0");
                else
                    textBox3.Text = (a / b).ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                textBox2.Text = "0";
                textBox3.Text = (a * a).ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                textBox2.Text = "0";
                textBox3.Text = (Math.Sqrt(a)).ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool bool_a = long.TryParse(textBox1.Text, out a);
            if (textBox1.Text.Trim() == "" || bool_a == false)
                textBox1.Text = "0";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool bool_a = long.TryParse(textBox2.Text, out a);
            if (textBox2.Text.Trim() == "" || bool_a == false)
                textBox2.Text = "0";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check_number())
            {
                textBox3.Text = (a + b).ToString();
            }
        }
    }
}
