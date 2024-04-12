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

namespace OTI2023judet
{
    public partial class Ghiceste : Form
    {
        public Ghiceste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new AlegeJoc();
            frm.Show();
            this.Hide();
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

        int stadiuFloare = 6, litereGresite = 0, listaCuvinteLenght, litereTotal=0;
        string cuvant;
        bool[] enableLetter = new bool[100];
        string[] listaCuvinte = new string[100];

        void add_DB()
        {
            using(SqlConnection conn =  new SqlConnection(Form1.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@tip, @email, @punctaj)", conn);
                cmd.Parameters.AddWithValue("@tip", "0");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@punctaj", (100 - 4 * litereGresite).ToString());
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            var frm = new AlegeJoc();
            frm.Show();
            this.Hide();
        }

        private void letterClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Visible = false;

            label3.Text = "";

            bool litGasita = false;
            int litereGasite = 0;

            for(int i = 0; i < cuvant.Length; i++)
            {
                if(enableLetter[i] == true)
                {
                    label3.Text += cuvant[i].ToString().ToUpper();
                }
                else
                {
                    if(cuvant[i].ToString().ToUpper() == btn.Text.ToUpper())
                    {
                        enableLetter[i] = true;
                        litGasita = true;
                        label3.Text += cuvant[i].ToString().ToUpper();
                    }
                    else
                    {
                        label3.Text += "_";
                    }
                }

                if (enableLetter[i] == true)
                    litereGasite++;

                if (i != cuvant.Length - 1)
                    label3.Text += "  ";
            }
            litereTotal++;
            if (litGasita == false)
            {
                stadiuFloare--;
                litereGresite++;
            }
            else
            {
                if(stadiuFloare < 5)
                    stadiuFloare++;
            }

            if (litereGasite == cuvant.Length)
            {
                label2.Text = "Punctaj: " + (100 - 4 * litereGresite).ToString();
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/" + stadiuFloare.ToString() + ".png");
                MessageBox.Show("Ai CASTIGAT!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

                add_DB();
            }
            else
            {

                if (stadiuFloare == 1 || litereTotal == 26)
                {
                    label2.Text = "Punctaj: 0";

                    MessageBox.Show("Ai pierdut! :(", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/1.png");

                    add_DB();
                }
                else
                {
                    label2.Text = "Punctaj: " + (100 - 4 * litereGresite).ToString();
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/" + stadiuFloare.ToString() + ".png");
                }
            }
        }

        private void Ghiceste_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/OJTI_2023_C#_Resurse/StadiiFloare/6.png");


            using(StreamReader reader = new StreamReader(Application.StartupPath + "/OJTI_2023_C#_Resurse/Cuvinte.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    listaCuvinte[listaCuvinteLenght++] = line;
                }
            }

            Random random = new Random(DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Second + DateTime.Now.Minute + DateTime.Now.Hour + DateTime.Now.Day);


            cuvant = listaCuvinte[random.Next(0, listaCuvinteLenght)];
            enableLetter = new bool[cuvant.Length];

            label3.Text = "";

            for (int i = 0; i < cuvant.Length - 1; i++)
            {
                label3.Text += "_   ";
            }
            label3.Text += "_";

            label2.Text = "Punctaj: 100";
        }
    }
}
