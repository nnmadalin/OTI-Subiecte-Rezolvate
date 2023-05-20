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

namespace OTI2019nationala
{
    public partial class BibliotecarBiblioteca : Form
    {
        public BibliotecarBiblioteca()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                this.Location = this.Location + (Size)e.Location - (Size)_mouse;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox3.Clear(); textBox4.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = true;
            textBox4.Enabled = true;
        }

        bool verifica_inregistrare_user()
        {
            if(textBox1.Text.Trim() == "")
            {
                return false;
            }
            if (textBox2.Text.Trim() == "")
            {
                return false;
            }
            if (radioButton1.Checked == true)
            {
                if (textBox3.Text.Trim() == "")
                {
                    return false;
                }
                if (textBox4.Text.Trim() == "")
                {
                    return false;
                }
            }
            int p1 = 0, p2 = 0;
            for(int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '@')
                    p1 = i;
                if (textBox2.Text[i] == '.')
                    p2 = i;
            }

            if (p1 >= p2)
                return false;
            if (p1 + 2 >= p2)
                return false;
            if (p2 + 3 > textBox2.Text.Length)
                return false;
            if (p1 < 3)
                return false;

            if (radioButton1.Checked == true && textBox3.Text != textBox4.Text)
                return false;

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Utilizatori where Email = @email", conn);
                cmd.Parameters.Add("@email", textBox2.Text);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    conn.Close();
                    return false;
                }
                conn.Close();
            }

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();

            if (verifica_inregistrare_user() == false)
                button4.Enabled = false;
            else
                button4.Enabled = true;

        }

        void load_tab_afisarecititor(string sort)
        {
            if (dataGridView1.RowCount > 0)
                dataGridView1.Rows.Clear();
            else
            {
                if(sort == "")
                {
                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select * from Utilizatori where TipUtilizator = 2", conn);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(read[0].ToString(), read[2].ToString(), read[3].ToString());
                        }

                        conn.Close();
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select * from Utilizatori where TipUtilizator = 2 and NumePrenume like @nume", conn);
                        cmd.Parameters.Add("@nume", sort);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(read[0].ToString(), read[2].ToString(), read[3].ToString());
                        }

                        conn.Close();
                    }
                }
            }
        }

        private void BibliotecarBiblioteca_Load(object sender, EventArgs e)
        {
            try 
            { 
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Resurse/Imagini/utilizatori/" + LogareBiblioteca.id + ".jpg");
            }
            catch { };
            label2.Text = LogareBiblioteca.numeprenume;
            load_tab_afisarecititor("");
        }

        Image img;
        bool tab_3 = false;

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
                img = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int kk = 1;
            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Utilizatori", conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    kk++;
                }
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Utilizatori values (@tip, @nume, @email, @parola)", conn);
                if (radioButton1.Checked == true)
                {
                    cmd.Parameters.Add("@tip", 1);
                    cmd.Parameters.Add("@nume", textBox1.Text);
                    cmd.Parameters.Add("@email", textBox2.Text);
                    cmd.Parameters.Add("@parola", textBox3.Text);
                }
                else
                {
                    cmd.Parameters.Add("@tip", 2);
                    cmd.Parameters.Add("@nume", textBox1.Text);
                    cmd.Parameters.Add("@email", textBox2.Text);
                    cmd.Parameters.Add("@parola", DBNull.Value);
                }

                
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Cont adaugat cu succes", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); pictureBox2.BackgroundImage = null;
                radioButton1.Checked = true;
            }
            img.Save(Application.StartupPath + "/Resurse/Imagini/utilizatori/" + kk.ToString() + ".jpg");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); pictureBox2.BackgroundImage = null;
            radioButton1.Checked = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox5.Text.Trim() == "")
                load_tab_afisarecititor("");
            else
                load_tab_afisarecititor(textBox5.Text);
        }

        int carti_imrp = 0, carti_rez = 0;

        void load_tab_cititor(int row)
        {
            carti_imrp = 0;
            carti_rez = 0;
            label10.Text = "ID: " + dataGridView1.Rows[row].Cells[0].Value.ToString();
            label11.Text = "Nume: " + dataGridView1.Rows[row].Cells[1].Value.ToString();

            pictureBox3.Image = Image.FromFile(Application.StartupPath + "/Resurse/Imagini/utilizatori/" + dataGridView1.Rows[row].Cells[0].Value + ".jpg");
            if (dataGridView2.RowCount > 0)
                dataGridView2.Rows.Clear();
            if (dataGridView3.RowCount > 0)
                dataGridView3.Rows.Clear();
            if (dataGridView4.RowCount > 0)
                dataGridView4.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Imprumuturi where IdCititor = @id and DataRestituire is NULL", conn);
                cmd.Parameters.Add("@id", dataGridView1.Rows[row_user].Cells[0].Value.ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    if (Convert.ToDateTime(read["DataImprumut"].ToString()).AddDays(7) >= DateTime.Now)

                        using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                        {
                            conn2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Carti where IdCarte = @id", conn2);
                            cmd2.Parameters.Add("@id", read[2].ToString());
                            SqlDataReader read2 = cmd2.ExecuteReader();
                            if (read2.Read())
                            {
                                carti_imrp++;
                                dataGridView2.Rows.Add(read[0].ToString(), read[2].ToString(), read2["Titlu"].ToString(), read2["Autor"].ToString(), read[3].ToString(),
                                    Convert.ToDateTime(read["DataImprumut"].ToString()).AddDays(7));
                            }
                            conn2.Close();
                        }
                         
                }

                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Rezervari where IdCititor = @id and StatusRezervare = 1", conn);
                cmd.Parameters.Add("@id", dataGridView1.Rows[row_user].Cells[0].Value.ToString());
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    if (Convert.ToDateTime(read["DataRezervare"].ToString()).AddDays(1) >= DateTime.Now)

                        using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                        {
                            conn2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Carti where IdCarte = @id", conn2);
                            cmd2.Parameters.Add("@id", read[2].ToString());
                            SqlDataReader read2 = cmd2.ExecuteReader();
                            if (read2.Read())
                            {
                                carti_rez++;
                                dataGridView3.Rows.Add(read[0].ToString(), read[2].ToString(), read2["Titlu"].ToString(), read2["Autor"].ToString(), read[3].ToString(),
                                    Convert.ToDateTime(read["DataRezervare"].ToString()).AddDays(1));
                            }
                            conn2.Close();
                        }

                }

                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                if (sort_autor.Trim() != "" && sort_titlu.Trim() == "")
                {
                    cmd = new SqlCommand("select * from Carti where Autor like @autor", conn);
                    cmd.Parameters.Add("@autor", sort_autor);
                }
                else if (sort_autor.Trim() == "" && sort_titlu.Trim() != "")
                {
                    cmd = new SqlCommand("select * from Carti where Titlu like @Titlu", conn);
                    cmd.Parameters.Add("@Titlu", sort_titlu);
                }
                else if (sort_autor.Trim() != "" && sort_titlu.Trim() != "")
                {
                    cmd = new SqlCommand("select * from Carti where Titlu like @Titlu and Autor like @autor", conn);
                    cmd.Parameters.Add("@Titlu", sort_titlu);
                    cmd.Parameters.Add("@autor", sort_autor);
                }
                else
                    cmd = new SqlCommand("select * from Carti", conn);


                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    bool ok = false;
                    using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                    {
                        conn2.Open();
                        SqlCommand cmd2 = new SqlCommand("select * from Rezervari where IdCarte = @id and StatusRezervare = 1", conn2);
                        cmd2.Parameters.Add("@id", read[0].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        while (read2.Read())
                        {
                            if(Convert.ToDateTime(read2["DataRezervare"].ToString()).AddDays(1) >= DateTime.Now)
                            {
                                ok = true;
                                break;
                            }
                        }
                        conn2.Close();
                    }
                    using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                    {
                        conn2.Open();
                        SqlCommand cmd2 = new SqlCommand("select * from Imprumuturi where IdCarte = @id and DataRestituire is NULL", conn2);
                        cmd2.Parameters.Add("@id", read[0].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        while (read2.Read())
                        {
                            if (Convert.ToDateTime(read2["DataImprumut"].ToString()).AddDays(7) >= DateTime.Now)
                            {
                                ok = true;
                                break;
                            }
                        }
                        conn2.Close();
                    }

                    if(ok == false)
                    {
                        dataGridView4.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString());
                    }

                }

                conn.Close();
            }

            label12.Text = "Rezervari ramase: " + (3 - carti_rez);
            label13.Text = "Imprumuturi ramase: " + (3 - carti_imrp);
        }

        int row_user;
        string sort_titlu = "", sort_autor = "";

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedCells[0].ColumnIndex == 3)
            {
                int row = dataGridView1.SelectedCells[0].RowIndex;
                row_user = row;
                load_tab_cititor(row_user);
                tab_3 = true;
                tabControl1.SelectedIndex = 2;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.ToString() == "TabPage: {Cititor}" && tab_3 == false)
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedCells[0].ColumnIndex == 6)
            {
                int row = dataGridView3.SelectedCells[0].RowIndex;
                using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update Rezervari set StatusRezervare = 0 where IdRezervare = @id", conn);
                    cmd.Parameters.Add("@id", dataGridView3.Rows[row].Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                load_tab_cititor(row_user);
            }
            else if (dataGridView3.SelectedCells[0].ColumnIndex == 7)
            {
                int row = dataGridView3.SelectedCells[0].RowIndex;

                if (carti_imrp < 3)
                {
                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update Rezervari set StatusRezervare = 0 where IdRezervare = @id", conn);
                        cmd.Parameters.Add("@id", dataGridView3.Rows[row].Cells[0].Value.ToString());
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }

                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("insert into Imprumuturi values (@id1, @id2, @datarezervare, @datafn)", conn);
                        cmd.Parameters.Add("@id1", dataGridView1.Rows[row_user].Cells[0].Value.ToString());
                        cmd.Parameters.Add("@id2", dataGridView3.Rows[row].Cells[1].Value);
                        cmd.Parameters.Add("@datarezervare", DateTime.Now);
                        cmd.Parameters.Add("@datafn", DBNull.Value);
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }

                    load_tab_cititor(row_user);
                }
                else
                {
                    MessageBox.Show("Poti imprumuta maxim 3 carti!");
                }
                

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView2.SelectedCells[0].ColumnIndex == 6)
            {
                int row = dataGridView2.SelectedCells[0].RowIndex;
                using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update Imprumuturi set DataRestituire = @data where IdImprumut = @id", conn);
                    cmd.Parameters.Add("@id", dataGridView2.Rows[row].Cells[0].Value.ToString());
                    cmd.Parameters.Add("@data", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    load_tab_cititor(row_user);
                }
            }
        }

        public static string id_carte = "", titlu_carte = "", autor_carte = "", pag_carte = "";

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView5.Rows.Count > 0)
                dataGridView5.Rows.Clear();
            int x = Convert.ToInt32(numericUpDown1.Value) * 7, k = 0;
            label17.Text = "x 7 = " + x.ToString();

            string[] id = new string[1000];
            string[] titlu = new string[1000];
            string[] autori = new string[1000];
            int[] pagini = new int[1000];

            using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Carti", conn);

                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    bool ok = false;
                    using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                    {
                        conn2.Open();
                        SqlCommand cmd2 = new SqlCommand("select * from Rezervari where IdCarte = @id and StatusRezervare = 1", conn2);
                        cmd2.Parameters.Add("@id", read[0].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        while (read2.Read())
                        {
                            if (Convert.ToDateTime(read2["DataRezervare"].ToString()).AddDays(1) >= DateTime.Now)
                            {
                                ok = true;
                                break;
                            }
                        }
                        conn2.Close();
                    }
                    using (SqlConnection conn2 = new SqlConnection(startBiblioteca.db))
                    {
                        conn2.Open();
                        SqlCommand cmd2 = new SqlCommand("select * from Imprumuturi where IdCarte = @id and DataRestituire is NULL", conn2);
                        cmd2.Parameters.Add("@id", read[0].ToString());
                        SqlDataReader read2 = cmd2.ExecuteReader();
                        while (read2.Read())
                        {
                            if (Convert.ToDateTime(read2["DataImprumut"].ToString()).AddDays(7) >= DateTime.Now)
                            {
                                ok = true;
                                break;
                            }
                        }
                        conn2.Close();
                    }

                    if (ok == false)
                    {
                        dataGridView4.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString());
                        id[k] = read[0].ToString();
                        titlu[k] = read[1].ToString();
                        autori[k] = read[2].ToString();
                        pagini[k++] = Convert.ToInt32(read[3].ToString());
                    }

                }

                conn.Close();
            }

            for(int i = 0; i < k; i++)
            {
                for(int j = i + 1; j < k; j++)
                {
                    for(int q = j + 1; q < k; q++)
                    {
                        if(pagini[i] + pagini[j] + pagini[q] <= x)
                        {
                            dataGridView5.Rows.Add(id[i], titlu[i], autori[i], pagini[i],
                                                    id[j], titlu[j], autori[j], pagini[j],
                                                    id[q], titlu[q], autori[q], pagini[q], (pagini[i] + pagini[j] + pagini[q]));
                        }
                    }
                }
            }

        }

        private void dataGridView4_DoubleClick(object sender, EventArgs e)
        {
            int row = dataGridView4.SelectedCells[0].RowIndex;
            id_carte = dataGridView4.Rows[row].Cells[0].Value.ToString();
            titlu_carte = dataGridView4.Rows[row].Cells[1].Value.ToString();
            autor_carte = dataGridView4.Rows[row].Cells[2].Value.ToString();
            pag_carte = dataGridView4.Rows[row].Cells[3].Value.ToString();

            var frm = new PrevizualizareCarte();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sort_titlu = textBox6.Text;
            sort_autor = textBox7.Text;
            load_tab_cititor(row_user);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.SelectedCells[0].ColumnIndex == 4)
            {
                int row = dataGridView4.SelectedCells[0].RowIndex;
                if (carti_rez < 3)
                {
                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("insert into Rezervari values (@id1, @id2, @datarezervare, @status)", conn);
                        cmd.Parameters.Add("@id1", dataGridView1.Rows[row_user].Cells[0].Value.ToString());
                        cmd.Parameters.Add("@id2", dataGridView4.Rows[row].Cells[0].Value);
                        cmd.Parameters.Add("@datarezervare", DateTime.Now);
                        cmd.Parameters.Add("@status", "1");
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }

                    load_tab_cititor(row_user);
                }
                else
                {
                    MessageBox.Show("Poti rezerva maxim 3 carti!");
                }
            }
            else if (dataGridView4.SelectedCells[0].ColumnIndex == 5)
            {
                int row = dataGridView4.SelectedCells[0].RowIndex;
                if (carti_imrp < 3)
                {
                    using (SqlConnection conn = new SqlConnection(startBiblioteca.db))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("insert into Imprumuturi values (@id1, @id2, @datarezervare, @datafn)", conn);
                        cmd.Parameters.Add("@id1", dataGridView1.Rows[row_user].Cells[0].Value.ToString());
                        cmd.Parameters.Add("@id2", dataGridView4.Rows[row].Cells[0].Value);
                        cmd.Parameters.Add("@datarezervare", DateTime.Now);
                        cmd.Parameters.Add("@datafn", DBNull.Value);
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }

                    load_tab_cititor(row_user);
                }
                else
                {
                    MessageBox.Show("Poti imprumuta maxim 3 carti!");
                }
            }
        }
    }
}
