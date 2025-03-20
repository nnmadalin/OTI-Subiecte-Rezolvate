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

namespace OTI2022judet_2025
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        void loadDataFromFileToDB()
        {
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            StreamReader streamReader = new StreamReader(classStrings.pathEXE + "/harti.txt");
            string line;
            while((line = streamReader.ReadLine()) != null)
            {
                string[] splitLine = line.Split('#');
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Harti] WHERE NumeHarta = @nume", conn);
                cmd.Parameters.AddWithValue("@nume", splitLine[0]);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (!sqlDataReader.Read())
                {
                    cmd = new SqlCommand("INSERT INTO [Harti] VALUES (@nume, @path)", conn);
                    cmd.Parameters.AddWithValue("@nume", splitLine[0]);
                    cmd.Parameters.AddWithValue("@path", splitLine[1]);
                    cmd.ExecuteNonQuery();
                }
            }

            streamReader = new StreamReader(classStrings.pathEXE + "/masurari.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] splitLine = line.Split('#');
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Masurare] WHERE PozitieX = @pozx and PozitieY = @pozy", conn);
                cmd.Parameters.AddWithValue("@pozx", splitLine[1]);
                cmd.Parameters.AddWithValue("@pozy", splitLine[2]);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (!sqlDataReader.Read())
                {
                    cmd = new SqlCommand("SELECT * FROM [Harti] WHERE NumeHarta = @nume", conn);
                    cmd.Parameters.AddWithValue("@nume", splitLine[0]);
                    sqlDataReader = cmd.ExecuteReader();
                    sqlDataReader.Read();

                    cmd = new SqlCommand("INSERT INTO [Masurare] VALUES (@id, @pozx, @pozy, @val, @date)", conn);
                    cmd.Parameters.AddWithValue("@id", sqlDataReader[0]);
                    cmd.Parameters.AddWithValue("@pozx", splitLine[1]);
                    cmd.Parameters.AddWithValue("@pozy", splitLine[2]);
                    cmd.Parameters.AddWithValue("@val", splitLine[3]);
                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(splitLine[4]));
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataFromFileToDB();
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE NumeUtilizator = @nume and Parola = @pass", conn);
            cmd.Parameters.AddWithValue("@nume", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            if (!sqlDataReader.Read())
            {
                
                textBox1.Clear(); textBox2.Clear();
                MessageBox.Show("Nume de utilizator si/sau parola invalid!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmd = new SqlCommand("UPDATE Utilizatori SET UltimaUtilizare = @date WHERE IdUtilizator = @id", conn);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", sqlDataReader[0]);
                cmd.ExecuteNonQuery();


                classStrings.idUser = sqlDataReader[0].ToString();
                classStrings.nameUser = sqlDataReader[1].ToString();

                Vizualizare frm = new Vizualizare();
                frm.Show();
                this.Hide();
            }


            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Inregistrare frm = new Inregistrare();
            frm.Show();
            this.Hide();
        }
    }
}
