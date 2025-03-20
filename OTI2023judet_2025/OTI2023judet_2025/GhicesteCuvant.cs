using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2023judet_2025
{
    public partial class GhicesteCuvant : Form
    {
        public GhicesteCuvant()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        string[] cuvs = new string[100];
        string[] pozGasite = new string[50];
        int lenghtCuvs = 0, randomCuvAles = 0, stadiuFloare = 6, nrLitereGresit = 0, nrLitere = 0, nrLitereRamase = 0;

        int verificareFinalizareJoc()
        {
            if(100 - 4 * nrLitereGresit <= 0)
                return 1;
            if (stadiuFloare == 1)
                return 1;
            if (nrLitereRamase <= 0)
                return 2;

            return 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Visible = false;

            bool ok = false;
            for (int i = 0; i < nrLitere; i++)
            {
                if (cuvs[randomCuvAles][i].ToString().ToLower() == btn.Text.ToLower())
                {
                    pozGasite[i] = btn.Text;
                    nrLitereRamase--;
                    ok = true;
                }
            }

            if (ok == false)
            {
                nrLitereGresit++;
                stadiuFloare--;
            }
            else if (stadiuFloare < 6)
                stadiuFloare++;

            generareCuvant();
            updateImage();

            label3.Text = "Punctaj: " + (100 - 4 * nrLitereGresit).ToString();

            if (verificareFinalizareJoc() == 1)
            {
                MessageBox.Show("Ai pierdut", "Ai pierdut!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                addInDB();
                this.Close();
            }
            else if (verificareFinalizareJoc() == 2)
            {
                MessageBox.Show("Ai CASTIGAT", "Ai CASTIGAT!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                addInDB();
                this.Close();

            }
        }

        void addInDB()
        {
            SqlConnection conn = new SqlConnection(initial.dbConnection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] values(0, @email, @pct)", conn);
            cmd.Parameters.AddWithValue("@email", initial.emailuser);
            cmd.Parameters.AddWithValue("@pct", (100 - 4 * nrLitereGresit));
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        void updateImage()
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/" + stadiuFloare.ToString() + ".png");

        }

        void generareCuvant()
        {
            textBox1.Text = "";
            for(int i = 0; i < nrLitere; i++)
            {
                if (pozGasite[i] == "" || pozGasite[i] == null)
                {
                    textBox1.Text += "_";
                }
                else
                {
                    textBox1.Text += pozGasite[i];
                }

                if (i != nrLitere - 1)
                    textBox1.Text += " ";
            }
        }

        private void GhicesteCuvant_Load(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Cuvinte.txt");
            string line;
            while((line = reader.ReadLine()) != null)
            {
                cuvs[lenghtCuvs++] = line;
            }

            Random rand = new Random();
            randomCuvAles = rand.Next(0, lenghtCuvs);

            nrLitere = cuvs[randomCuvAles].Length;
            nrLitereRamase = cuvs[randomCuvAles].Length;

            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/" + stadiuFloare.ToString() + ".png");
            generareCuvant();
        }
    }
}
