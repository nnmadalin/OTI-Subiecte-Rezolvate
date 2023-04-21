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

namespace OTI2019judet
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void load_tab1()
        {
            if(dataGridView1.RowCount != 0)
                dataGridView1.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from  [carti]", conn);
                SqlDataReader read = cmd.ExecuteReader();

                int i = 1;
                while (read.Read())
                {
                    using (SqlConnection conn2 = new SqlConnection(home.db))
                    {
                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("select * from [imprumut] where id_carte = @id and email = @email", conn);
                        cmd2.Parameters.Add("@id", i);
                        cmd2.Parameters.Add("@email", login.email_user);
                        SqlDataReader read2 = cmd2.ExecuteReader();

                        if (read2.HasRows)
                        {
                            while (read2.Read())
                            {
                                DateTime time = Convert.ToDateTime(read2["data_imprumut"].ToString());
                                if (time.Date.AddDays(30) < DateTime.Now.Date)
                                {
                                    dataGridView1.Rows.Add(read["titlu"].ToString(), read["autor"].ToString(), read["gen"].ToString());
                                    break;
                                }
                            }
                        }
                        else
                            dataGridView1.Rows.Add(read["titlu"].ToString(), read["autor"].ToString(), read["gen"].ToString());

                        conn2.Close();
                    }
                    i++;
                }

                conn.Close();
            }

        }

        void load_tab2()
        {
            if (dataGridView2.RowCount != 0)
                dataGridView2.Rows.Clear();
            int i = 1;
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [imprumut] where email = @email", conn);
                cmd.Parameters.Add("@email", login.email_user);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    using (SqlConnection conn2 = new SqlConnection(home.db))
                    {
                        conn2.Open();
                        SqlCommand cmd2;

                        cmd2 = new SqlCommand("select * from [carti] where id_carte = @id", conn2);
                        cmd2.Parameters.Add("@id", read["id_carte"].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        DateTime time = Convert.ToDateTime(read["data_imprumut"].ToString());
                        while (read2.Read())
                        {
                           
                            dataGridView2.Rows.Add(i++, read2["titlu"].ToString(), read2["autor"].ToString(), read["data_imprumut"].ToString(), time.AddDays(30).Date);
                            break;
                        }
                       
                        if (time.Date.AddDays(30) >= DateTime.Now.Date)
                        {
                            dataGridView2.Rows[i - 2].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else
                        {
                            dataGridView2.Rows[i - 2].DefaultCellStyle.BackColor = Color.Red;
                        }

                        conn2.Close();
                    }
                }


                conn.Close();
            }

            label3.Text = "Disponibiltate: " + number_book + "/3";
            progressBar1.Value = number_book;
            chart_load();
        }

        void load_chart1()
        {
            int[] val = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [imprumut] where email = @email", conn);
                cmd.Parameters.Add("@email", login.email_user);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    DateTime time = Convert.ToDateTime(read["data_imprumut"].ToString());
                    int year = time.Year;
                    if (year.ToString() == comboBox1.GetItemText(comboBox1.SelectedItem))
                    {
                        val[time.Month]++;
                    }
                }

                conn.Close();
            }

            for(int i = 1; i <= 12; i++)
            {
                chart1.Series["Luna"].Points.AddXY(i, val[i]);
                
            }
        }

        void chart_load()
        {
            chart1.Series["Luna"].Points.Clear();
            chart2.Series["Carti"].Points.Clear();
            comboBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [imprumut] where email = @email", conn);
                cmd.Parameters.Add("@email", login.email_user);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    DateTime time = Convert.ToDateTime(reader["data_imprumut"].ToString());
                    int year = time.Year;

                    comboBox1.Items.Add(year);
                }

                conn.Close();
            }


            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            load_chart1();
            load_chart2();

        }
        
        void load_chart2()
        {
            
        }

        int number_book = 0;

        void get_number_book()
        {
            number_book = 0;
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [imprumut] where email = @email", conn);
                cmd.Parameters.Add("@email", login.email_user);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    DateTime time = Convert.ToDateTime(read["data_imprumut"].ToString());
                    if (time.Date.AddDays(30) >= DateTime.Now.Date)
                    {
                        number_book++;
                    }
                }
                

                conn.Close();
            }
        }

        private void menu_Load(object sender, EventArgs e)
        {
            label2.Text = "Email: " + login.email_user;
            load_tab1();
            get_number_book();
            load_tab2();
        }

        Point _mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouse.X;
                int dy = e.Location.Y - _mouse.Y;

                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        string find_id(string x)
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("select * from [carti] where titlu = @titlu", conn);
                cmd.Parameters.Add("@titlu", x);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    return read["id_carte"].ToString();
                }


                conn.Close();
                return "0";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3 && number_book != 3)
            {
                int row = dataGridView1.CurrentRow.Index;

                number_book++;
                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();
                    SqlCommand cmd;

                    cmd = new SqlCommand("insert into [imprumut] values (@id, @email, @data)", conn);
                    cmd.Parameters.Add("@email", login.email_user);
                    cmd.Parameters.Add("@data", DateTime.Now);
                    cmd.Parameters.Add("@id", find_id(dataGridView1.Rows[row].Cells[0].Value.ToString()));
                    cmd.ExecuteNonQuery();
                    load_tab1();
                    load_tab2();
                    chart_load();

                    conn.Close();
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == 3 && number_book == 3)
            {
                MessageBox.Show("Maxim 3 carti!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public static string id = "1";
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView2.CurrentRow.Index;
            if(dataGridView2.Rows[row].DefaultCellStyle.BackColor == Color.Red)
            {
                MessageBox.Show("Perioada imprumutului expirata!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var form = new carte();
                id = find_id(dataGridView1.Rows[row].Cells[1].Value.ToString());
                form.ShowDialog();
                
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_chart1();
        }
    }
}
