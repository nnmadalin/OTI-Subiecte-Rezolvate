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
using System.IO;

namespace OTI2016judet
{
    public partial class optiuni : Form
    {
        public optiuni()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new form();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int s = 0;
            s += Convert.ToInt32(numericUpDown1.Value);
            s += Convert.ToInt32(numericUpDown2.Value);
            s += Convert.ToInt32(numericUpDown3.Value);

            if(s < 250)
            {
                s = 1800;
            }
            else if(s <= 275)
            {
                s = 2200;
            }
            else
            {
                s = 2500;
            }

            textBox1.Text = s + " kcal";

            SqlConnection conn = new SqlConnection(form.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("update [Clienti] set kcal_zilnice = @kcal where email = @email", conn);
            cmd.Parameters.Add("@email", autentificare.email_user);
            cmd.Parameters.Add("@kcal", s);
            cmd.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Valoare salvata in baza de date!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void optiuni_Load(object sender, EventArgs e)
        {
            //load_meniu

            SqlConnection conn = new SqlConnection(form.db);
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from [Meniu]", conn);
            cmd.ExecuteNonQuery();

            using (StreamReader read = new StreamReader(Application.StartupPath + @"/Resurse_C#/meniu.txt"))
            {
                string line;
                line = read.ReadLine();
                while ((line = read.ReadLine()) != null)
                {
                    string[] split = line.Split(';');

                    if (split.Length >= 6)
                    {

                        cmd = new SqlCommand("insert into [Meniu] values (@id, @denumiere, @descriere, @pret, @kcal, @fel)", conn);
                        cmd.Parameters.Add("@id", split[0]);
                        cmd.Parameters.Add("@denumiere", split[1]);
                        cmd.Parameters.Add("@descriere", split[2]);
                        cmd.Parameters.Add("@pret", split[3]);
                        cmd.Parameters.Add("@kcal", split[4]);
                        cmd.Parameters.Add("@fel", split[5]);
                        cmd.ExecuteNonQuery();

                    }
                }
            }

            conn.Close();
        }

        void load_tab_1()
        {
            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Meniu]", conn);
                SqlDataReader sqlData = cmd.ExecuteReader();
                int k = 0;
                while (sqlData.Read())
                {
                    dataGridView1.Rows.Insert(k++, Convert.ToInt32(sqlData[0].ToString()), sqlData[1].ToString(), sqlData[2].ToString(), sqlData[3].ToString(), sqlData[4].ToString(), sqlData[5].ToString(), "1");
                }

                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Clienti] where email = @email", conn);
                cmd.Parameters.Add("@email", autentificare.email_user);
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                textBox2.Text = read["kcal_zilnice"].ToString();
                necesar = textBox2.Text;

                conn.Close();
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        void load_tab_2()
        {
            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Clienti] where email = @email", conn);
                cmd.Parameters.Add("@email", autentificare.email_user);
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                textBox6.Text = read["kcal_zilnice"].ToString();
                necesar = textBox2.Text;

                conn.Close();
            }

        }

        bool numeric(string x)
        {
            for(int i = 0; i < x.Length; i++)
            {
                if(x[i] < '0' || x[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        void load_tab_3()
        {
            

            string[] id_comanda = new string[10000];
            string[] id_produs = new string[10000];
            int l = 0, l2 = 0;

            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Comenzi] where id_client = @id", conn);
                cmd.Parameters.Add("@id", autentificare.id_user);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    id_comanda[l++] = read["id_comanda"].ToString();
                }
                conn.Close();
            }
            int q = 0;
            for(int i = 0; i < l; i++)
            {
                using (SqlConnection conn = new SqlConnection(form.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from [Subcomenzi] where id_comanda = @id", conn);
                    cmd.Parameters.Add("@id", id_comanda[i]);
                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read()) 
                    {
                        id_produs[l2++] = read["id_produs"].ToString();
                    }
                    conn.Close();
                }
            }

            for (int i = 0; i < l2; i++)
            {
                using (SqlConnection conn = new SqlConnection(form.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from [Meniu] where id_produs = @id", conn);
                    cmd.Parameters.Add("@id", id_produs[i]);
                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        chart1.Series["Kcal"].Points.Add(Convert.ToInt32(read["kcal"].ToString()));
                        chart1.Series["Kcal"].Points[i].AxisLabel = read["denumire_produs"].ToString();
                    }
                    conn.Close();
                }
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if(tabControl1.SelectedTab.Text == "Comanda")
            {
                load_tab_1();
            }
            else if (tabControl1.SelectedTab.Text == "Generare Meniu")
            {
                load_tab_2();
            }
            else if (tabControl1.SelectedTab.Text == "Grafic Kcal")
            {
                load_tab_3();
            }
        }

        public static string[] id = new string[1000];
        public static string[] denumire = new string[1000];
        public static string[] kcal = new string[1000];
        public static string[] pret = new string[1000];
        public static string[] cantitate = new string[1000];
        public static string necesar, totalkcal = "0", totalpret = "0";

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new comanda();
            frm.Show();
            this.Hide();
        }
        string[] fel1 = new string[10000];
        string[] fel1_id = new string[10000];
        string[] fel2 = new string[10000];
        string[] fel2_id = new string[10000];
        string[] fel3 = new string[10000];
        string[] fel3_id = new string[10000];
        int[] fel1_kcal = new int[10000];
        int[] fel2_kcal = new int[10000];
        int[] fel3_kcal = new int[10000];
        int[] fel1_pret = new int[10000];
        int[] fel2_pret = new int[10000];
        int[] fel3_pret = new int[10000];

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            

            int l1 = 0, l2 = 0, l3 = 0;


            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Meniu] where felul = 1", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    fel1[l1] = read["denumire_produs"].ToString();
                    fel1_id[l1] = read["id_produs"].ToString();
                    fel1_kcal[l1] = Convert.ToInt32(read["kcal"].ToString());
                    fel1_pret[l1] = Convert.ToInt32(read["pret"].ToString());
                    l1++;
                }
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Meniu] where felul = 2", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    fel2[l2] = read["denumire_produs"].ToString();
                    fel2_id[l2] = read["id_produs"].ToString();
                    fel2_kcal[l2] = Convert.ToInt32(read["kcal"].ToString());
                    fel2_pret[l2] = Convert.ToInt32(read["pret"].ToString());
                    l2++;
                }
                conn.Close();
            }
            using (SqlConnection conn = new SqlConnection(form.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [Meniu] where felul = 3", conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    fel3[l3] = read["denumire_produs"].ToString();
                    fel3_id[l3] = read["id_produs"].ToString();
                    fel3_kcal[l3] = Convert.ToInt32(read["kcal"].ToString());
                    fel3_pret[l3] = Convert.ToInt32(read["pret"].ToString());
                    l3++;
                }
                conn.Close();
            }
            int q = 0;
            for (int i = 0; i < l1; i++)
            {
                for (int j = 0; j < l2; j++)
                {
                    for (int k = 0; k < l3; k++)
                    {
                        if (fel1_kcal[i] + fel2_kcal[j] + fel3_kcal[k] <= Convert.ToInt32(textBox6.Text))
                        {
                            if (fel1_pret[i] + fel2_pret[j] + fel3_pret[k] <= numericUpDown4.Value)
                            {
                                dataGridView2.Rows.Add(fel1[i], fel2[j], fel3[k], fel1_kcal[i] + fel2_kcal[j] + fel3_kcal[k], fel1_pret[i] + fel2_pret[j] + fel3_pret[k]);
                                q++;
                            }
                        }
                    }
                }
            }
            dataGridView2.Sort(dataGridView2.Columns[4], ListSortDirection.Ascending);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        int getid(string x, string[] q, string[] p)
        {
            for (int i = 0; i < q.Length; i++)
            {
                if (x == q[i])
                    return Convert.ToInt32(p[i]);
            }
            return 0;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells[0].ColumnIndex == 5)
            {
                int row = dataGridView2.SelectedCells[0].RowIndex;
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
                    int id_prod = getid(dataGridView2.Rows[row].Cells[0].Value.ToString(), fel1, fel1_id);
                    SqlCommand cmd = new SqlCommand("insert into [Subcomenzi] values (@id, @idp, @cant)", conn);
                    cmd.Parameters.Add("@id", l);
                    cmd.Parameters.Add("@idp", id_prod);
                    cmd.Parameters.Add("@cant", 1);
                    cmd.ExecuteNonQuery();

                    id_prod = getid(dataGridView2.Rows[row].Cells[1].Value.ToString(), fel2, fel2_id);
                    cmd = new SqlCommand("insert into [Subcomenzi] values (@id, @idp, @cant)", conn);
                    cmd.Parameters.Add("@id", l);
                    cmd.Parameters.Add("@idp", id_prod);
                    cmd.Parameters.Add("@cant", 1);
                    cmd.ExecuteNonQuery();

                    id_prod = getid(dataGridView2.Rows[row].Cells[0].Value.ToString(), fel3, fel3_id);
                    cmd = new SqlCommand("insert into [Subcomenzi] values (@id, @idp, @cant)", conn);
                    cmd.Parameters.Add("@id", l);
                    cmd.Parameters.Add("@idp", id_prod);
                    cmd.Parameters.Add("@cant", 1);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }

                MessageBox.Show("Comanda plasata cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static int lungime = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells[0].ColumnIndex == 7)
            {
                int row = dataGridView1.SelectedCells[0].RowIndex;
                if (numeric(dataGridView1.Rows[row].Cells[6].Value.ToString()) == true)
                {
                    int s = Convert.ToInt32(textBox3.Text);
                    s += (Convert.ToInt32(dataGridView1.Rows[row].Cells[4].Value) * Convert.ToInt32(dataGridView1.Rows[row].Cells[6].Value));
                    textBox3.Text = s.ToString();
                    totalkcal = textBox3.Text;

                    s = Convert.ToInt32(textBox4.Text);
                    s += (Convert.ToInt32(dataGridView1.Rows[row].Cells[3].Value) * Convert.ToInt32(dataGridView1.Rows[row].Cells[6].Value));
                    textBox4.Text = s.ToString();
                    totalpret = textBox4.Text;

                    id[lungime] = dataGridView1.Rows[row].Cells[0].Value.ToString();
                    denumire[lungime] = dataGridView1.Rows[row].Cells[1].Value.ToString();
                    kcal[lungime] = dataGridView1.Rows[row].Cells[4].Value.ToString();
                    pret[lungime] = dataGridView1.Rows[row].Cells[3].Value.ToString();
                    cantitate[lungime] = dataGridView1.Rows[row].Cells[6].Value.ToString();
                    lungime++;
                }
                else
                {
                    dataGridView1.Rows[row].Cells[6].Value = "1";
                    MessageBox.Show("Introdu doar valoari numerice si pozitive", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
