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

namespace OTI2019judet
{
    public partial class signin : Form
    {
        public signin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new home();
            frm.Show();
            this.Hide();
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
                int dx = e.X - _mouse.X;
                int dy = e.Y - _mouse.Y;

                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        bool check_null()
        {
            if (textBox1.Text.Trim() == "")
                return false;
            if (textBox2.Text.Trim() == "")
                return false;
            if (textBox3.Text.Trim() == "")
                return false;
            if (textBox4.Text.Trim() == "")
                return false;
            if (textBox5.Text.Trim() == "")
                return false;
            return true;
        }

        bool check_email()
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [utilizatori] where email = @email", conn);
                cmd.Parameters.Add("@email", textBox1.Text);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    conn.Close();
                    return false;
                }
                
                conn.Close();
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check_null())
            {
                if (check_email())
                {
                    if(textBox4.Text == textBox5.Text)
                    {
                        using (SqlConnection conn = new SqlConnection(home.db))
                        {
                            conn.Open();
                            SqlCommand cmd;

                            cmd = new SqlCommand("insert into [utilizatori] values (@email, @parola, @nume, @prenume)", conn);
                            cmd.Parameters.Add("@email", textBox1.Text);
                            cmd.Parameters.Add("@nume", textBox2.Text);
                            cmd.Parameters.Add("@prenume", textBox3.Text);
                            cmd.Parameters.Add("@parola", textBox4.Text);
                            cmd.ExecuteNonQuery();

                            conn.Close();
                        }
                        MessageBox.Show("Cont creat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        login.email_user = textBox1.Text;
                        login.nume_user = textBox2.Text;
                        login.prenume_user = textBox3.Text;

                        var frm = new menu();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Parolele nu corespund!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Email existent in baza de date!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Completati toate casetele!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
