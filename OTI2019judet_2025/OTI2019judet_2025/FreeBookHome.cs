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
using System.Globalization;

namespace OTI2019judet_2025
{
    public partial class FreeBookHome : Form
    {
        public FreeBookHome()
        {
            InitializeComponent();
        }


        void loadDataFromFileToDB()
        {
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd;
            SqlDataReader sqlDataReader;
            StreamReader streamReader;
            string line;

            streamReader = new StreamReader(classStrings.resursePath + "/utilizatori.txt");
            while((line = streamReader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('*');
                cmd = new SqlCommand("SELECT * FROM [utilizatori] WHERE email = @email", conn);
                cmd.Parameters.AddWithValue("@email", lineSplit[0]);

                sqlDataReader = cmd.ExecuteReader();

                if (!sqlDataReader.Read())
                {
                    cmd = new SqlCommand("INSERT INTO [Utilizatori] VALUES (@email, @parola, @nume, @prenume)", conn);
                    cmd.Parameters.AddWithValue("@email", lineSplit[0]);
                    cmd.Parameters.AddWithValue("@parola", lineSplit[1]);
                    cmd.Parameters.AddWithValue("@nume", lineSplit[2]);
                    cmd.Parameters.AddWithValue("@prenume", lineSplit[3]);
                    cmd.ExecuteNonQuery();
                }

            }

            streamReader = new StreamReader(classStrings.resursePath + "/carti.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('*');
                cmd = new SqlCommand("SELECT * FROM [carti] WHERE titlu = @titlu", conn);
                cmd.Parameters.AddWithValue("@titlu", lineSplit[0]);

                sqlDataReader = cmd.ExecuteReader();

                if (!sqlDataReader.Read())
                {
                    cmd = new SqlCommand("INSERT INTO [carti] VALUES (@titlu, @autor, @gen)", conn);
                    cmd.Parameters.AddWithValue("@titlu", lineSplit[0]);
                    cmd.Parameters.AddWithValue("@autor", lineSplit[1]);
                    cmd.Parameters.AddWithValue("@gen", lineSplit[2]);
                    cmd.ExecuteNonQuery();
                }
            }

            streamReader = new StreamReader(classStrings.resursePath + "/imprumuturi.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('*');

                cmd = new SqlCommand("SELECT * FROM [carti] WHERE titlu = @titlu", conn);
                cmd.Parameters.AddWithValue("@titlu", lineSplit[0]);
                sqlDataReader = cmd.ExecuteReader();
                sqlDataReader.Read();

                var id = sqlDataReader[0];

                cmd = new SqlCommand("SELECT * FROM [imprumut] WHERE id_carte = @id and email = @email and data_imprumut = @data", conn);
                cmd.Parameters.AddWithValue("@id", sqlDataReader[0]);
                cmd.Parameters.AddWithValue("@email", lineSplit[1]);
                cmd.Parameters.AddWithValue("@data", DateTime.ParseExact(lineSplit[2].Trim(), "M/d/yyyy", CultureInfo.InvariantCulture));

                sqlDataReader = cmd.ExecuteReader();

                if (!sqlDataReader.Read())
                {
                    cmd = new SqlCommand("INSERT INTO [imprumut] VALUES (@id, @email, @data)", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", lineSplit[1]);
                    cmd.Parameters.AddWithValue("@data", DateTime.ParseExact(lineSplit[2].Trim(), "M/d/yyyy", CultureInfo.InvariantCulture));
                    cmd.ExecuteNonQuery();
                }
            }



            conn.Close();
        }
        private void FreeBookHome_Load(object sender, EventArgs e)
        {
            loadDataFromFileToDB();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            CreeazaContFreeBook frm = new CreeazaContFreeBook();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogareFreeBook frm = new LogareFreeBook();
            frm.Show();
            this.Hide();
        }
    }
}
