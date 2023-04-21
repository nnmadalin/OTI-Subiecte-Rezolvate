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

namespace OTI2019judet
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FreeBook.mdf;Integrated Security=True;MultipleActiveResultSets = true; Connect Timeout=3";

        void insert_file_to_db()
        {
            using(SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();
                SqlCommand cmd;

                

                //load file

                using (StreamReader read = new StreamReader(Application.StartupPath + "/OJTI_2019_C#_resurse/utilizatori.txt"))
                {
                    string line;
                    while((line = read.ReadLine()) != null)
                    {
                        string[] split = line.Split('*');

                        cmd = new SqlCommand("select * from [utilizatori] where email = @email and parola = @parola and nume = @nume and prenume = @prenume", conn);
                        cmd.Parameters.Add("@email", split[0]);
                        cmd.Parameters.Add("@parola", split[1]);
                        cmd.Parameters.Add("@nume", split[2]);
                        cmd.Parameters.Add("@prenume", split[3]);
                        SqlDataReader readsql = cmd.ExecuteReader();

                        if (!readsql.HasRows)
                        {
                            cmd = new SqlCommand("insert into [utilizatori] values (@email, @parola, @nume, @prenume)", conn);
                            cmd.Parameters.Add("@email", split[0]);
                            cmd.Parameters.Add("@parola", split[1]);
                            cmd.Parameters.Add("@nume", split[2]);
                            cmd.Parameters.Add("@prenume", split[3]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                int i = 0;
                using (StreamReader read = new StreamReader(Application.StartupPath + "/OJTI_2019_C#_resurse/carti.txt"))
                {
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        string[] split = line.Split('*');

                        cmd = new SqlCommand("select * from [carti] where titlu = @titlu and autor = @autor and gen = @gen", conn);
                        cmd.Parameters.Add("@titlu", split[0]);
                        cmd.Parameters.Add("@autor", split[1]);
                        cmd.Parameters.Add("@gen", split[2]);
                        SqlDataReader readsql = cmd.ExecuteReader();

                        if (!readsql.HasRows)
                        {

                            cmd = new SqlCommand("insert into [carti] values (@titlu, @autor, @gen)", conn);
                            cmd.Parameters.Add("@titlu", split[0]);
                            cmd.Parameters.Add("@autor", split[1]);
                            cmd.Parameters.Add("@gen", split[2]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                using (StreamReader read = new StreamReader(Application.StartupPath + "/OJTI_2019_C#_resurse/imprumuturi.txt"))
                {
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        string[] split = line.Split('*');

                        string id = "";


                        cmd = new SqlCommand("select * from [carti] where titlu = @titlu", conn);
                        cmd.Parameters.Add("@titlu", split[0]);
                        SqlDataReader readsql = cmd.ExecuteReader();
                        readsql.Read();
                        id = readsql["id_carte"].ToString();

                        cmd = new SqlCommand("select * from [imprumut] where id_carte = @id_carte and email = @email and data_imprumut = @data_imprumut", conn);
                        cmd.Parameters.Add("@id_carte", id);
                        cmd.Parameters.Add("@email", split[1]);
                        cmd.Parameters.Add("@data_imprumut", Convert.ToDateTime(split[2]));

                        readsql = cmd.ExecuteReader();

                        if (!readsql.HasRows)
                        {
                            cmd = new SqlCommand("insert into [imprumut] values (@id_carte, @email, @data_imprumut)", conn);

                            cmd.Parameters.Add("@id_carte", id);
                            cmd.Parameters.Add("@email", split[1]);
                            cmd.Parameters.Add("@data_imprumut", Convert.ToDateTime(split[2]));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                conn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            insert_file_to_db();
            pictureBox1.ImageLocation = Application.StartupPath + "/OJTI_2019_C#_resurse/sila_Biblioteca.jpg";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new login();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new signin();
            frm.Show();
            this.Hide();
        }
    }
}
