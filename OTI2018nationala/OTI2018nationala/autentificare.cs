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

namespace OTI2018nationala
{
    public partial class autentificare : Form
    {
        public autentificare()
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
                this.Location = this.Location + (Size)e.Location - (Size)mouse;
        }

        public static string id = "", nume = "";

        private void button3_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Utilizatori where Email = @email and Parola = @pass", conn);
                cmd.Parameters.Add("@email", textBox1.Text);
                cmd.Parameters.Add("@pass", textBox2.Text);
                SqlDataReader read = cmd.ExecuteReader();

                if (!read.HasRows)
                {
                    MessageBox.Show("Email si/sau parola gresita!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    read.Read();
                    id = read["IdUtilizator"].ToString();
                    nume = read["Nume"].ToString();
                    MessageBox.Show("Au fost activate optiunile din meniul Centenar_Start", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    home.auth = true;
                    this.Close();
                }

                conn.Close();
            }
        }

        public static string email = "";

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() != "")
            {

                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from Utilizatori where Email = @email", conn);
                    cmd.Parameters.Add("@email", textBox1.Text);
                    SqlDataReader read = cmd.ExecuteReader();

                    if (!read.HasRows)
                    {
                        MessageBox.Show("Email gresit!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        email = textBox1.Text;
                        var frm = new am_uitat();
                        frm.ShowDialog();
                        this.Close();
                    }
                    conn.Close();
                }                
            }
            else
            {
                MessageBox.Show("Completeaza caseta EMAIL", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }
    }
}
