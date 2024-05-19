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
    public partial class creareCont : Form
    {
        public creareCont()
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

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.db);
            conn.Open();

            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Completati toate casetele!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Parolele nu corespund!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Utilizatori] VALUES (@email, @nume, @pass)", conn);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@nume", textBox2.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cont creeat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var frm = new Form1();
                    frm.Show();
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("Email deja folosit!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
        }
    }
}
