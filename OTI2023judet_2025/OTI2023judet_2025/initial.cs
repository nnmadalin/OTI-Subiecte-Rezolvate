using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace OTI2023judet_2025
{
    public partial class initial : Form
    {
        public initial()
        {
            InitializeComponent();
        }

        public static string dbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\JocEducativ.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";
        public static string numeUser = "", emailuser = "";
        private void initial_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbConnection);
            conn.Open();
            SqlCommand cmd;

            StreamReader reader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Itemi.txt");
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] split = line.Split(';');
                cmd = new SqlCommand("SELECT * FROM [Itemi] WHERE EnuntItem = @enunt", conn);
                cmd.Parameters.Add("@enunt", split[1]);
                SqlDataReader readerSql = cmd.ExecuteReader();

                if (!readerSql.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [Itemi] values(@enunt, @r1, @r2, @r3, @rc, @p)", conn);
                    cmd2.Parameters.Add("@enunt", split[1]);
                    cmd2.Parameters.Add("@r1", split[2]);
                    cmd2.Parameters.Add("@r2", split[3]);
                    cmd2.Parameters.Add("@r3", split[4]);
                    cmd2.Parameters.Add("@rc", split[5]);
                    cmd2.Parameters.Add("@p", split[6]);
                    cmd2.ExecuteNonQuery();

                }
            }

            reader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Rezultate.txt");
            while ((line = reader.ReadLine()) != null)
            {
                string[] split = line.Split(';');
                cmd = new SqlCommand("SELECT * FROM [Rezultate] WHERE TipJoc = @tip and EmailUtilizator= @email and PunctajJoc = @pct", conn);
                cmd.Parameters.Add("@tip", split[1]);
                cmd.Parameters.Add("@email", split[2]);
                cmd.Parameters.Add("@pct", split[3]);
                SqlDataReader readerSql = cmd.ExecuteReader();

                if (!readerSql.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [Rezultate] values(@tip, @email, @pct)", conn);
                    cmd2.Parameters.Add("@tip", split[1]);
                    cmd2.Parameters.Add("@email", split[2]);
                    cmd2.Parameters.Add("@pct", split[3]);
                    cmd2.ExecuteNonQuery();

                }
            }

            reader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Utilizatori.txt");
            while ((line = reader.ReadLine()) != null)
            {
                string[] split = line.Split(';');
                cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator = @email", conn);
                cmd.Parameters.Add("@email", split[0]);
                SqlDataReader readerSql = cmd.ExecuteReader();

                if (!readerSql.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [Utilizatori] values(@email, @nume, @parola)", conn);
                    cmd2.Parameters.Add("@email", split[0]);
                    cmd2.Parameters.Add("@nume", split[1]);
                    cmd2.Parameters.Add("@parola", split[2]);
                    cmd2.ExecuteNonQuery();

                }
            }

            conn.Close();


            this.Hide();
            Autentificare frm = new Autentificare();
            frm.Show();
           

        }
    }
}
