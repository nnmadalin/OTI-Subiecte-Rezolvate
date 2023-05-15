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

namespace OTI2022judet
{
    public partial class inregistrare : Form
    {
        public inregistrare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void inregistrare_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Background/back14.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new autentificare();
            this.Hide();
        }

        bool check_textbox()
        {
            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Completati caseta ~Nume de utilizator~!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Completati caseta ~Parola~!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Completati caseta ~Confirmare Parola~!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Completati caseta ~Email~!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (textBox1.Text.Length < 4)
            {
                MessageBox.Show("Numele de utilizator trebuie sa aiba mai mult de 4 caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (textBox2.Text.Length < 4)
            {
                MessageBox.Show("Parola trebuie sa aiba mai mult de 6 caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Parolele nu corespund!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // verificare email

            int p1 = -1;

            for(int i = 0; i < textBox4.Text.Length; i++)
            {
                if(textBox4.Text[i] == '@')
                {
                    p1 = i;
                }
            }

            if(p1+4 > textBox4.Text.Length)
            {
                MessageBox.Show("Email invalid!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            
            return true;
        }


        bool check_username()
        {
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Utilizatori] where NumeUtilizator = @username", conn);
                cmd.Parameters.Add("@username", textBox1.Text);
                try
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        MessageBox.Show("Nume de utilizator deja in baza de date!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            };

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_textbox() && check_username())
            {
                using (SqlConnection conn = new SqlConnection(autentificare.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("insert into [Utilizatori] values (@username, @parola, @email, @last_login)", conn);
                    cmd.Parameters.Add("@username", textBox1.Text);
                    cmd.Parameters.Add("@parola", textBox2.Text);
                    cmd.Parameters.Add("@email", textBox4.Text);
                    cmd.Parameters.Add("@last_login", DateTime.Now);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Utilizator adaugat in baza de date!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var frm = new autentificare();
                        frm.Show(); this.Hide();
                    }
                    catch
                    {
                        MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                   

                    conn.Close();
                };
            }
        }
    }
}
