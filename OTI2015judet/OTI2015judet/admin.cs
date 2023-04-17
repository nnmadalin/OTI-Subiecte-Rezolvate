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

namespace OTI2015judet
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void turisti_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Application.StartupPath + "/Resurse_C#/MareaNeagra.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: " + pictureBox1.PointToClient(Cursor.Position).X;
            label2.Text = "Y: " + pictureBox1.PointToClient(Cursor.Position).Y;
        }

        int start_click_coord = -1;
        Point[] porturi = new Point[13];
        public static string[] denumire = { "Constanta", "Varna", "Burgas", "Istambul", "Kozlu", "Samsun", 
            "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa" };

        private void button1_Click(object sender, EventArgs e)
        {
            start_click_coord = 0;
            MessageBox.Show("Apasa pe fiecare cerc (in sens invers ceasornic) pentru a seta coordonatele, incepand cu portul Constanta!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (start_click_coord > 12)
                start_click_coord = -1;
            if(start_click_coord != -1)
            {
                porturi[start_click_coord] = pictureBox1.PointToClient(Cursor.Position);
                MessageBox.Show("Coordonate salvate temoporar pentru portul cu numele " + (denumire[start_click_coord]),
                    "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (start_click_coord == 12)
                {
                    MessageBox.Show("Coordonate salvate temporar! Pentru a salva in baza de date noile coordonate, apasa pe butonul salvare coordonate!", 
                        "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    start_click_coord = -2;
                }
                start_click_coord++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(home.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("delete from [Porturi]", conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("DBCC Checkident (Porturi, RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            for (int i = 0; i < denumire.Length; i++)
            {
                cmd = new SqlCommand("insert into [Porturi] values (@nume, @poz_x, @poz_y)", conn);
                cmd.Parameters.Add("@nume", denumire[i]);
                cmd.Parameters.Add("@poz_x", porturi[i].X);
                cmd.Parameters.Add("@poz_y", porturi[i].Y);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Coordonate salvate cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(home.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("delete from [Distante]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident (Distante, RESEED, 0)", conn);
                cmd.ExecuteNonQuery();

                StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/Harta_Distantelor.txt");
                string line;
                int k = 1;
                while ((line = read.ReadLine()) != null)
                {
                    string[] cols = line.Split(' ');
                    for (int i = 0; i < denumire.Length; i++)
                    {
                        cmd = new SqlCommand("insert into [Distante] values (@id_aici, @id, @nume, @dist)", conn);
                        cmd.Parameters.Add("@id_aici", k);
                        cmd.Parameters.Add("@id", (i + 1));
                        cmd.Parameters.Add("@nume", denumire[i]);
                        cmd.Parameters.Add("@dist", cols[i]);

                        cmd.ExecuteNonQuery();
                    }
                    k++;
                }
                MessageBox.Show("Actualizare distante - succes", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Eroare");
            }
        }

        int[] a = new int[20];
        int[] v = new int[20];

        int n = 3;

        int calc_s(int start, int finish)
        {
            SqlConnection conn = new SqlConnection(home.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from [Distante] where ID_Port = @idport and ID_Port_Destinatie = @idportdest", conn);
            cmd.Parameters.Add("@idport", start);
            cmd.Parameters.Add("@idportdest", finish);
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();


            int s = 0;
            s = Convert.ToInt32(read["Distanta"].ToString());      
            conn.Close();
            return s;

            
        }

        void add_db(int s)
        {
            string calatorie = "1, ";
            for (int i = 0; i < n; i++)
            {
                calatorie += (v[i].ToString() + ", ");
            }
            calatorie += "1";

            SqlConnection conn = new SqlConnection(home.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Croaziere values (@tip, @lista, @start, @finish, @pret, @numar)", conn);
            cmd.Parameters.Add("@tip", (n + 1));
            cmd.Parameters.Add("@lista", calatorie);
            cmd.Parameters.Add("@start", DBNull.Value);
            cmd.Parameters.Add("@finish", DBNull.Value);
            cmd.Parameters.Add("@pret", (s * 2));
            cmd.Parameters.Add("@numar", DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        void check()
        {
            int s = 0;

            s += calc_s(1, v[0]);
            s += calc_s(v[n - 1], 1);

            for (int i = 1; i < n; i++)
            {
                s += calc_s(v[i - 1], v[i]);
            }
            if(n == 2)
            {
                if (s <= 1100 && s >= 800)
                {
                    add_db(s);
                }
                            
            }
            else if(n == 4)
            {
                if (s <= 1600 && s >= 800)
                {
                    add_db(s);
                }
                
            }
            else if (n == 7)
            {
                if (s <= 1900 && s >= 800)
                {
                    add_db(s);
                }

            }
        }

        int back(int k, int pred)
        {
            for(int i = pred; i <= denumire.Length; i++)
            {
                if(a[i] == 0)
                {
                    a[i] = 1;
                    v[k] = i;

                    if(k < n - 1)
                    {
                        back(k + 1, i + 1);
                    }
                    else
                    {
                        a[i] = 0;
                        check();
                    }
                    a[i] = 0;

                }
            }
            return 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(home.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("delete from [Croaziere]", conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("DBCC Checkident (Croaziere, RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            ///3 zile
            n = 2;
            back(0, 2);

            n = 4;
            back(0, 2);

            n = 7;
            back(0, 2);

            


            MessageBox.Show("Croaziere generate cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home.q = 2;
            this.Hide();
        }
    }
}
