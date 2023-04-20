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
using System.IO;

namespace OTI2018judet
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;MultipleActiveResultSets=true;Connect Timeout=5";

        void load_db()
        {

            int where = 1;
            
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("delete from [Utilizatori]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from [Itemi]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from [Evaluari]", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DBCC checkident(Utilizatori, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC checkident(Itemi, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC checkident(Evaluari, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                StreamReader read = new StreamReader(Application.StartupPath + @"/OJI_2018_C#_resurse/date.txt");
                string line;
                while((line = read.ReadLine()) != null)
                {
                    string[] split = line.Split(';');
                    if(split.Length == 1 && split[0] == "Utilizatori:")
                    {
                        where = 1;
                    }
                    else if (split.Length == 1 && split[0] == "Itemi:")
                    {
                        where = 2;
                    }
                    else if (split.Length == 1 && split[0] == "Evaluari:")
                    {
                        where = 3;
                    }
                    else
                    {
                        if(where == 1)
                        {
                            cmd = new SqlCommand("insert into [Utilizatori] values (@nume, @parola, @email, @clasa)", conn);

                            cmd.Parameters.Add("@nume", split[0]);
                            cmd.Parameters.Add("@parola", split[1]);
                            cmd.Parameters.Add("@email", split[2]);
                            cmd.Parameters.Add("@clasa", split[3]);

                            cmd.ExecuteNonQuery();
                        }
                        else if(where == 2)
                        {
                            cmd = new SqlCommand("insert into [Itemi] values (@tip, @enunt, @rasp1, @rasp2, @rasp3, @rasp4, @raspcor)", conn);
                            cmd.Parameters.Add("@tip", split[0]);
                            cmd.Parameters.Add("@enunt", split[1]);

                            if(split[2] != "NULL")
                                cmd.Parameters.Add("@rasp1", split[2]);
                            else
                                cmd.Parameters.Add("@rasp1", DBNull.Value);

                            if (split[3] != "NULL")
                                cmd.Parameters.Add("@rasp2", split[3]);
                            else
                                cmd.Parameters.Add("@rasp2", DBNull.Value);

                            if (split[4] != "NULL")
                                cmd.Parameters.Add("@rasp3", split[4]);
                            else
                                cmd.Parameters.Add("@rasp3", DBNull.Value);

                            if (split[5] != "NULL")
                                cmd.Parameters.Add("@rasp4", split[5]);
                            else
                                cmd.Parameters.Add("@rasp4", DBNull.Value);

                            cmd.Parameters.Add("@raspcor", split[6]);

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("insert into [Evaluari] values (@id, @data, @nota)", conn);

                            cmd.Parameters.Add("@id", split[0]);
                            cmd.Parameters.Add("@data", split[1]);
                            cmd.Parameters.Add("@nota", split[2]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                conn.Close();
            }
        }

        int index = 0;

        void load_img()
        {
            if(index == 5)
            {
                index = 0;
            }
            progressBar1.Value = (index + 1);

            pictureBox1.ImageLocation = Application.StartupPath + "/OJI_2018_C#_resurse/imaginislideshow/" + (index + 1) + @".jpg";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_db();
            load_img();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(button5.Text == "Manual")
            {
                button5.Text = "Auto";
                timer1.Stop();
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button5.Text = "Manual";
                timer1.Start();
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            index++;
            if (index == 5)
                index = 0;

            load_img();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            index--;
            if (index == -1)
                index = 4;

            load_img();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            index++;
            if (index == 5)
                index = 0;

            load_img();
        }

        public static string nume_user = "", email_user = "", clasa_user = "", id_user = "";

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Eroare de autentificare!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from [Utilizatori] where EmailUtilizator = @email and ParolaUtilizator = @pass", conn);
                    cmd.Parameters.Add("email", textBox2.Text);
                    cmd.Parameters.Add("pass", textBox3.Text);
                    SqlDataReader read = cmd.ExecuteReader();

                    if (read.Read())
                    {
                        id_user = read["IdUtilizator"].ToString();
                        nume_user = read["NumePrenumeUtilizator"].ToString();
                        email_user = read["EmailUtilizator"].ToString();
                        clasa_user = read["ClasaUtilizator"].ToString();

                        var frm = new elev();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Eroare de autentificare!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }

                    conn.Close();
                }
            }
        }
    }
}
