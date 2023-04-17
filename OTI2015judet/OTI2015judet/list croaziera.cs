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

namespace OTI2015judet
{
    public partial class list_croaziera : Form
    {
        public list_croaziera()
        {
            InitializeComponent();
        }

        string get_db(int i)
        {
            return admin.denumire[i - 1];
        }

        void load_db()
        {
            if(comboBox1.SelectedIndex == 1)
            {
                SqlConnection conn = new SqlConnection(home.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Croaziere] where Tip_Croaziera = 5", conn);
                var datatable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                SqlConnection conn = new SqlConnection(home.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Croaziere] where Tip_Croaziera = 8", conn);
                var datatable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
            }
            else
            {
                SqlConnection conn = new SqlConnection(home.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Croaziere] where Tip_Croaziera = 3", conn);
                var datatable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(datatable);
                dataGridView1.DataSource = datatable;
            }

            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                string cell = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string r = "";
                string[] spart = cell.Split(',');
                for(int j = 0; j < spart.Length; j++)
                {
                    int k = Convert.ToInt32(spart[j]);
                    r += (admin.denumire[k - 1] + ", ");
                }
                dataGridView1.Rows[i].Cells[2].Value = r;
            }

        }

        private void list_croaziera_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            load_db();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_db();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            home.q = 1;
            this.Hide();
        }
    }
}
