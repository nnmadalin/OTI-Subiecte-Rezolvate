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

namespace OTI2013judet
{
    public partial class clasament : Form
    {
        public clasament()
        {
            InitializeComponent();
        }

        private void clasament_Load(object sender, EventArgs e)
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

        private void clasament_FormClosing(object sender, FormClosingEventArgs e)
        {
            var home = new normal();
            home.Show();
            this.Hide();
        }
    }
}
