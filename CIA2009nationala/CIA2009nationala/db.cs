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

namespace CIA2009nationala
{
    public partial class db : Form
    {
        public db()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        string stringdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Elevi.mdf;Integrated Security=True;Connect Timeout=5";

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 1;
            if(dataGridView1.Rows.Count > 0)
            {
                i = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value) + 1;
            }
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = 0;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = 0;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(stringdb);
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from TabelaElevi", conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("DBCC checkident(TabelaElevi, RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                cmd = new SqlCommand("insert into TabelaElevi values (@nume, @nota1, @nota2, @medie, @rez)", conn);
                if(dataGridView1.Rows[i].Cells[1].Value == null)
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
                if (dataGridView1.Rows[i].Cells[4].Value == null)
                    cmd.Parameters.Add("@medie", DBNull.Value);
                else
                    cmd.Parameters.Add("@medie", dataGridView1.Rows[i].Cells[4].Value);
                if (dataGridView1.Rows[i].Cells[5].Value == null)
                    cmd.Parameters.Add("@rez", DBNull.Value);
                else
                    cmd.Parameters.Add("@rez", dataGridView1.Rows[i].Cells[5].Value);
                cmd.ExecuteNonQuery();
            }

            conn.Close();

            load_db();
        }

        void load_db()
        {

            if (dataGridView1.RowCount > 0)
                dataGridView1.Rows.Clear();
            SqlConnection conn = new SqlConnection(stringdb);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from TabelaElevi", conn);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                dataGridView1.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString(), read[4].ToString(), read[5].ToString());
            }

            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0 && dataGridView1.SelectedCells.Count > 0)
            {
                int row = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(row);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int na, nb;
                bool a = int.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out na);
                bool b = int.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out nb);

                if(a == false)
                {
                    dataGridView1.Rows[i].Cells[2].Value = 0;
                }
                if (b == false)
                {
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                }

                if(a == b == true && na >= 5 && nb >= 5)
                {
                    dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(((Convert.ToDouble(na) + Convert.ToDouble(nb)) / 2));
                }
                else if(na < 5)
                {
                    dataGridView1.Rows[i].Cells[2].Value = 0;
                    dataGridView1.Rows[i].Cells[4].Value = 0;
                }
                else if (nb < 5)
                {
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                    dataGridView1.Rows[i].Cells[4].Value = 0;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int na;
                bool a = int.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out na);
                if(a == false)
                {
                    dataGridView1.Rows[i].Cells[5].Value = "RESPINS";
                }
                else if(na > 5)
                {
                    dataGridView1.Rows[i].Cells[5].Value = "ADMIN";
                }
                else
                    dataGridView1.Rows[i].Cells[5].Value = "RESPINS";
            }
        }

        private void db_Load(object sender, EventArgs e)
        {
            load_db();
        }
    }
}
