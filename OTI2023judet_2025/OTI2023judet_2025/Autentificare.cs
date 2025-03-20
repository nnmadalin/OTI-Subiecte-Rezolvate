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

namespace OTI2023judet_2025
{
    public partial class Autentificare : Form
    {
        public Autentificare()
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
            if(e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X,  this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(initial.dbConnection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator = @email and Parola = @parola", conn);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@parola", textBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                textBox1.Clear(); textBox2.Clear();

                MessageBox.Show("Date de autentificare invalide!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                initial.numeUser = reader[1].ToString();
                initial.emailuser = reader[0].ToString();
                AlegeJoc frm = new AlegeJoc();
                frm.Show();
                this.Hide();
            }

            conn.Close();
        }

        private void Autentificare_Load(object sender, EventArgs e)
        {
            initial frm = new initial();
            frm.Hide();
        }
    }
}
