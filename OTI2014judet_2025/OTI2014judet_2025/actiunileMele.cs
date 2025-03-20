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
using System.Data.SqlTypes;

namespace OTI2014judet_2025
{
    public partial class actiunileMele : Form
    {
        public actiunileMele()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        string dbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBBursa.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";

        private void actiunileMele_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(dbString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Table]", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                dataGridView1.Rows.Add(reader[1], reader[2], reader[3]);
            }

            conn.Close();
            timer1.Start();
        }

        public static int suma = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            suma = 0;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[4].Value = Form1.randomVal[i];
                dataGridView1.Rows[i].Cells[3].Value = (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) + Form1.randomVal[i]);
                dataGridView1.Rows[i].Cells[5].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                dataGridView1.Rows[i].Cells[7].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value) - Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                suma += Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
            }
            textBox1.Text = Convert.ToString(suma);
        }
    }
}
