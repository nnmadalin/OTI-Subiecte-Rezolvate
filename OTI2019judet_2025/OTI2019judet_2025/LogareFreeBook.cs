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

namespace OTI2019judet_2025
{
    public partial class LogareFreeBook : Form
    {
        public LogareFreeBook()
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
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [utilizatori] WHERE email = @email and parola = @pass", conn);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                textBox1.Clear(); textBox2.Clear();
                MessageBox.Show("Eroare autentificare", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                classStrings.emailUser = reader[0].ToString();
                classStrings.numeUser = reader[2].ToString();

                MeniuFreeBook frm = new MeniuFreeBook();
                frm.Show();
                this.Hide();

            }

            conn.Close();
        }
    }
}
