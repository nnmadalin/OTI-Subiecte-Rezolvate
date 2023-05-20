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

namespace OTI2019nationala
{
    public partial class startBiblioteca : Form
    {
        public startBiblioteca()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=5; MultipleActiveResultSets = true";

        public static string criptare(string pass)
        {           
            string new_pass = " ";

            for(int i = 0; i < pass.Length; i++)
            {
                if(pass[i] >= 'a' && pass[i] <= 'z')
                {
                    if (pass[i] != 'z')
                        new_pass += Convert.ToChar(((int)pass[i] + 1));
                    else
                        new_pass += 'a';
                }
                else if (pass[i] >= 'A' && pass[i] <= 'Z')
                {
                    if (pass[i] != 'A')
                        new_pass += Convert.ToChar(((int)pass[i] - 1));
                    else
                        new_pass += 'Z';
                }
                else if (pass[i] >= '0' && pass[i] <= '9')
                {
                    int x = Convert.ToInt32(pass[i].ToString());
                    x = 9 - x;
                    new_pass += x.ToString();
                }
                else
                    new_pass += pass[i];
            }

            return new_pass;
        }

        void incarca_fisiere()
        {
           
            using(SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();
                SqlCommand cmd;
                using (StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse/utilizatori.txt"))
                {
                    string row;
                    while((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split(';');

                        cmd = new SqlCommand("select * from Utilizatori where  Email = @email", conn);
                        cmd.Parameters.Add("@email", split[2]);
                        SqlDataReader read = cmd.ExecuteReader();

                        if (!read.HasRows)
                        {
                            cmd = new SqlCommand("insert into Utilizatori values (@tip, @nume, @email, @parola)", conn);
                            cmd.Parameters.Add("@tip", split[0]);
                            cmd.Parameters.Add("@nume", split[1]);
                            cmd.Parameters.Add("@email", split[2]);
                            cmd.Parameters.Add("@parola", criptare(split[3]));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
                cmd = new SqlCommand("delete from Carti", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from Rezervari", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from Imprumuturi", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DBCC checkident(Carti, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC checkident(Rezervari, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC checkident(Imprumuturi, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                using (StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse/carti.txt"))
                {
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split(';');

                        cmd = new SqlCommand("insert into Carti values (@titlu, @autor, @nr)", conn);
                        cmd.Parameters.Add("@titlu", split[0]);
                        cmd.Parameters.Add("@autor", split[1]);
                        cmd.Parameters.Add("@nr", split[2]);
                        cmd.ExecuteNonQuery();
                    }
                }

                using (StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse/imprumuturi.txt"))
                {
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split(';');
                        string newsp = "", newsp2 = "";

                        for(int i = 0, k = 0; i < split[2].Length; i++)
                        {
                            if (split[2][i] == '/')
                                k++;
                            if (k > 2 && split[2][i] == '/')
                                newsp += ':';
                            else
                                newsp += split[2][i];
                        }

                        for (int i = 0, k = 0; i < split[3].Length; i++)
                        {
                            if (split[3][i] == '/')
                                k++;
                            if (k > 2 && split[3][i] == '/')
                                newsp2 += ':';
                            else
                                newsp2 += split[3][i];
                        }

                        cmd = new SqlCommand("insert into Imprumuturi values (@idc, @idc2, @di, @dr)", conn);
                        cmd.Parameters.Add("@idc", split[0]);
                        cmd.Parameters.Add("@idc2", split[1]);
                        cmd.Parameters.Add("@di", newsp);                        
                        if(split[3] != "NULL")
                            cmd.Parameters.Add("@dr", newsp2);
                        else
                            cmd.Parameters.Add("@dr", DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                using (StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse/rezervari.txt"))
                {
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        string[] split = row.Split(';');

                        string newsp = "";

                        for (int i = 0, k = 0; i < split[2].Length; i++)
                        {
                            if (split[2][i] == '/')
                                k++;
                            if (k > 2 && split[2][i] == '/')
                                newsp += ':';
                            else
                                newsp += split[2][i];
                        }


                        cmd = new SqlCommand("insert into Rezervari values (@idc, @idc2, @di, @dr)", conn);
                        cmd.Parameters.Add("@idc", split[0]);
                        cmd.Parameters.Add("@idc2", split[1]);
                        cmd.Parameters.Add("@di", newsp);
                        cmd.Parameters.Add("@dr", split[3]);
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            incarca_fisiere();

            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"/Resurse/Imagini/altele/img11.jpg");
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
                this.Location = this.Location + (Size)e.Location - (Size)_mouse;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new LogareBiblioteca();
            frm.Show();
            this.Hide();
        }
    }
}
