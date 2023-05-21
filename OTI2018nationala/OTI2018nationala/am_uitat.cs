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
using System.IO;

namespace OTI2018nationala
{
    public partial class am_uitat : Form
    {
        public am_uitat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                this.Location = this.Location + (Size)e.Location - (Size)mouse;
        }

        string[] oameni = new string[20];
        string[] not_oameni = {"2.jpg", "3.jpg", "4.jpg", "7.jpg", "10.jpg", "11.jpg", "14.jpg", "16.jpg" };
        bool[] is_oameni = new bool[20];

        void generate_captcha()
        {
            is_oameni = new bool[20];
            bool[] img_fost = new bool[20];
            bool[] img_fost_2 = new bool[20];

            pictureBox1.BorderStyle = pictureBox2.BorderStyle = pictureBox3.BorderStyle = pictureBox4.BorderStyle = pictureBox5.BorderStyle = pictureBox6.BorderStyle = BorderStyle.None;

            Random rand = new Random();

            int x = Convert.ToInt32(rand.Next(1, 3));
            if(x == 1)
            {
                is_oameni[1] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while(img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[1] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

            x = Convert.ToInt32(rand.Next(1, 3));
            if (x == 1)
            {
                is_oameni[2] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while (img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[2] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

            x = Convert.ToInt32(rand.Next(1, 3));
            if (x == 1)
            {
                is_oameni[3] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while (img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox3.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[3] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox3.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

            x = Convert.ToInt32(rand.Next(1, 3));
            if (x == 1)
            {
                is_oameni[4] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while (img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox4.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[4] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox4.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

            x = Convert.ToInt32(rand.Next(1, 3));
            if (x == 1)
            {
                is_oameni[5] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while (img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox5.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[5] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox5.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

            x = Convert.ToInt32(rand.Next(1, 3));
            if (x == 1)
            {
                is_oameni[6] = true;
                int y = Convert.ToInt32(rand.Next(0, 12));
                while (img_fost[y] != false)
                {
                    y = Convert.ToInt32(rand.Next(0, 12));
                }
                img_fost[y] = true;
                pictureBox6.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + oameni[y]);
            }
            else
            {
                is_oameni[6] = false;
                int y = Convert.ToInt32(rand.Next(0, 8));
                while (img_fost_2[y] == true)
                {
                    y = Convert.ToInt32(rand.Next(0, 8));
                }
                img_fost_2[y] = true;
                pictureBox6.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/Captcha/" + not_oameni[y]);
            }

        }

        private void am_uitat_Load(object sender, EventArgs e)
        {
            label2.Text = "Am uitat parola pentru emailul: " + autentificare.email;

            using (StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/oameni.txt"))
            {
                int k = 0;
                string row;
                while((row = read.ReadLine()) != null)
                {
                    oameni[k++] = row;
                }
            }

            generate_captcha();

        }

        bool check_img()
        {
            if (pictureBox1.BorderStyle == BorderStyle.None && is_oameni[1] == true)
                return false;
            if (pictureBox1.BorderStyle != BorderStyle.None && is_oameni[1] != true)
                return false;

            if (pictureBox2.BorderStyle == BorderStyle.None && is_oameni[2] == true)
                return false;
            if (pictureBox2.BorderStyle != BorderStyle.None && is_oameni[2] != true)
                return false;

            if (pictureBox3.BorderStyle == BorderStyle.None && is_oameni[3] == true)
                return false;
            if (pictureBox3.BorderStyle != BorderStyle.None && is_oameni[3] != true)
                return false;

            if (pictureBox4.BorderStyle == BorderStyle.None && is_oameni[4] == true)
                return false;
            if (pictureBox4.BorderStyle != BorderStyle.None && is_oameni[4] != true)
                return false;

            if (pictureBox5.BorderStyle == BorderStyle.None && is_oameni[5] == true)
                return false;
            if (pictureBox5.BorderStyle != BorderStyle.None && is_oameni[5] != true)
                return false;

            if (pictureBox6.BorderStyle == BorderStyle.None && is_oameni[6] == true)
                return false;
            if (pictureBox6.BorderStyle != BorderStyle.None && is_oameni[6] != true)
                return false;

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check_img() == false)
            {
                MessageBox.Show("Ai gresit la captcha!");
                generate_captcha();
            }
            else
            {
                if (textBox1.Text == textBox2.Text && textBox1.Text.Length >= 5)
                {
                    using (SqlConnection conn = new SqlConnection(home.db))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("Update Utilizatori set Parola = @parola where Email = @email", conn);
                        cmd.Parameters.Add("@email", autentificare.email);
                        cmd.Parameters.Add("@parola", textBox2.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Parola modificata");
                        this.Close();

                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Parola gresita");
                    generate_captcha();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pct = sender as PictureBox;
            if(pct.BorderStyle == BorderStyle.None)
            {
                pct.BorderStyle = BorderStyle.Fixed3D;
            }
            else
                pct.BorderStyle = BorderStyle.None;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
