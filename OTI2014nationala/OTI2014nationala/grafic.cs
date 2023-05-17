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

namespace OTI2014nationala
{
    public partial class grafic : Form
    {
        public grafic()
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
                this.Location = this.Location + (Size)e.Location - (Size)_mouse;
            }
        }

        void load_grafice()
        {
            chart1.Series["Colesterol_Total"].Points.Clear();
            chart2.Series["HDL"].Points.Clear();
            if(dataGridView1.Rows.Count > 0)
                dataGridView1.Rows.Clear();
            int k = 0;
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AnalizePacienti where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", (comboBox1.SelectedIndex + 1).ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    chart1.Series["Colesterol_Total"].Points.AddXY(Convert.ToDateTime(read["Data_Analize"].ToString()).ToShortDateString(), read["Colesterol_Total"].ToString());
                    if (Convert.ToInt32(read["Colesterol_Total"].ToString()) < 200)
                    {
                        chart1.Series["Colesterol_Total"].Points[k++].Color = Color.Blue;
                    }
                    else if (Convert.ToInt32(read["Colesterol_Total"].ToString()) < 240)
                    {
                        chart1.Series["Colesterol_Total"].Points[k++].Color = Color.Green;
                    }
                    else
                    {
                        chart1.Series["Colesterol_Total"].Points[k++].Color = Color.Red;
                    }
                }
                conn.Close();
            }
            k = 0;
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AnalizePacienti where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", (comboBox1.SelectedIndex + 1).ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    chart2.Series["HDL"].Points.AddXY(Convert.ToDateTime(read["Data_Analize"].ToString()).ToShortDateString(), read["HDL"].ToString());
                    if (Convert.ToInt32(read["HDL"].ToString()) < 40)
                    {
                        chart2.Series["HDL"].Points[k++].Color = Color.Blue;
                    }
                    else if (Convert.ToInt32(read["HDL"].ToString()) < 60)
                    {
                        chart2.Series["HDL"].Points[k++].Color = Color.Green;
                    }
                    else
                    {
                        chart2.Series["HDL"].Points[k++].Color = Color.Red;
                    }
                }
                conn.Close();
            }

            int varsta = 0;
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", (comboBox1.SelectedIndex + 1).ToString());
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    varsta = Convert.ToInt32(read["Varsta"].ToString());
                }
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AnalizePacienti where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", (comboBox1.SelectedIndex + 1).ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    double x = 0.0799 * varsta + 3.137 * Math.Log10(Convert.ToDouble(read["TAS"].ToString()))
                        + 0.180 * Math.Log10(Convert.ToDouble(read["PCR"].ToString())) + 1.382 * Math.Log10(Convert.ToDouble(read["Colesterol_Total"].ToString()))
                        - 1.172 * Math.Log10(Convert.ToDouble(read["HDL"].ToString())) + 0.818 * Math.Log10(Convert.ToDouble(read["Fumator"].ToString()))
                        + 0.438 * Math.Log10(Convert.ToDouble(read["BCVF"].ToString()));
                    double risk = (1 - Math.Pow(0.98634, x - 22.325)) * 100;

                    if(risk < 1)
                    {
                        dataGridView1.Rows.Add(Convert.ToDateTime(read["Data_Analize"].ToString()), "risc scăzut");
                    }
                    else if (risk < 5)
                    {
                        dataGridView1.Rows.Add(Convert.ToDateTime(read["Data_Analize"].ToString()), "risc moderat");
                    }
                    else if (risk < 10)
                    {
                        dataGridView1.Rows.Add(Convert.ToDateTime(read["Data_Analize"].ToString()), "risc crescut");
                    }
                    else
                    {
                        dataGridView1.Rows.Add(Convert.ToDateTime(read["Data_Analize"].ToString()), "risc foarte crescut");
                    }
                }
                conn.Close();
            }

        }

        private void grafic_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale ", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    comboBox1.Items.Add(read["Nume"].ToString() + " " + read["Prenume"].ToString());
                }
                comboBox1.SelectedIndex = 0;
                conn.Close();
            }
            load_grafice();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_grafice();
        }
    }
}
