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

namespace OTI2018nationala
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CentenarDB.mdf;Integrated Security=True;Connect Timeout=5; MultipleActiveResultSets = true";

        private void Form1_Load(object sender, EventArgs e)
        {
            using(SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();

                using(StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/utilizatori.txt"))
                {
                    string row;
                    while ((row = read.ReadLine()) != null)
                    {
                        string[] split = row.Split('*');
                        SqlCommand cmd;

                        cmd = new SqlCommand("select * from Utilizatori where Nume = @nume and Parola = @parola and Email = @email", conn);
                        cmd.Parameters.Add("@nume", split[0]);
                        cmd.Parameters.Add("@parola", split[1]);
                        cmd.Parameters.Add("@email", split[2]);
                        SqlDataReader readsql = cmd.ExecuteReader();
                        if (!readsql.HasRows)
                        {
                            cmd = new SqlCommand("insert into Utilizatori values (@nume, @parola, @email)", conn);
                            cmd.Parameters.Add("@nume", split[0]);
                            cmd.Parameters.Add("@parola", split[1]);
                            cmd.Parameters.Add("@email", split[2]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                using (StreamReader read = new StreamReader(Application.StartupPath + "/Resurse_C#/lectii.txt"))
                {
                    string row;
                    while ((row = read.ReadLine()) != null)
                    {
                        string[] split = row.Split('*');
                        SqlCommand cmd;

                        cmd = new SqlCommand("select * from Lectii where IdUtilizator = @id and TitluLectie = @titlu and Regiune = @reg and DataCreare = @data and NumeImagine = @img", conn);
                        cmd.Parameters.Add("@id", split[0]);
                        cmd.Parameters.Add("@titlu", split[1]);
                        cmd.Parameters.Add("@reg", split[2]);
                        cmd.Parameters.Add("@img", split[3]);
                        cmd.Parameters.Add("@data", split[4]);
                        SqlDataReader readsql = cmd.ExecuteReader();
                        if (!readsql.HasRows)
                        {
                            cmd = new SqlCommand("insert into Lectii values (@id, @titlu, @reg, @data, @img)", conn);
                            cmd.Parameters.Add("@id", split[0]);
                            cmd.Parameters.Add("@titlu", split[1]);
                            cmd.Parameters.Add("@reg", split[2]);
                            cmd.Parameters.Add("@img", split[3]);
                            cmd.Parameters.Add("@data", split[4]);
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

        Point mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                this.Location = this.Location + (Size)e.Location - (Size)mouse;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new VizualizareLectii();
            frm.ShowDialog();
        }

        public static bool auth = false;

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new autentificare();
            frm.ShowDialog();

            if(auth == true)
            {
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new CreareLectie();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new ghiceste_regiunea();
            frm.ShowDialog();
        }
    }
}
