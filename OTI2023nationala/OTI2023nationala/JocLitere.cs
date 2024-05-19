using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;

namespace OTI2023nationala
{
    public partial class JocLitere : Form
    {
        public JocLitere()
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
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - mouse.X, this.Location.Y + e.Location.Y - mouse.Y);
            }
        }

        string[] nameImage = { "avion", "bloc", "caine", "caprioara", "iepure", "leu", "lup", "masina", "minge", "patine", "pisica", "taur", "urs", "vulpe" };
        //90
        string nowComp = "";
        int poz = 0;
        String[] randomOrder;

        

        private void JocLitere_Load(object sender, EventArgs e)
        {


            Random rand = new Random();
            string str;
            do
            {
                nameImage = nameImage.OrderBy(x => rand.Next()).ToArray();

                str = nameImage[0].Trim() + nameImage[1].Trim();
                randomOrder = new string[str.Length];

                for (int i = 0; i < str.Length; i++)
                    randomOrder[i] = str[i].ToString();

                randomOrder = randomOrder.OrderBy(x => rand.Next()).ToArray();
            } while (str.Length >= 12);

            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Imagini/" + nameImage[0] + ".png");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Imagini/" + nameImage[1] + ".png");

            int labelStart = 2;

            for (int i = 0; i < randomOrder.Length; i++)
            {
                foreach(Control ctr in panel2.Controls)
                {
                    if(ctr.Name == "label" + (labelStart + i).ToString())
                    {
                        ctr.Text = randomOrder[i].ToString();
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left && poz > 0)
            {
                poz--;
                pictureBox3.Location = new Point(pictureBox3.Location.X - 90, pictureBox3.Location.Y);
            }
            else if (keyData == Keys.Right && poz < randomOrder.Length)
            {
                poz++;
                pictureBox3.Location = new Point(pictureBox3.Location.X + 90, pictureBox3.Location.Y);
            }
            else if(keyData == Keys.Up)
            {
                if(randomOrder[poz] != " ")
                {
                    nowComp += randomOrder[poz];
                    randomOrder[poz] = " ";
                    foreach (Control ctr in panel2.Controls)
                    {
                        if (ctr.Name == "label" + (2 + poz).ToString())
                        {
                            ctr.Text = "";
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if(nameImage[0].StartsWith(nowComp) == false && nameImage[1].StartsWith(nowComp) == false)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("AI PIERDUT");

                SqlConnection conn = new SqlConnection(Form1.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@type, @email, @pct, @data)", conn);
                cmd.Parameters.AddWithValue("@type", "1");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@pct", 0);
                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                conn.Close();

                var frm2 = new AlegeJoc();
                frm2.Show();
                this.Hide();
            }
            else if(nameImage[0] == nowComp)
            {
                nowComp = "";
                pictureBox1.Image = null;
            }
            else if (nameImage[1] == nowComp)
            {
                nowComp = "";
                pictureBox2.Image = null;
            }

            if(pictureBox1.Image == null && pictureBox2.Image == null)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Ai Castigat!");

                SqlConnection conn = new SqlConnection(Form1.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@type, @email, @pct, @data)", conn);
                cmd.Parameters.AddWithValue("@type", "1");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@pct", timer);
                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                conn.Close();

                var frm2 = new AlegeJoc();
                frm2.Show();
                this.Hide();
            }
        }
        int timer = 100;
        private void timer2_Tick(object sender, EventArgs e)
        {
            label13.Text = "Timp ramas: " + timer.ToString();

            timer--;

            if(timer == 0)
            {
                MessageBox.Show("AI PIERDUT");

                SqlConnection conn = new SqlConnection(Form1.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@type, @email, @pct, @data)", conn);
                cmd.Parameters.AddWithValue("@type", "1");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@pct", 0);
                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                conn.Close();

                var frm2 = new AlegeJoc();
                frm2.Show();
                this.Hide();
            }
        }
    }
}
