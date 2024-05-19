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
using System.Data.SqlTypes;
using System.Data.Sql;
using System.IO;
using MessagingToolkit.QRCode.Codec.Data;

namespace OTI2023nationala
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Jocuri.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";

        void loadDataDB()
        {
            SqlConnection conn = new SqlConnection(db);
            conn.Open();

            SqlCommand cmd;
            SqlDataReader reader;

            StreamReader read = new StreamReader(Application.StartupPath + "/ONTI_2023_C#_Resurse/Utilizatori.txt");
            string line;
            string[] lineSplit;

            while((line = read.ReadLine()) != null)
            {
                lineSplit = line.Split(';');

                cmd = new SqlCommand("SELECT * FROM Utilizatori WHERE EmailUtilizator=@email", conn);
                cmd.Parameters.AddWithValue("@email", lineSplit[0]);
                reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    cmd = new SqlCommand("INSERT INTO Utilizatori VALUES (@email, @nume, @pass)", conn);
                    cmd.Parameters.AddWithValue("@email", lineSplit[0]);
                    cmd.Parameters.AddWithValue("@nume", lineSplit[1]);
                    cmd.Parameters.AddWithValue("@pasS", lineSplit[2]);
                    cmd.ExecuteNonQuery();
                }
            }

            read = new StreamReader(Application.StartupPath + "/ONTI_2023_C#_Resurse/Rezultate.txt");

            while ((line = read.ReadLine()) != null)
            {
                lineSplit = line.Split(';');

                cmd = new SqlCommand("SELECT * FROM Rezultate WHERE TipJoc=@id and EmailUtilizator=@email and PunctajJoc=@pct and Data=@data", conn);
                cmd.Parameters.AddWithValue("@id", lineSplit[0]);
                cmd.Parameters.AddWithValue("@email", lineSplit[1]);
                cmd.Parameters.AddWithValue("@pct", lineSplit[2]);
                cmd.Parameters.AddWithValue("@data", DateTime.ParseExact(lineSplit[3], "dd.MM.yyyy", null));
                reader = cmd.ExecuteReader();

                

                if (!reader.HasRows)
                {
                    cmd = new SqlCommand("INSERT INTO Rezultate VALUES (@id, @email, @pct, @data)", conn);
                    cmd.Parameters.AddWithValue("@id", lineSplit[0]);
                    cmd.Parameters.AddWithValue("@email", lineSplit[1]);
                    cmd.Parameters.AddWithValue("@pct", lineSplit[2]);
                    cmd.Parameters.AddWithValue("@data", DateTime.ParseExact(lineSplit[3], "dd.MM.yyyy", null));
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataDB();
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
            {
                this.Location = new Point(this.Location.X + e.Location.X - mouse.X, this.Location.Y + e.Location.Y - mouse.Y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                MessagingToolkit.QRCode.Codec.QRCodeDecoder objDecodare = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();
                string sirCodare = objDecodare.decode(new MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(pictureBox1.Image as Bitmap));

                string[] split = sirCodare.Split('\n');

                textBox1.Text = split[1];
                textBox2.Text = split[2];
            }
        }

        public static string emailUser, nameUser;

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Utilizatori] WHERE EmailUtilizator=@email and Parola=@parola", conn);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@parola", textBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Date de autentificare invalide", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear(); textBox2.Clear();
            }
            else
            {
                reader.Read();
                emailUser = reader[0].ToString();
                nameUser = reader[1].ToString();

                var frm = new AlegeJoc();
                frm.Show();
                this.Hide();
            }

            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new creareCont();
            frm.Show();
            this.Hide();
        }
    }
}
