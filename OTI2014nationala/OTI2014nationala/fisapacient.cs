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
    public partial class fisapacient : Form
    {
        public fisapacient()
        {
            InitializeComponent();
        }

        int index = 1, maxim = 0;

        void load_pacient()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); dateTimePicker2.Value = DateTime.Now;
            textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            checkBox1.Checked = checkBox2.Checked = false; radioButton1.Checked = true;

            textBox1.Text = index.ToString();

            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", index);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    
                    textBox3.Text = read["Nume"].ToString();
                    textBox4.Text = read["Prenume"].ToString();
                    dateTimePicker2.Value = Convert.ToDateTime(read["Data_Nasterii"].ToString());
                    textBox2.Text = ((int)((DateTime.Now - dateTimePicker2.Value).TotalDays / 365)).ToString();;
                    textBox6.Text = read["Email"].ToString();
                    if (read["Gen"].ToString() == "F")
                        radioButton2.Checked = true;
                }
                conn.Close();
            }

            load_analize_data();
        }

        void load_analize_data()
        {
            textBox7.Clear(); textBox8.Clear(); textBox9.Clear(); textBox10.Clear();
            checkBox1.Checked = checkBox2.Checked = false;
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AnalizePacienti where ID_Pacient = @id and Data_Analize = @data", conn);
                cmd.Parameters.Add("@id", index);
                cmd.Parameters.Add("@data", dateTimePicker1.Value.ToShortDateString());
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    textBox7.Text = read["Colesterol_Total"].ToString();
                    textBox8.Text = read["HDL"].ToString();
                    textBox9.Text = read["TAS"].ToString();
                    textBox10.Text = read["PCR"].ToString();
                    if (read["BCVF"].ToString() == "1")
                        checkBox1.Checked = true;
                    if (read["Fumator"].ToString() == "1")
                        checkBox2.Checked = true;
                }
                conn.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(index < maxim)
            {
                index++;
                load_pacient();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(index > 1)
            {
                index--;
                load_pacient();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            index = 1;
            load_pacient();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            index = maxim;
            load_pacient();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            load_analize_data();
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update DatePersonale set Nume = @nume, Prenume = @prenume, Gen = @gen, Varsta = @varsta, Data_Nasterii = @data, Email = @email where ID_Pacient = @id", conn);
                cmd.Parameters.Add("@id", textBox1.Text);
                cmd.Parameters.Add("@nume", textBox3.Text);
                cmd.Parameters.Add("@prenume", textBox4.Text);
                if(radioButton1.Checked == true)
                    cmd.Parameters.Add("@gen", "M");
                else
                    cmd.Parameters.Add("@gen", "F");
                cmd.Parameters.Add("@varsta", textBox2.Text);
                cmd.Parameters.Add("@data", dateTimePicker2.Value.ToShortDateString());
                cmd.Parameters.Add("@email", textBox6.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Salvat cu succes!");

                conn.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool gasit = false;

            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AnalizePacienti where ID_Pacient = @id and Data_Analize = @data", conn);
                cmd.Parameters.Add("@id", index);
                cmd.Parameters.Add("@data", dateTimePicker1.Value.ToShortDateString());
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    gasit = true;
                }
                conn.Close();
            }
            if (gasit == true)
            {
                using (SqlConnection conn = new SqlConnection(medic.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update AnalizePacienti set Colesterol_Total = @ct, HDL = @HDL, TAS = @TAS, PCR = @PCR, BCVF = @BCVF, Fumator = @Fumator where ID_Pacient = @id and Data_Analize = @data", conn);
                    cmd.Parameters.Add("@ct", textBox7.Text);
                    cmd.Parameters.Add("@HDL", textBox8.Text);
                    cmd.Parameters.Add("@TAS", textBox9.Text);                    
                    cmd.Parameters.Add("@PCR", textBox10.Text);
                    if(checkBox1.Checked == true)
                        cmd.Parameters.Add("@BCVF", "1");
                    else
                        cmd.Parameters.Add("@BCVF", "0");
                    if (checkBox2.Checked == true)
                        cmd.Parameters.Add("@Fumator", "1");
                    else
                        cmd.Parameters.Add("@Fumator", "0");
                    cmd.Parameters.Add("@id", index);
                    cmd.Parameters.Add("@data", dateTimePicker1.Value.ToShortDateString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Salvat cu succes!");

                    conn.Close();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(medic.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into AnalizePacienti values (@id, @data, @ct, @HDL, @TAS, @PCR, @BCVF, @Fumator)", conn);
                    cmd.Parameters.Add("@ct", textBox7.Text);
                    cmd.Parameters.Add("@HDL", textBox8.Text);
                    cmd.Parameters.Add("@TAS", textBox9.Text);
                    cmd.Parameters.Add("@PCR", textBox10.Text);
                    if (checkBox1.Checked == true)
                        cmd.Parameters.Add("@BCVF", "1");
                    else
                        cmd.Parameters.Add("@BCVF", "0");
                    if (checkBox2.Checked == true)
                        cmd.Parameters.Add("@Fumator", "1");
                    else
                        cmd.Parameters.Add("@Fumator", "0");
                    cmd.Parameters.Add("@id", index);
                    cmd.Parameters.Add("@data", dateTimePicker1.Value.ToShortDateString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Salvat cu succes!");

                    conn.Close();
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = ((int)((DateTime.Now - dateTimePicker2.Value).TotalDays / 365)).ToString(); ;
        }

        private void fisapacient_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    maxim++;
                }
                conn.Close();

            }

            load_pacient();
        }
    }
}
