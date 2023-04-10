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

namespace CIA2010judet
{
    public partial class baza_de_date : Form
    {
        public baza_de_date()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var form = new home();
            form.Show();
            this.Hide();
        }

        string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Scoala.mdf;Integrated Security=True;Connect Timeout=30";

        void load_elevi()
        {
            
            SqlConnection conn = new SqlConnection(db);
            conn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [Table]", conn);

            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            dataGridView1.DataSource = data;

            

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load_elevi();
        }

        private void baza_de_date_Load(object sender, EventArgs e)
        {
            load_elevi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into [Table] values (@nume, @prenume, @clasa, @absente)", conn);
            cmd.Parameters.Add("@nume", textBox1.Text);
            cmd.Parameters.Add("@prenume", textBox2.Text);
            cmd.Parameters.Add("@clasa", textBox3.Text);
            cmd.Parameters.Add("@absente", numericUpDown1.Value);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Comanda executata cu succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            numericUpDown1.Value = 0;

            conn.Close();
            load_elevi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                string id_last = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value.ToString();

                SqlConnection conn = new SqlConnection(db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM [Table] WHERE IDElev = @id", conn);
                cmd.Parameters.Add("@id", id_last);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Comanda executata cu succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();
            }
            load_elevi();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Descending);
        }
    }
}
