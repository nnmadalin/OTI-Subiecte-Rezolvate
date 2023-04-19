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

namespace OTI2017judet
{
    public partial class visualizare_excursie : Form
    {
        public visualizare_excursie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new home();
            form.Show();
            this.Hide();
        }

        void load_tab1()
        {
            dataGridView1.Rows.Clear();
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Planificari]", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    using (SqlConnection conn2 = new SqlConnection(home.db))
                    {
                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("select * from [Localitati] where IDLocalitate = @id", conn);
                        cmd2.Parameters.Add("@id", reader["IDLocalitate"].ToString());
                        SqlDataReader reader2 = cmd2.ExecuteReader();

                        reader2.Read();

                        string start = "", finish = "";
                        if (reader["DataStart"].ToString() != "")
                        {
                            start = Convert.ToDateTime(reader["DataStart"]).ToString("dd.MM.yyy");
                            finish = Convert.ToDateTime(reader["DataStop"]).ToString("dd.MM.yyy");
                        }
                        dataGridView1.Rows.Add(reader2["Nume"].ToString(), start, finish, reader["Frecventa"].ToString(), reader["Ziua"].ToString());

                        conn2.Close();
                    }
                }

                conn.Close();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Text == "Planificari")
            {
                load_tab1();
            }
        }

        private void visualizare_excursie_Load(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Planificari")
            {
                load_tab1();
            }
        }

        string get_name(string x)
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Localitati] where IDLocalitate = @id", conn);
                cmd.Parameters.Add("@id", x);
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                return reader["Nume"].ToString();

                conn.Close();
            }
            
        }

        Tuple<DateTime, DateTime> check_data(DateTime ds, DateTime df)
        {
            DateTime start = dateTimePicker1.Value.Date;
            DateTime finish = dateTimePicker2.Value.Date;

            if(ds.Date >= start.Date && ds.Date <= finish.Date)
            {
                if(df.Date > finish.Date)
                {
                    df = finish;
                }
            }
            else if(ds.Date < start.Date && df >= start.Date)
            {
                ds = start;
                if(df.Date > finish.Date)
                {
                    df = finish;
                }
            }
            else
            {
                return Tuple.Create(Convert.ToDateTime("1/1/1111"), Convert.ToDateTime("1/1/1111"));
            }
            return Tuple.Create(ds, df);
        }

       
        

        private void button3_Click(object sender, EventArgs e)
        {
            
            if(dataGridView2.RowCount != 0)
                dataGridView2.Rows.Clear();
            if (dataGridView3.RowCount != 0)
                dataGridView3.Rows.Clear();
            int q = 0;
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Planificari]", conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    string denumire = get_name(read["IDLocalitate"].ToString());
                    DateTime ds, df;
                    if (read["DataStart"].ToString() != "")
                    {
                        ds = Convert.ToDateTime(read["DataStart"].ToString());
                        df = Convert.ToDateTime(read["DataStop"].ToString());


                        DateTime dss = check_data(ds, df).Item1;
                        DateTime dff = check_data(ds, df).Item2;

                        if (dss != Convert.ToDateTime("1/1/1111")) 
                        { 
                            dataGridView2.Rows.Insert(q++, denumire, dss.ToString("dd.MM.yyy"), dff.ToString("dd.MM.yyy"), "ocazional");
                        }
                    }
                    else
                    {
                        string frecventa = read["Frecventa"].ToString();
                        string zi = read["Ziua"].ToString();

                        if(frecventa == "lunar")
                        {
                            DateTime start = dateTimePicker1.Value.Date;
                            DateTime finish = dateTimePicker2.Value.Date;

                            DateTime now = start;
                            
                            bool first = false;
                            while (now <= finish)
                            {
                                
                                int days = DateTime.DaysInMonth(now.Year, now.Month);
                                if (now.Month == finish.Month)
                                {
                                    days = finish.Day;
                                }
                                if (Convert.ToInt32(zi) <= days && Convert.ToInt32(zi) >= now.Day)
                                {
                                    dataGridView2.Rows.Insert(q++, denumire, zi + "." + now.Month + "." + now.Year, zi + "." + now.Month + "." + now.Year, "lunar");

                                }

                                if(first == false)
                                {
                                    first = true;
                                    now = Convert.ToDateTime(now.Month + "/" + 1 +"/" + now.Year);
                                }

                                now = now.AddMonths(1);
                            }
                        }
                        else
                        {
                            DateTime start = dateTimePicker1.Value;
                            DateTime finish = dateTimePicker2.Value;

                            DateTime anual = new DateTime(start.Year, 1, 1);
                            anual = anual.AddDays(Convert.ToInt32(zi) - 1);

                            if (anual >= start && anual <= finish)
                            {
                                dataGridView2.Rows.Insert(q++, denumire, anual.ToString("dd.MM.yyy"), anual.ToString("dd.MM.yyy"), "anual");

                            }
                        }
                    }
                }
                conn.Close();
            }

            DateTime dstart = dateTimePicker1.Value.Date;
            DateTime dfinish = dateTimePicker2.Value.Date;
            q = 0;
            while (dstart <= dfinish)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    string[] split = dataGridView2.Rows[i].Cells[1].Value.ToString().Split('.');
                    string new_str = split[1] + "/" + split[0] + "/" + split[2];
                    DateTime d1 = Convert.ToDateTime(new_str).Date;

                    split = dataGridView2.Rows[i].Cells[2].Value.ToString().Split('.');
                    new_str = split[1] + "/" + split[0] + "/" + split[2];
                    DateTime d2 = Convert.ToDateTime(new_str).Date;
                    if (dstart.Date >= d1 && dstart.Date <= d2)
                    {
                        
                        dataGridView3.Rows.Insert(q++, dataGridView2.Rows[i].Cells[0].Value, dstart.ToString("dd.MM.yyy"));
                    }
                }
                dstart = dstart.AddDays(1);
            }

            if (dataGridView3.RowCount != 0)
            {
                button2.Enabled = true;
                label4.Text = dataGridView3.Rows[0].Cells[0].Value.ToString();
                label5.Text = dataGridView3.Rows[0].Cells[1].Value.ToString();

                string denumire = label4.Text;
                if (denumire == "Cluj-Napoca")
                    denumire = "Cluj";
                else if (denumire == "Targu Neamt")
                    denumire = "TarguNeamt";
                else if (denumire == "Vatra Dornei")
                    denumire = "VatraDornei";

                pictureBox1.ImageLocation = home.path_image + denumire + "1.jpg";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                nmax = dataGridView3.RowCount;
                progressBar1.Maximum = nmax;
                progressBar1.Value = 0;
                index = 0;
            }
            MessageBox.Show("Generat cu succes", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        int index = 0, nmax;

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "Start")
            {
                button2.Text = "Stop";
                button3.Enabled = false;

                timer1.Start();
            }
            else
            {
                button2.Text = "Start";
                button3.Enabled = true;

                timer1.Stop();
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (index == nmax-1)
            {
                index = 0;
            }
            else
                index++;
            
            label4.Text = dataGridView3.Rows[index].Cells[0].Value.ToString();
            label5.Text = dataGridView3.Rows[index].Cells[1].Value.ToString();

            string denumire = label4.Text;
            if (denumire == "Cluj-Napoca")
                denumire = "Cluj";
            else if (denumire == "Targu Neamt")
                denumire = "TarguNeamt";
            else if (denumire == "Vatra Dornei")
                denumire = "VatraDornei";

            progressBar1.Value = index;

            pictureBox1.ImageLocation = home.path_image + denumire + "1.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
