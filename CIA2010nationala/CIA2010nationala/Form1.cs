using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CIA2010nationala
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

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int patrate = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 1)
            {
                if(textBox1.Text == "3" || textBox1.Text == "4" || textBox1.Text == "5" || textBox1.Text == "6" || textBox1.Text == "7" || textBox1.Text == "8" || textBox1.Text == "9")
                {
                    panel2.Visible = false;

                    

                    jocToolStripMenuItem.Enabled = true;
                    salvareToolStripMenuItem.Enabled = true;

                    patrate = Convert.ToInt32(textBox1.Text);
                }
                else
                {
                    MessageBox.Show("Introdu doar valoe intre 3 si 9!");
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Introdu doar valoe intre 3 si 9!");
                textBox1.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.Size = new Size(785, 456);
            panel2.Location = new Point(17, 79);
        }

        int[,] matrix = new int[15, 15];

        private void change_color(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Tag);

            if (btn.BackColor == Color.Aqua)
            {
                btn.BackColor = Color.Black;
            }
            else
                btn.BackColor = Color.Aqua;

            

            cauta((id - patrate).ToString());
            cauta((id + patrate).ToString());
            if(id % patrate != 0)
                cauta((id + 1).ToString());
            if ((id-1) % patrate != 0)
                cauta((id - 1).ToString());

            int p = 1;
            textBox2.Text = "";
            foreach (Control control in panel3.Controls)
            {
                if (control is Button)
                {
                    if(p > patrate)
                    {
                        p = 1;
                        textBox2.Text += "\r\n";
                    }
                    if (control.BackColor == Color.Aqua)
                    {
                        textBox2.Text += ("0 ");
                    }
                    else
                        textBox2.Text += ("1 ");
                    p++;
                }
            }

        }

        void cauta(string cautat)
        {
            foreach (Control control in panel3.Controls)
            {
                if (control is Button && control.Tag.ToString() == cautat)
                {
                    
                    if (control.BackColor == Color.Aqua)
                    {
                        control.BackColor = Color.Black;
                    }
                    else
                        control.BackColor = Color.Aqua;
                    break;
                }
            }
        }

        void load_game()
        {
            int x = 10, y = 10, k = 1;
            Random random = new Random();
            for (int i = 1; i <= patrate; i++)
            {
                y = 10;
                for(int j = 1; j <= patrate; j++)
                {
                    
                    string str = random.Next(0, 2).ToString();
                    Button btn = new Button()
                    {
                        Text = "",
                        Size = new Size(45, 45),
                        Location = new Point(y, x),
                        BackColor = Color.Red,
                        Tag = k.ToString()
                    };
                    k++;
                    btn.Click += new EventHandler(change_color);
                    if (str == "0")
                    {
                        matrix[i, j] = 0;
                        btn.BackColor = Color.Aqua;
                    }
                    else
                    {
                        matrix[i, j] = 1;
                        btn.BackColor = Color.Black;
                    }
                    textBox2.Text += (str + " ");
                    panel3.Controls.Add(btn);
                    btn.Show();
                    y += 47;
                }
                x += 47;
                textBox2.Text += "\r\n";
            }
        }

        private void jocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel3.Size = new Size(785, 456);
            panel3.Location = new Point(17, 79);

            foreach (Control control in panel3.Controls)
            {
                if(control is Button)
                {
                    panel3.Controls.Remove(control);
                }
            }
            textBox2.Text = "";

            load_game();
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
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void salvareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult dialogResult = saveFileDialog1.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                using (StreamWriter writer = File.CreateText(saveFileDialog1.FileName))
                {
                    for (int i = 1; i <= patrate; i++)
                    {
                        string line = "";
                        for (int j = 1; j <= patrate; j++)
                        {
                            line += (matrix[i, j].ToString() + " ");
                        }
                        writer.WriteLine(line);
                    }
                }
                MessageBox.Show("Salvat cu succes");
            }
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new despre();
            frm.Show();
        }
    }
}
