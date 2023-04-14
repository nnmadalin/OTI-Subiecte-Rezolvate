using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014judet
{
    public partial class actiunile_mele : Form
    {
        public actiunile_mele()
        {
            InitializeComponent();
        }

        string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBBursa.mdf;Integrated Security=True;Connect Timeout=5";

        public static int lung = 0;
        public static int[] val_time = new int[1000000];

        void calc_table()
        {
            Random random = new Random();
            int r = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int val = random.Next(-4, 4);
                dataGridView1.Rows[i].Cells[4].Value = val;
                dataGridView1.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) + val;
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                dataGridView1.Rows[i].Cells[7].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value) - Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                r += Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
            }
            textBox1.Text = r.ToString();
            val_time[lung++] = r / 100;
        }

        

        private void actiunile_mele_Load(object sender, EventArgs e)
        {           
            SqlConnection sqlConnection = new SqlConnection(db);
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Actiuni]", sqlConnection);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                dataGridView1.Rows.Add( read["Denumire"].ToString(), read["NrActiuni"].ToString(), read["Valoare"].ToString(), read["Valoare"].ToString(), "", Convert.ToInt32(read["NrActiuni"].ToString()) * Convert.ToInt32(read["Valoare"].ToString()));
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            calc_table();
        }

        private void actiunile_mele_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (home.enable == true)
            {
                timer1.Interval = home.time;
                
                if(timer1.Enabled == false)
                    timer1.Start();
            }
            else
                timer1.Stop();
        }
    }
}
