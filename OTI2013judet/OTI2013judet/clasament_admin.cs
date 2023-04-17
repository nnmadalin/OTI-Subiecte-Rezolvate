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

namespace OTI2013judet
{
    public partial class clasament_admin : Form
    {
        public clasament_admin()
        {
            InitializeComponent();
        }

        private void clasament_admin_Load(object sender, EventArgs e)
        {
            string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Clasament.mdf;Integrated Security=True;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from [Table]", conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sqlDataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.Refresh();
        }

        private void clasament_admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            var home = new normal();
            home.Show();
            this.Hide();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Clasament.mdf;Integrated Security=True;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("delete from [Table]", conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("DBCC Checkident ([Table], RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                cmd = new SqlCommand("insert into [Table] values (@nume, @timp, @patrate)", conn);
                cmd.Parameters.Add("@nume", dataGridView1.Rows[i].Cells[1].Value);
                cmd.Parameters.Add("@timp", dataGridView1.Rows[i].Cells[2].Value);
                cmd.Parameters.Add("@patrate", dataGridView1.Rows[i].Cells[3].Value);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
