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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cia2009judet
{
    public partial class bd : Form
    {
        public bd()
        {
            InitializeComponent();
        }

        string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\database.mdf;Integrated Security=True;Connect Timeout=30";

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new home();
            form.Show();
        }
        int k = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                k = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value);
            }
            else
                k = 0;
            k++;

            dataGridView1.Rows.Add();

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = k;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedCells.Count > 0)
            {
                int row = dataGridView1.SelectedCells[0].RowIndex;
                Console.WriteLine(row.ToString());
                dataGridView1.Rows.Remove(dataGridView1.Rows[row]);
            }
        }

        void load_db()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            SqlConnection conn = new SqlConnection(db);
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Probleme cu Baza De Date!!!");
            }
            int index = 0;
            SqlCommand cmd = new SqlCommand("SELECT * FROM [TabelaElevi]", conn);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    k = Convert.ToInt32(read["id"].ToString());
                    dataGridView1.Rows.Insert(index++, read["id"].ToString(), read["Nume"].ToString(), read["Nota1"].ToString(), read["Nota2"].ToString(), read["Medie"].ToString());
                }
            }

            conn.Close();
        }

        private void bd_Load(object sender, EventArgs e)
        {
            
            load_db();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                int a = 0, b = 0;
                bool bool_a = int.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out a);
                if (bool_a == false)
                {
                    MessageBox.Show("Introdu doar numere!");
                    break;
                }
                bool bool_b = int.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out b);
                if (bool_b == false)
                {
                    MessageBox.Show("Introdu doar numere!");
                    break;
                }

                a = a + b;
                a /= 2;
                dataGridView1.Rows[i].Cells[4].Value = a;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(db);
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Probleme cu Baza De Date!!!");
            }
            SqlCommand cmd = new SqlCommand("DELETE FROM [TabelaElevi]", conn);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("DBCC CHECKIDENT ('[TabelaElevi]', RESEED, 0);", conn);
            cmd.ExecuteNonQuery();
            
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                cmd = new SqlCommand("insert into [TabelaElevi] values (@nume, @nota1, @nota2, @medie)", conn);
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    cmd.Parameters.Add("@nume", DBNull.Value);
                else
                    cmd.Parameters.Add("@nume", dataGridView1.Rows[i].Cells[1].Value);
                if (dataGridView1.Rows[i].Cells[2].Value == null)
                    cmd.Parameters.Add("@nota1", DBNull.Value);
                else
                    cmd.Parameters.Add("@nota1", dataGridView1.Rows[i].Cells[2].Value);
                if (dataGridView1.Rows[i].Cells[3].Value == null)
                    cmd.Parameters.Add("@nota2", DBNull.Value);
                else
                    cmd.Parameters.Add("@nota2", dataGridView1.Rows[i].Cells[3].Value);
                if(dataGridView1.Rows[i].Cells[4].Value == null)
                    cmd.Parameters.Add("@medie", DBNull.Value);
                else
                    cmd.Parameters.Add("@medie", dataGridView1.Rows[i].Cells[4].Value);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Salvat cu succes!");
            conn.Close();
            load_db();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new home();
            form.Show();
        }
    }
}
