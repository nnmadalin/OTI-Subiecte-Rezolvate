using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIA2012nationala
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

        Point mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouse.X;
                int dy = e.Location.Y - mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox2.Visible == false)
                listBox2.Visible = true;
            else
                listBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(copii.Length >= 2 && propozitie.Length >= 1 && listBox1.Items.Count > 1)
            {
                int p = lengh;
                int val = 0;
                while (p > 1)
                {
                    p--;
                    val++;
                    if (val > listBox1.Items.Count - 1) 
                        val = 0;
                }
                MessageBox.Show("Iese din joc: " + listBox1.Items[val]);
                listBox1.Items.RemoveAt(val);
            }
            else if(listBox1.Items.Count == 1)
            {
                MessageBox.Show("A castigat: " + listBox1.Items[0]);
                load_copii();
            }
            else
            {
                MessageBox.Show("Trebuie minim 2 copii si minim un cuvant in propozitie", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string[] copii, propozitie;
        int lengh = 0;

        void load_copii()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            copii = textBox1.Text.Split(new char[] { ' ', '.',',',';','?','!' });
            if (propozitie != null)
            {
                for (int i = 0; i < copii.Length; i++)
                {
                    if (copii[i].Trim() != "")
                    {
                        listBox1.Items.Add(copii[i]);
                        listBox2.Items.Add(copii[i]);
                    }
                }
                
            }
            propozitie = textBox2.Text.Split(new char[] { ' ', '.', ',', ';', '?', '!' });
            lengh = 0;
            if (propozitie != null)
            {
                for (int i = 0; i < propozitie.Length; i++)
                {
                    if (propozitie[i].Trim() != "")
                    {
                        lengh++;
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            load_copii();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            load_copii();
        }
    }
}
