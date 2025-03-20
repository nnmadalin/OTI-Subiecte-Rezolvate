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

namespace OTI2019judet_2025
{
    public partial class CreeazaContFreeBook : Form
    {
        public CreeazaContFreeBook()
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [utilizatori] WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                if(textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Eroare inregistrare: parolele nu corespund", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO [utilizatori] VALUES (@email, @parola, @nume, @prenume)", conn);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@parola", textBox3.Text);
                    cmd.Parameters.AddWithValue("@nume", textBox4.Text);
                    cmd.Parameters.AddWithValue("@prenume", textBox5.Text);
                    cmd.ExecuteNonQuery();

                    FreeBookHome frm = new FreeBookHome();
                    frm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Eroare inregistrare: email folosit", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }
    }
}
