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

namespace OTI2019nationala
{
    public partial class LogareBiblioteca : Form
    {
        public LogareBiblioteca()
        {
            InitializeComponent();
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
            if (e.Button == MouseButtons.Left)
            {
                this.Location = this.Location + (Size)e.Location - (Size)_mouse;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new startBiblioteca();
            frm.Show();
            this.Hide();
        }

        public static string id, numeprenume, email;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pass = startBiblioteca.criptare(textBox2.Text);
            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(string.Format("select * from Utilizatori where TipUtilizator = '1' and Email = '{0}' and Parola = '{1}'", textBox1.Text, pass), conn);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    read.Read();
                    id = read["IdUtilizator"].ToString();
                    numeprenume = read["NumePrenume"].ToString();
                    email = textBox1.Text;
                    var frm = new BibliotecarBiblioteca();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email si/ sau parola invalida!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
        }
    }
}
