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
using System.IO;

namespace OJTI2018judet_2025
{
    public partial class eLearning1918_start : Form
    {
        public eLearning1918_start()
        {
            InitializeComponent();
        }

        void loadDB()
        {
            SqlConnection conn = new SqlConnection(ClassStrings.dbString);
            conn.Open();

            SqlCommand cmd;

            StreamReader streamReader = new StreamReader(ClassStrings.ResursePath + "date.txt");
            string line;
            int stateInsertDB = 1;
            while((line = streamReader.ReadLine()) != null)
            {
                if(line.Trim() == "Utilizatori:")
                {
                    stateInsertDB = 1;
                }
                else if (line.Trim() == "Itemi:")
                {
                    stateInsertDB = 2;
                }
                else if (line.Trim() == "Evaluari:")
                {
                    stateInsertDB = 3;
                }
                else
                {
                    string[] lineSplited = line.Split(';');

                    if(stateInsertDB == 1)
                    {
                        cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE CONVERT(VARCHAR, NumePrenumeUtilizator)  = @numePrenume and CONVERT(VARCHAR, EmailUtilizator) = @email", conn);
                        cmd.Parameters.AddWithValue("@numePrenume", lineSplited[0]);
                        cmd.Parameters.AddWithValue("@email", lineSplited[2]);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.Read())
                        {
                            cmd = new SqlCommand("INSERT INTO [Utilizatori] values(@numePrenume, @parola, @email, @clasa)", conn);

                            cmd.Parameters.AddWithValue("@numePrenume", lineSplited[0]);
                            cmd.Parameters.AddWithValue("@parola", lineSplited[1]);
                            cmd.Parameters.AddWithValue("@email", lineSplited[2]);
                            cmd.Parameters.AddWithValue("@clasa", lineSplited[3]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if(stateInsertDB == 2)
                    {
                        cmd = new SqlCommand("SELECT * FROM [Itemi] WHERE CONVERT(VARCHAR, EnuntItem) = @enunt", conn);
                        cmd.Parameters.AddWithValue("@enunt", lineSplited[1]);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.Read())
                        {   
                            cmd = new SqlCommand("INSERT INTO [Itemi] values(@tip, @enunt, @rasp1, @rasp2, @rasp3, @rasp4, @raspCorect)", conn);

                            cmd.Parameters.AddWithValue("@tip", lineSplited[0]);
                            cmd.Parameters.AddWithValue("@enunt", lineSplited[1]);
                            cmd.Parameters.AddWithValue("@rasp1", lineSplited[2]);
                            cmd.Parameters.AddWithValue("@rasp2", lineSplited[3]);
                            cmd.Parameters.AddWithValue("@rasp3", lineSplited[4]);
                            cmd.Parameters.AddWithValue("@rasp4", lineSplited[5]);
                            cmd.Parameters.AddWithValue("@raspCorect", lineSplited[6]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM [Evaluari] WHERE IdElev = @id and DataEvaluare = @data", conn);
                        cmd.Parameters.AddWithValue("@id", lineSplited[0]);
                        cmd.Parameters.AddWithValue("@data", lineSplited[1]);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.Read())
                        {
                            cmd = new SqlCommand("INSERT INTO [Evaluari] values(@id, @data, @nota)", conn);

                            cmd.Parameters.AddWithValue("@id", lineSplited[0]);
                            cmd.Parameters.AddWithValue("@data", lineSplited[1]);
                            cmd.Parameters.AddWithValue("@nota", lineSplited[2]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            conn.Close();
        }

        int indexImage = 1;

        void autoNavImage()
        {
            indexImage++;

            if (indexImage == 6) 
                indexImage = 1;
            progressBar1.Value = indexImage;
            pictureBox1.Image = Image.FromFile(ClassStrings.ResursePath + "imaginislideshow/" + indexImage.ToString() + ".jpg");

        }

        void nextNavImage()
        {
            indexImage++;

            if (indexImage == 6)
                indexImage = 1;

            progressBar1.Value = indexImage;
            pictureBox1.Image = Image.FromFile(ClassStrings.ResursePath + "imaginislideshow/" + indexImage.ToString() + ".jpg");
        }

        void prevNavImage()
        {
            indexImage--;

            if (indexImage == 0)
                indexImage = 5;

            progressBar1.Value = indexImage;
            pictureBox1.Image = Image.FromFile(ClassStrings.ResursePath + "imaginislideshow/" + indexImage.ToString() + ".jpg");
        }

        private void eLearning1918_start_Load(object sender, EventArgs e)
        {
            loadDB();
            pictureBox1.Image = Image.FromFile(ClassStrings.ResursePath + "imaginislideshow/" + indexImage.ToString() + ".jpg");

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
            if(e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            autoNavImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(button4.Text == "Manual")
            {
                timer1.Enabled = false;
                button4.Text = "Auto";
                button6.Enabled = button5.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
                button4.Text = "Manual";
                button6.Enabled = button5.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nextNavImage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            prevNavImage();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ClassStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Utilizatori WHERE CONVERT(VARCHAR, EmailUtilizator) = @email and CONVERT(VARCHAR, ParolaUtilizator) = @parola", conn);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@parola", textBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ClassStrings.idUser = reader[0].ToString();
                ClassStrings.emailUser = reader[3].ToString();
                ClassStrings.numeUser = reader[1].ToString();

                eLearning1918_Elev frm = new eLearning1918_Elev();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Eroare de autentificare!", "Eroare!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = textBox2.Text = "";
            }

            conn.Close();
        }
    }
}
