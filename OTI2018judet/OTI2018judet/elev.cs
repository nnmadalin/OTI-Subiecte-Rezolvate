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

namespace OTI2018judet
{
    public partial class elev : Form
    {
        public elev()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void carnetDeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void graficNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        
        string rasp_corect = "";

        void make_invizible()
        {
            checkBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Text = "";
            checkBox2.Checked = false;
            checkBox3.Text = "";
            checkBox3.Checked = false;
            checkBox4.Text = "";
            checkBox4.Checked = false;

            radioButton1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Text = "";
            radioButton2.Checked = false;
            radioButton3.Text = "";
            radioButton3.Checked = false;
            radioButton4.Text = "";
            radioButton4.Checked = false;

            textBox2.Text = "";
        }        

        void change_disable()
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;

            textBox2.Enabled = false;
            radioButton7.Enabled = false;
            radioButton8.Enabled = false;

        }

        void change_enable()
        {
            checkBox1.Enabled = true;
            checkBox1.BackColor = Color.Transparent;

            checkBox2.Enabled = true;
            checkBox2.BackColor = Color.Transparent;

            checkBox3.Enabled = true;
            checkBox3.BackColor = Color.Transparent;

            checkBox4.Enabled = true;
            checkBox4.BackColor = Color.Transparent;

            radioButton1.Enabled = true;
            radioButton1.BackColor = Color.Transparent;

            radioButton2.Enabled = true;
            radioButton2.BackColor = Color.Transparent;

            radioButton3.Enabled = true;
            radioButton3.BackColor = Color.Transparent;

            radioButton4.Enabled = true;
            radioButton4.BackColor = Color.Transparent;

            textBox2.Enabled = true;
            textBox2.BackColor = Color.White;

            radioButton7.Enabled = true;
            radioButton7.BackColor = Color.Transparent;
            radioButton8.Enabled = true;
            radioButton8.BackColor = Color.Transparent;

            panel5.Visible = false;
            panel4.Visible = false;
            panel3.Visible = false;
            panel6.Visible = false;
        }

        void check_answer()
        {
            if (order[index] == 1)
            {
                string t1 = textBox2.Text;
                string t2 = rasp_corect;
                string a = "", b = "";

                for(int i = 0; i < t1.Length; i++)
                {
                    if(t1[i] != ' ')
                    {
                        a += t1[i];
                    }
                }
                for (int i = 0; i < t2.Length; i++)
                {
                    if (t2[i] != ' ')
                    {
                        b += t2[i];
                    }
                }

                a = a.ToLower();
                b = b.ToLower();

                if(a == b)
                {
                    point++;
                    textBox2.BackColor = Color.Green;
                }
                else
                    textBox2.BackColor = Color.Red;

                question[index] = textBox1.Text;
                answer1[index] = textBox2.Text;
                answer_correct[index] = rasp_corect;
            }
            else if (order[index] == 2)
            {


                if (rasp_corect == "1")
                {
                    if (radioButton1.Checked == true)
                    {
                        radioButton1.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton1.BackColor = Color.Red;
                        if (radioButton2.Checked == true)
                            radioButton2.BackColor = Color.Red;
                        else if (radioButton3.Checked == true)
                            radioButton3.BackColor = Color.Red;
                        else if (radioButton4.Checked == true)
                            radioButton4.BackColor = Color.Red;
                    }
                }
                else if (rasp_corect == "2")
                {
                    if (radioButton2.Checked == true)
                    {
                        radioButton2.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton2.BackColor = Color.Red;
                        if (radioButton1.Checked == true)
                            radioButton1.BackColor = Color.Red;
                        else if (radioButton3.Checked == true)
                            radioButton3.BackColor = Color.Red;
                        else if (radioButton4.Checked == true)
                            radioButton4.BackColor = Color.Red;
                    }
                }
                else if (rasp_corect == "3")
                {
                    if (radioButton3.Checked == true)
                    {
                        radioButton3.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton3.BackColor = Color.Red;
                        if (radioButton1.Checked == true)
                            radioButton1.BackColor = Color.Red;
                        else if (radioButton2.Checked == true)
                            radioButton2.BackColor = Color.Red;
                        else if (radioButton4.Checked == true)
                            radioButton4.BackColor = Color.Red;
                    }
                }
                else if (rasp_corect == "4")
                {
                    if (radioButton4.Checked == true)
                    {
                        radioButton4.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton4.BackColor = Color.Red;
                        if (radioButton1.Checked == true)
                            radioButton1.BackColor = Color.Red;
                        else if (radioButton3.Checked == true)
                            radioButton3.BackColor = Color.Red;
                        else if (radioButton2.Checked == true)
                            radioButton2.BackColor = Color.Red;
                    }
                }
                
                
            }
            else if (order[index] == 3)
            {
                int l = 0;
                bool a = false, b = false, c = false, d = false;
                for (int i = 0; i < rasp_corect.Length; i++)
                {
                    if (rasp_corect[i] == '1')
                    {
                        a = true;
                    }
                    else if (rasp_corect[i] == '2')
                    {
                        b = true;
                    }
                    else if (rasp_corect[i] == '3')
                    {
                        c = true;
                    }
                    else if (rasp_corect[i] == '4')
                    {
                        d = true;
                    }
                }

                if (checkBox1.Checked == true && a == true)
                {
                    checkBox1.BackColor = Color.Green;
                    l++;
                }
                else if (checkBox1.Checked == true && a == false)
                {
                    checkBox1.BackColor = Color.Red;
                }
                else if (checkBox1.Checked == false && a == true)
                {
                    checkBox1.BackColor = Color.Red;
                }
                ////////
                if (checkBox2.Checked == true && b == true)
                {
                    checkBox2.BackColor = Color.Green;
                    l++;
                }
                else if (checkBox2.Checked == true && b == false)
                {
                    checkBox2.BackColor = Color.Red;
                }
                else if (checkBox2.Checked == false && b == true)
                {
                    checkBox2.BackColor = Color.Red;
                }
                ////////
                if (checkBox3.Checked == true && c == true)
                {
                    checkBox3.BackColor = Color.Green;
                    l++;
                }
                else if (checkBox3.Checked == true && c == false)
                {
                    checkBox3.BackColor = Color.Red;
                }
                else if (checkBox3.Checked == false && c == true)
                {
                    checkBox3.BackColor = Color.Red;
                }
                ////////
                if (checkBox4.Checked == true && d == true)
                {
                    checkBox4.BackColor = Color.Green;
                    l++;
                }
                else if (checkBox4.Checked == true && d == false)
                {
                    checkBox4.BackColor = Color.Red;
                }
                else if (checkBox4.Checked == false && d == true)
                {
                    checkBox4.BackColor = Color.Red;
                }

                if(l == rasp_corect.Length)
                {
                    point++;
                }
            }
            else if (order[index] == 4)
            {
                if (rasp_corect == "0")
                {
                    if (radioButton7.Checked == true)
                    {
                        radioButton7.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton7.BackColor = Color.Red;
                        if(radioButton8.Checked == true)
                            radioButton8.BackColor = Color.Red;
                    }
                }
                else if (rasp_corect == "1")
                {
                    if (radioButton8.Checked == true)
                    {
                        radioButton8.BackColor = Color.Green;
                        point++;
                    }
                    else
                    {
                        radioButton8.BackColor = Color.Red;
                        if (radioButton7.Checked == true)
                            radioButton7.BackColor = Color.Red;
                    }
                }
            }

            label2.Text = "Punctaj: " + point;
        }

        void load_question()
        {
            int id = 0;
            if (order[index] == 1)
            {
                id = question_tip1[p1++];
            }
            else if (order[index] == 2)
            {
                id = question_tip2[p2++];
            }
            else if (order[index] == 3)
            {
                id = question_tip3[p3++];
            }
            else if (order[index] == 4)
            {
                id = question_tip4[p4++];
            }

            label3.Text = "Item nr: " + (index + 1);
            

            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Itemi] where IdItem = @id", conn);
                cmd.Parameters.Add("@id", id);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {

                    rasp_corect = read["RaspunsCorectItem"].ToString();
                    textBox1.Text = read["EnuntItem"].ToString();
                    //load item;

                    if (order[index] == 1)
                    {
                        make_invizible();
                        panel5.Visible = true;
                        panel5.Location = new Point(24, 234);
                        panel5.Size = new Size(920, 290);
                    }
                    else if (order[index] == 2)
                    {
                        make_invizible();
                        panel3.Visible = true;
                        panel3.Location = new Point(24, 234);
                        panel3.Size = new Size(920, 290);

                        radioButton1.Text = read["Raspuns1Item"].ToString();
                        radioButton2.Text = read["Raspuns2Item"].ToString();
                        radioButton3.Text = read["Raspuns3Item"].ToString();
                        radioButton4.Text = read["Raspuns4Item"].ToString();
                    }
                    else if (order[index] == 3)
                    {
                        make_invizible();
                        panel4.Visible = true;
                        panel4.Location = new Point(24, 234);
                        panel4.Size = new Size(920, 290);

                        checkBox1.Text = read["Raspuns1Item"].ToString();
                        checkBox2.Text = read["Raspuns2Item"].ToString();
                        checkBox3.Text = read["Raspuns3Item"].ToString();
                        checkBox4.Text = read["Raspuns4Item"].ToString();
                    }
                    else if (order[index] == 4)
                    {
                        make_invizible();
                        panel6.Visible = true;
                        panel6.Location = new Point(24, 234);
                        panel6.Size = new Size(920, 290);
                    }
                }
                

                conn.Close();
            }
        }

        int index = 0, point = 1, p1 = 0, p2 = 0, p3 = 0, p4 = 0;

        int[] question_tip1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] question_tip2 = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

        public static string[] question = new string[15];
        public static string[] answer1 = new string[15];
        public static string[] answer2 = new string[15];
        public static string[] answer3 = new string[15];
        public static string[] answer4 = new string[15];
        public static string[] answer_correct = new string[15];

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Text == "Carnet de note")
            {
                textBox3.Text = "Carnetul de note al elevului " + home.nume_user;
                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from Evaluari where IdElev = @id", conn);
                    cmd.Parameters.Add("@id", home.id_user);

                    SqlDataAdapter sql = new SqlDataAdapter(cmd);

                    var dt = new DataTable();
                    sql.Fill(dt);
                    

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    conn.Close();
                }
            }
            else if (tabControl1.SelectedTab.Text == "Grafic note")
            {
                chart1.Series["Medie"].Points.Clear();
                chart1.Series["Note"].Points.Clear();
                int medie = 0, i = 0;
                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from Utilizatori where ClasaUtilizator = @clasa", conn);
                    cmd.Parameters.Add("@clasa", home.clasa_user);
                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        using (SqlConnection conn2 = new SqlConnection(home.db))
                        {
                            conn2.Open();

                            SqlCommand cmd2 = new SqlCommand("select * from Evaluari where IdElev = @id", conn);
                            cmd2.Parameters.Add("@id", read["IdUtilizator"].ToString());
                            SqlDataReader read2 = cmd2.ExecuteReader();

                            while (read2.Read())
                            {
                                medie += Convert.ToInt32(read2["NotaEvaluare"].ToString());
                                i++;
                            }

                            conn2.Close();
                        }
                    }

                    conn.Close();
                }
                medie /= i;
                int index = 0;
                using (SqlConnection conn = new SqlConnection(home.db))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from Evaluari where IdElev = @id", conn);
                    cmd.Parameters.Add("@id", home.id_user);
                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        chart1.Series["Note"].Points.AddXY(index++, Convert.ToInt32(read["NotaEvaluare"].ToString()));
                    }

                    conn.Close();
                }

                for(int j = 0; j < index + 5; j++)
                {
                    chart1.Series["Medie"].Points.AddXY(j, medie);
                }
            }            
        }

        private void elev_Load(object sender, EventArgs e)
        {

        }
        Bitmap bit;
        private void button4_Click(object sender, EventArgs e)
        {
            bit = new Bitmap(dataGridView1.Width, dataGridView1.Height + 200);
            Graphics g = Graphics.FromImage(bit);

            g.FillRectangle(Brushes.White, new RectangleF(0, 0, dataGridView1.Width, dataGridView1.Height + 200));

            Font font = new Font(FontFamily.GenericSansSerif, 35, FontStyle.Bold);
            g.DrawString("Carnetul de note al elevului " + home.nume_user, font, Brushes.Black, new Point(10, 40));

            dataGridView1.DrawToBitmap(bit, new Rectangle(0, 150, dataGridView1.Width, dataGridView1.Height));

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bit, 0, 0);
        }

        private void ieșireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {         
            if(button3.Text == "Raspunde")
            {
                change_disable();
                button3.Text = "Next";

                check_answer();
            }
            else
            {
                if (index < 8)
                {
                    index++;
                    change_enable();
                    button3.Text = "Raspunde";
                    load_question();
                }
                else
                {

                    MessageBox.Show("Felicitari, ai obtinut: " + point, "Informare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    //salvare
                    using (SqlConnection conn = new SqlConnection(home.db))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("insert into Evaluari values (@id, @data, @nota)", conn);
                        cmd.Parameters.Add("@id", home.id_user);
                        cmd.Parameters.Add("@data", DateTime.Now);
                        cmd.Parameters.Add("@nota", point);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    //afisare raport


                    //intiial
                    label2.Text = "Punctaj: 1";
                    index = 0;
                    point = 1;
                    label3.Text = "Item nr: 1";
                    textBox1.Text = "";
                    panel5.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    panel6.Visible = false;

                    button3.Enabled = false;
                    button2.Enabled = true;
                }
            }
        }

        int[] question_tip3 = { 20, 21, 22, 23, 24, 25};
        int[] question_tip4 = { 26, 27, 28, 29, 30, 31, 32};
        int[] order = { 1, 1, 1, 2, 2, 3, 3, 4, 4 };

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == true)
            {
                make_invizible();
                change_disable();
                change_enable();

                button2.Enabled = false;
                panel2.Enabled = true;
                button3.Enabled = true;

                button3.Text = "Raspunde";

                p1 = 0;
                p2 = 0;
                p3 = 0;

                Random random = new Random();

                question_tip1 = question_tip1.OrderBy(x => random.Next()).ToArray();
                question_tip2 = question_tip2.OrderBy(x => random.Next()).ToArray();
                question_tip3 = question_tip3.OrderBy(x => random.Next()).ToArray();
                question_tip4 = question_tip4.OrderBy(x => random.Next()).ToArray();
                order = order.OrderBy(x => random.Next()).ToArray();
            }
            load_question();
        }

        //size = 920, 290
        //loc = 24, 234
    }
}
