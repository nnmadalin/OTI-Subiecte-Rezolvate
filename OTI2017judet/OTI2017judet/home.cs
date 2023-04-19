using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace OTI2017judet
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Turism.mdf;Integrated Security=True;MultipleActiveResultSets=true;Connect Timeout=3";

        void add_localitate(string nume)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT into [Localitati] values (@nume)", conn);
                cmd.Parameters.Add("@nume", nume);
                cmd.ExecuteNonQuery();
                
                conn.Close();
            }
        }

        void add_planificare(int IDLocalitate, string Frecventa, string DataStart, string DataStop, string Ziua)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                if (Frecventa == "ocazional")
                {

                    SqlCommand cmd = new SqlCommand("INSERT into [Planificari] values (@IDLocalitate, @Frecventa, @DataStart, @DataStop, @Ziua)", conn);
                    cmd.Parameters.Add("@IDLocalitate", IDLocalitate);
                    cmd.Parameters.Add("@Frecventa", Frecventa);

                    string[] split = DataStart.Split('.');
                    DataStart = split[1] + "." + split[0] + "." + split[2];

                    split = DataStop.Split('.');
                    DataStop = split[1] + "." + split[0] + "." + split[2];

                    DateTime start = Convert.ToDateTime(DataStart);
                    DateTime finish = Convert.ToDateTime(DataStop);

                    cmd.Parameters.Add("@DataStart", start);
                    cmd.Parameters.Add("@DataStop", finish);
                    cmd.Parameters.Add("@Ziua", DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                else 
                {

                    SqlCommand cmd = new SqlCommand("INSERT into [Planificari] values (@IDLocalitate, @Frecventa, @DataStart, @DataStop, @Ziua)", conn);
                    cmd.Parameters.Add("@IDLocalitate", IDLocalitate);
                    cmd.Parameters.Add("@Frecventa", Frecventa);
                    cmd.Parameters.Add("@DataStart", DBNull.Value);
                    cmd.Parameters.Add("@DataStop", DBNull.Value);
                    cmd.Parameters.Add("@Ziua", Ziua);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
        void add_imagine(int IDLocalitate, string path)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT into [Imagini] values (@IDLocalitate, @CaleFisier)", conn);
                cmd.Parameters.Add("@IDLocalitate", IDLocalitate);
                cmd.Parameters.Add("@CaleFisier", path);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        void db_delete()
        {
            using(SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE from [Localitati]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident (Localitati, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE from [Imagini]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident (Imagini, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE from [Planificari]", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DBCC Checkident (Planificari, Reseed, 0)", conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public static string path_image = Application.StartupPath + "/Resurse/Imagini/";

        private void button2_Click(object sender, EventArgs e)
        {
            int k = 1;
            db_delete();
            using (StreamReader read = new StreamReader(Application.StartupPath + "/Resurse/planificari.txt"))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] split = line.Split('*');
                    add_localitate(split[0].ToString().Trim());
                    int poz = 0;
                    if (split[1].ToString().Trim() == "ocazional")
                    {
                        add_planificare(k, split[1].ToString().Trim(), split[2].ToString().Trim(), split[3].ToString().Trim(), null);
                        poz = 4;
                    }
                    else
                    {
                        add_planificare(k, split[1].ToString().Trim(), null, null, split[2].ToString().Trim());
                        poz = 3;
                    }

                    for (int i = poz; i < split.Length; i++)
                    {
                        add_imagine(k, split[i].ToString().Trim());
                    }
                    k++;

                }
            }

            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                path_image = folderBrowserDialog1.SelectedPath.ToString();
            }

            MessageBox.Show("Initilizare cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var home = new genereaza_poster();
            home.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var home = new visualizare_excursie();
            home.Show();
            this.Hide();
        }
    }
}
