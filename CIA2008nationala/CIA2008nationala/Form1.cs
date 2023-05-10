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

namespace CIA2008nationala
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //size : 752, 349
        //location: 12, 69

        string dbstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Proiecte.mdf;Integrated Security=True;Connect Timeout=5; MultipleActiveResultSets = true";

        private void Form1_Load(object sender, EventArgs e)
        {
            make_invisible();

            SqlConnection conn = new SqlConnection(dbstring);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [Proiecte]", conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView_tabel.DataSource = dataTable;
            dataGridView_cautat.DataSource = dataTable;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void make_invisible()
        {
            panel_text.Visible = false;
            panel_tabel.Visible = false;
            panel_cautare.Visible = false;
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            make_invisible();

            panel_text.Size = new Size(752, 349);
            panel_text.Location = new Point(12, 69);
            panel_text.Visible = true;        
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "Textul introdus este: " + textBox1.Text;
        }

        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void tabelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            make_invisible();

            panel_tabel.Size = new Size(752, 349);
            panel_tabel.Location = new Point(12, 69);
            panel_tabel.Visible = true;
        }

        private void cautareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            make_invisible();

            panel_cautare.Size = new Size(752, 349);
            panel_cautare.Location = new Point(12, 69);
            panel_cautare.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";

            SqlConnection conn = new SqlConnection(dbstring);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [Proiecte] where NrOrdine = @id", conn);
            cmd.Parameters.Add("@id", numericUpDown1.Value);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                textBox3.Text = reader[1].ToString();
            }
            else
            {
                MessageBox.Show("NrOrdine gresit!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conn.Close();
        }
    }
}
