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
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using System.Collections.Specialized;

namespace OTI2015judet_2025
{
    public partial class Administrare : Form
    {
        public Administrare()
        {
            InitializeComponent();
        }

        private void Administrare_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Administrare_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/MareaNeagra.jpg");
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouse = pictureBox1.PointToClient(Cursor.Position);

            label2.Text = "X = " + (mouse.X).ToString();
            label3.Text = "Y = " + (mouse.Y).ToString();
        }

        public static string[] Ports = { "Constanta", "Varna", "Burgas", "Instambul", "Kozlu", "Samsun", "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa" };
        Point[] locationPorts = new Point[100];
        int lenghtLocationPorts = 0;
        bool statusSelectedLocationPorts = false;
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult status = MessageBox.Show("Esti sigura ca vrei sa incepi selectarea?", "Informare", MessageBoxButtons.OKCancel);

            if(status == DialogResult.OK)
            {
                statusSelectedLocationPorts = true;
                lenghtLocationPorts = 0;
                MessageBox.Show("Poti incepe sa selectezi porturile!");
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(statusSelectedLocationPorts == true && lenghtLocationPorts < 13)
            {
                locationPorts[lenghtLocationPorts] = e.Location;
                lenghtLocationPorts++;
                MessageBox.Show("Memorat!");
                if (lenghtLocationPorts == 13)
                {
                    statusSelectedLocationPorts=false;
                    MessageBox.Show("Ai terminat de selectat toate porturile!");
                }
            }
        }

        public static string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";

        private void button2_Click(object sender, EventArgs e)
        {
            if(statusSelectedLocationPorts ==  false)
            {
                SqlConnection conn = new SqlConnection(connection);
                conn.Open();

                SqlCommand cmd;

                cmd = new SqlCommand("DELETE from Porturi", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC checkident (Porturi, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                for(int i = 0; i < 13; i++)
                {
                    cmd = new SqlCommand("INSERT INTO Porturi values (@nume, @pozx, @pozy)", conn);
                    cmd.Parameters.Add("@nume", Ports[i]);
                    cmd.Parameters.Add("@pozx", locationPorts[i].X);
                    cmd.Parameters.Add("@pozy", locationPorts[i].Y);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

                MessageBox.Show("Locatiile au fost salvate!");

            }
            else
            {
                MessageBox.Show("Termina mai intai de selectat toate porturile!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd;

            cmd = new SqlCommand("DELETE from Distante", conn);
            cmd.ExecuteNonQuery();

            StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse_C#/Harta_Distantelor.txt");
            string line;
            int k = 1;
            while((line = reader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split(' ');
                for(int i = 0; i < lineSplit.Length; i++)
                {
                    if (lineSplit[i] != "0")
                    {
                        cmd = new SqlCommand("INSERT INTO Distante values (@idP, @idD, @nume, @dist)", conn);
                        cmd.Parameters.Add("@idP", k);
                        cmd.Parameters.Add("@idD", i + 1);
                        cmd.Parameters.Add("@nume", Ports[i]);
                        cmd.Parameters.Add("@dist", Convert.ToInt32(lineSplit[i]));
                        cmd.ExecuteNonQuery();
                    }
                }
                k++;
            }


            conn.Close();

            MessageBox.Show("Distantiile au fost salvate!");

        }

        int[] a = new int[20];
        int[] v = new int[20];
        int n = 3;

        int calculDistanta(int start, int finish)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();


            SqlCommand cmd = new SqlCommand("select * from [Distante] where ID_Port = @idport and ID_Port_Destinatie = @idportdest", conn);
            cmd.Parameters.Add("@idport", start);
            cmd.Parameters.Add("@idportdest", finish);
            SqlDataReader read = cmd.ExecuteReader();
            read.Read();

            int s = Convert.ToInt32(read[4].ToString());
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

            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Croaziere values (@tip, @lista, @start, @finish, @pret, @numar)", conn);
            cmd.Parameters.Add("@tip", n+1);
            cmd.Parameters.Add("@lista", calatorie);
            cmd.Parameters.Add("@start", DBNull.Value);
            cmd.Parameters.Add("@finish", DBNull.Value);
            cmd.Parameters.Add("@pret", (s * 2));
            cmd.Parameters.Add("@numar", DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        void checkCroaziara()
        {
            //for(int i = 0; i < n; i++)
            //{
            //    Console.Write(v[i] + " ");
            //}
            //Console.WriteLine("");

            int s = 0;
            s += calculDistanta(1, v[0]);
            s += calculDistanta(v[n - 1], 1);

            for (int i = 1; i < n; i++)
                s += calculDistanta(v[i - 1], v[i]);
            //Console.WriteLine(s);
            if(n == 2 && s <= 1100 && s >= 800)
            {
                add_db(s);
            }
            else if (n == 4 && s <= 1600 && s >= 800)
            {
                add_db(s);
            }
            else if  (n == 7 && s <= 1900 && s >= 800)
            {
                add_db(s);
            }


        }

        void generareRecursiv(int k, int predecesor)
        {
            for(int i = predecesor; i <= Ports.Length; i++)
            {
                a[i] = 0;
                v[k] = i;
                if(k < n - 1)
                {
                    generareRecursiv(k + 1, i + 1);
                }
                else
                {
                    a[i] = 0;
                    checkCroaziara();
                }
                a[i] = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("delete from [Croaziere]", conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("DBCC Checkident (Croaziere, RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            ///3 zile
            n = 2;
            generareRecursiv(0, 2);

            n = 4;
            generareRecursiv(0, 2);

            n = 7;
            generareRecursiv(0, 2);



            MessageBox.Show("Croaziere generate cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ListaCroaziere frm = new ListaCroaziere();
            frm.ShowDialog();
        }
    }
}
