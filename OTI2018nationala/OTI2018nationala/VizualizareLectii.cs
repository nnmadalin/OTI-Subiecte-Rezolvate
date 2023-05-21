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
    public partial class VizualizareLectii : Form
    {
        public VizualizareLectii()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void VizualizareLectii_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Lectii", conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    listBox1.Items.Add(read["TitluLectie"].ToString());
                }

                conn.Close();
            }
            load_lectie(0);
        }

        void load_lectie(int id)
        {
            id++;
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Lectii where IdLectie = @id", conn);
                cmd.Parameters.Add("@id", id.ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"/Resurse_C#/ContinutLectii/" + read["NumeImagine"].ToString() + ".bmp");
                    using (SqlConnection conn2 = new SqlConnection(home.db))
                    {
                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("select * from Utilizatori where IdUtilizator = @id", conn2);
                        cmd2.Parameters.Add("@id", read["IdUtilizator"].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        if (read2.Read())
                        {
                            label3.Text = "Nume Utilizator: " + read2["Nume"].ToString();
                            label4.Text = "Email: " + read2["Email"].ToString();
                            label5.Text = "Regiune: " + read["Regiune"].ToString();
                            label6.Text = "Data: " + read["DataCreare"].ToString();
                        }

                        conn2.Close();
                    }
                }
                conn.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_lectie(listBox1.SelectedIndex);
        }
    }
}
