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
using System.IO;

namespace OTI2023judet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\JocEducativ.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";

        public static string emailUser = "", numeUser = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                SqlCommand cmd;

                using (StreamReader streamReader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Utilizatori.txt"))
                {
                    string line;
                    string[] lineSplit;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineSplit = line.Split(';');

                        cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator=@email", conn);
                        cmd.Parameters.AddWithValue("@email", lineSplit[0]);
                        SqlDataReader read = cmd.ExecuteReader();

                        if (!read.HasRows)
                        {
                            cmd = new SqlCommand("INSERT INTO [Utilizatori] VALUES (@email, @nume, @parola)", conn);
                            cmd.Parameters.AddWithValue("@email", lineSplit[0]);
                            cmd.Parameters.AddWithValue("@nume", lineSplit[1]);
                            cmd.Parameters.AddWithValue("@parola", lineSplit[2]);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }

                using (StreamReader streamReader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Rezultate.txt"))
                {
                    string line;
                    string[] lineSplit;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineSplit = line.Split(';');

                        cmd = new SqlCommand("SELECT * FROM [Rezultate] WHERE idRezultat=@id", conn);
                        cmd.Parameters.AddWithValue("@id", lineSplit[0]);
                        SqlDataReader read = cmd.ExecuteReader();

                        if (!read.HasRows)
                        {
                            cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@TipJoc, @EmailUtilizator, @PunctajJoc)", conn);
                            cmd.Parameters.AddWithValue("@TipJoc", lineSplit[1]);
                            cmd.Parameters.AddWithValue("@EmailUtilizator", lineSplit[2]);
                            cmd.Parameters.AddWithValue("@PunctajJoc", lineSplit[3]);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }

                using (StreamReader streamReader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Itemi.txt"))
                {
                    string line;
                    string[] lineSplit;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineSplit = line.Split(';');

                        cmd = new SqlCommand("SELECT * FROM [Itemi] WHERE idItem=@id", conn);
                        cmd.Parameters.AddWithValue("@id", lineSplit[0]);
                        SqlDataReader read = cmd.ExecuteReader();

                        if (!read.HasRows)
                        {
                            cmd = new SqlCommand("INSERT INTO [Itemi] VALUES (@EnuntItem, @Raspuns1, @Raspuns2, @Raspuns3, @RaspunsCorect, @PunctajItem)", conn);
                            cmd.Parameters.AddWithValue("@EnuntItem", lineSplit[1]);
                            cmd.Parameters.AddWithValue("@Raspuns1", lineSplit[2]);
                            cmd.Parameters.AddWithValue("@Raspuns2", lineSplit[3]);
                            cmd.Parameters.AddWithValue("@Raspuns3", lineSplit[4]);
                            cmd.Parameters.AddWithValue("@RaspunsCorect", lineSplit[5]);
                            cmd.Parameters.AddWithValue("@PunctajItem", lineSplit[6]);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }

                conn.Close();
            }


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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator=@email and Parola=@pass", conn);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    emailUser = sqlDataReader[0].ToString();
                    numeUser = sqlDataReader[1].ToString();

                    var frm = new AlegeJoc();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();

                    MessageBox.Show("Date de autentificare invalide!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conn.Close();
            }
        }
    }
}
