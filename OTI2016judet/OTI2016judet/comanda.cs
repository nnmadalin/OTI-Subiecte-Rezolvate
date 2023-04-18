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

namespace OTI2016judet
{
    public partial class comanda : Form
    {
        public comanda()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var forum = new optiuni();
            forum.Show();
            this.Hide();
        }

        private void comanda_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < optiuni.lungime; i++)
            {
                dataGridView1.Rows.Insert(i, optiuni.denumire[i], optiuni.kcal[i], optiuni.pret[i], optiuni.cantitate[i]);
            }

            textBox2.Text = optiuni.necesar;
            textBox3.Text = optiuni.totalkcal;
            textBox4.Text = optiuni.totalpret;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.Rows.RemoveAt(row);

            int s = 0;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                s += (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value));                
            }
            textBox3.Text = s.ToString();

            s = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                s += (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value));
            }
            textBox4.Text = s.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        int getid(string x)
        {
            for(int i = 0; i < optiuni.denumire.Length; i++)
            {
                if (x == optiuni.denumire[i])
                    return Convert.ToInt32(optiuni.id[i]);
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int l = 1;
            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Comenzi]", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    l++;
                }                
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [Comenzi] values (@id, @idc, @data)", conn);
                cmd.Parameters.Add("@id", l);
                cmd.Parameters.Add("@idc", autentificare.id_user);
                cmd.Parameters.Add("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                for(int i = 0; i < dataGridView1.RowCount; i++)
                {
                    int id_prod = getid(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    SqlCommand cmd = new SqlCommand("insert into [Subcomenzi] values (@id, @idp, @cant)", conn);
                    cmd.Parameters.Add("@id", l);
                    cmd.Parameters.Add("@idp", id_prod);
                    cmd.Parameters.Add("@cant", dataGridView1.Rows[i].Cells[3].Value.ToString());
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            MessageBox.Show("Comanda plasata cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var forum = new form();
            forum.Show();
            this.Hide();
        }
    }
}
