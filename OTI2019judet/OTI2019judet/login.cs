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
    public partial class login : Form
    {
        public login()
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
            if(e.Button == MouseButtons.Left)
            {
                int dx = e.X - _mouse.X;
                int dy = e.Y - _mouse.Y;

                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        public static string email_user = "", nume_user = "", prenume_user = "";

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from  [utilizatori] where email = @email and parola = @parola", conn);
                cmd.Parameters.Add("@email", textBox1.Text);
                cmd.Parameters.Add("@parola", textBox2.Text);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    read.Read();

                    email_user = read["email"].ToString();
                    nume_user = read["nume"].ToString();
                    prenume_user = read["prenume"].ToString();

                    var frm = new menu();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Eroare autentificare!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = textBox2.Text = "";
                }

                conn.Close();
            }
        }
    }
}
