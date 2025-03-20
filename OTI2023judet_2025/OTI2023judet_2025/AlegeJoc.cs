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
            label2.Text = "Bine ai venit,\n" + initial.numeUser + "\n" + initial.emailuser;

            SqlConnection conn = new SqlConnection(initial.dbConnection);
            conn.Open();

            SqlCommand cmd, cmd2;
            SqlDataReader reader, reader2;
            int k = 0;

            cmd = new SqlCommand("SELECT * FROM [Rezultate] where TipJoc = 0 ORDER BY PunctajJoc DESC", conn);
            reader = cmd.ExecuteReader();
            while(reader.Read() && k != 3){
                cmd2 = new SqlCommand("SELECT * FROM [Utilizatori] where EmailUtilizator = @email", conn);
                cmd2.Parameters.AddWithValue("@email", reader[2].ToString());
                reader2 = cmd2.ExecuteReader();
                reader2.Read();
                dataGridView1.Rows.Add(reader2[1], reader2[0], reader[3]);
                k++;
            }

            k = 0;
            cmd = new SqlCommand("SELECT * FROM [Rezultate] where TipJoc = 1 ORDER BY PunctajJoc DESC", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read() && k != 3)
            {
                cmd2 = new SqlCommand("SELECT * FROM [Utilizatori] where EmailUtilizator = @email", conn);
                cmd2.Parameters.AddWithValue("@email", reader[2].ToString());
                reader2 = cmd2.ExecuteReader();
                reader2.Read();
                dataGridView2.Rows.Add(reader2[1], reader2[0], reader[3]);
                k++;
            }

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GhicesteCuvant frm = new GhicesteCuvant();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SarpeEducativ frm = new SarpeEducativ();
            frm.ShowDialog();
        }
    }
}
