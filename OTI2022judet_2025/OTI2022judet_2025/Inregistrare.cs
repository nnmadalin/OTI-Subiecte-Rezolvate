using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2022judet_2025
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Autentificare frm = new Autentificare();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 4)
            {
                MessageBox.Show("Numele de utilizatoe trebuie sa aiba minim 4 caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }
            else if (textBox3.Text != textBox2.Text)
            {
                MessageBox.Show("Parolele nu corespund!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else if(textBox3.Text.Length < 6)
            {
                MessageBox.Show("Parola trebuie sa aiba minim 6 caractere!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else if(textBox4.Text.Contains("@") == false  || textBox4.Text.Contains(".") == false || textBox4.Text.Length <= 5)
            {
                MessageBox.Show("Emailul nu este valid!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE NumeUtilizator = @nume", conn);
            cmd.Parameters.AddWithValue("@nume", textBox1.Text);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if(sqlDataReader.Read())
            {
                MessageBox.Show("Numele de utilizator este deja in baza de date", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                cmd = new SqlCommand("INSERT INTO [Utilizatori] VALUES (@nume, @pass, @email, NULL)", conn);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);

                cmd.ExecuteNonQuery();

                textBox1.Clear(); textBox2.Clear(); textBox3.Clear();textBox4.Clear();

                MessageBox.Show("Utilizatorul a fost adaugat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            conn.Close();
        }
    }
}
