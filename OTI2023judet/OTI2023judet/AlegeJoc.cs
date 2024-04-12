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
using System.Data.Sql;

namespace OTI2023judet
{
    public partial class AlegeJoc : Form
    {
        public AlegeJoc()
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

        private void AlegeJoc_Load(object sender, EventArgs e)
        {
            label2.Text = "Bine ai venit, " + Form1.numeUser + "! (" + Form1.emailUser + ")"; 

            using (SqlConnection conn = new SqlConnection(Form1.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM [Rezultate] ORDER BY PunctajJoc DESC", conn);
                SqlDataReader read = cmd.ExecuteReader();

                int dgv1 = 0, dgv2 = 0;
                while(read.HasRows && read.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator=@email", conn);
                    cmd2.Parameters.AddWithValue("@email", read["EmailUtilizator"]);
                    SqlDataReader read2 = cmd2.ExecuteReader();
                    read2.Read();

                    if (read["TipJoc"].ToString() == "0" && dgv1 <= 2)
                    {
                        dataGridView1.Rows.Add(read2["NumeUtilizator"], read["EmailUtilizator"], read["PunctajJoc"]);
                        dgv1++;
                    }
                    else if(read["TipJoc"].ToString() == "1" && dgv2 <= 2)
                    {
                        dataGridView2.Rows.Add(read2["NumeUtilizator"], read["EmailUtilizator"], read["PunctajJoc"]);
                        dgv2++;
                    }
                }

                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new Ghiceste();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new Sarpe();
            frm.Show();
        }
    }
}
