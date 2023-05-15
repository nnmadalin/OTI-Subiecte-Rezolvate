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

namespace OTI2022judet
{
    public partial class autentificare : Form
    {
        public autentificare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
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

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Poluare.mdf;Integrated Security=True;Connect Timeout=3; ";

        public static string username = "";

        void load_db()
        {
            string[] denumire = new string[100];
            int k = 0;
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("delete from Harti", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from Masurare", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident(Harti, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident(Masurare, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                using (StreamReader reader = new StreamReader(Application.StartupPath + @"\OJTI_2022_C#_Resurse\harti.txt"))
                {
                    string row;
                    while((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split('#');

                        cmd = new SqlCommand("insert into [Harti] values (@nume, @fisier)", conn);
                        cmd.Parameters.Add("@nume", split[0]);
                        cmd.Parameters.Add("@fisier", split[1]);
                        cmd.ExecuteNonQuery();

                        denumire[k++] = split[0];
                    }
                }

                using (StreamReader reader = new StreamReader(Application.StartupPath +  @"\OJTI_2022_C#_Resurse\masurari.txt"))
                {
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split('#');
                        string[] data_split = split[4].Split(new char[] { ' ', ':', '/'});
                        string data = data_split[1] + "/" + data_split[0] + "/" + data_split[2] + " " + data_split[3] + ":" + data_split[4];

                        for(int i = 0; i < k; i++)
                        {
                            if(denumire[i] == split[0])
                            {
                                cmd = new SqlCommand("insert into [Masurare] values (@id, @pozx, @pozy, @val, @data)", conn);
                                cmd.Parameters.Add("@id", i + 1);
                                cmd.Parameters.Add("@pozx", split[1]);
                                cmd.Parameters.Add("@pozy", split[2]);
                                cmd.Parameters.Add("@val", split[3]);
                                cmd.Parameters.Add("@data", data);
                                cmd.ExecuteNonQuery();

                                denumire[k++] = split[0];
                                break;
                            }
                        }


                       
                    }
                }

                conn.Close();
            }
        }

        private void autentificare_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Background/back14.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            load_db();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new inregistrare();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Utilizatori] where NumeUtilizator = @username and Parola = @pass", conn);
                cmd.Parameters.Add("@username", textBox1.Text);
                cmd.Parameters.Add("@pass", textBox2.Text);
                SqlDataReader read = cmd.ExecuteReader();
                try
                {
                    if (!read.HasRows)
                    {
                        MessageBox.Show("Nume de utilizator si/sau parola invalida!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        username = textBox1.Text;

                        var frm = new vizualizare();
                        frm.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            }

        }
    }
}
