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
    public partial class normal : Form
    {
        public normal()
        {
            InitializeComponent();
        }

        void load_db()
        {
            if (comboBox1.SelectedIndex == 1)
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

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string cell = dataGridView1.Rows[i].Cells[2].Value.ToString();
                arr[i] = cell;
                string r = "";
                string[] spart = cell.Split(',');
                for (int j = 0; j < spart.Length; j++)
                {
                    int k = Convert.ToInt32(spart[j]);
                    r += (admin.denumire[k - 1] + ", ");
                }
                dataGridView1.Rows[i].Cells[2].Value = r;
            }
            
        }

        private void normal_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            load_db();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_db();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        public static string[] arr = new string[1000];
        public static int id = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(home.db);
            conn.Open();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                SqlCommand cmd = new SqlCommand("update Croaziere set Data_Start = @Data_Start, Data_Final = @Data_Final, Nr_Pasageri = @Nr_Pasageri where ID_Croaziera = @id", conn);
                cmd.Parameters.Add("@Data_Start", dataGridView1.Rows[i].Cells[3].Value);
                cmd.Parameters.Add("@Data_Final", dataGridView1.Rows[i].Cells[4].Value);
                cmd.Parameters.Add("@Nr_Pasageri", dataGridView1.Rows[i].Cells[6].Value);
                cmd.Parameters.Add("@id", dataGridView1.Rows[i].Cells[0].Value);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Salvat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            home.q = 4;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            id = row - 1;
            dataGridView1.Rows[row].Cells[3].Value = dateTimePicker1.Value;
            dataGridView1.Rows[row].Cells[4].Value = dateTimePicker2.Value;
        }
    }
}
