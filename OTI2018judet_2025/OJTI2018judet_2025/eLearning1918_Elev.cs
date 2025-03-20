using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2018judet_2025
{
    public partial class eLearning1918_Elev : Form
    {
        public eLearning1918_Elev()
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
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        string[] question = new string[20];
        string[] typeQuestion = new string[20];
        string[] answerCorrect = new string[20];
        string[] answer1 = new string[20];
        string[] answer2 = new string[20];
        string[] answer3 = new string[20];
        string[] answer4= new string[20];

        void loadQuestions()
        {
            SqlConnection conn = new SqlConnection(ClassStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Itemi] ORDER BY NEWID()", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            int k = 1;

            while (reader.Read())
            {
                if (k < 10)
                {
                    question[k] = reader[2].ToString();
                    typeQuestion[k] = reader[1].ToString();
                    answerCorrect[k] = reader[7].ToString();
                    if (reader[1].ToString() == "2" || reader[1].ToString() == "3")
                    {
                        answer1[k] = reader[3].ToString();
                        answer2[k] = reader[4].ToString();
                        answer3[k] = reader[5].ToString();
                        answer4[k] = reader[6].ToString();
                    }
                    k++;
                }
                
            }

            conn.Close();
        }

        void loadQuestionToUser()
        {
            label2.Text = "Item nr: " + indexQuestion.ToString();
            label3.Text = "Punctaj: " + point.ToString();
            textBox1.Text = question[indexQuestion];

            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = radioButton7.Checked = radioButton8.Checked = false;
            checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;


            if (typeQuestion[indexQuestion] == "1")
            {
                panel4.Visible = panel5.Visible = panel6.Visible = false;
                panel3.Visible = true;
                panel3.Size = new Size(824, 194);
                panel3.Location = new Point(9, 186);
            }
            else if(typeQuestion[indexQuestion] == "2")
            {
                panel3.Visible = panel5.Visible = panel6.Visible = false;
                panel4.Visible = true;
                panel4.Size = new Size(824, 194);
                panel4.Location = new Point(9, 186);
                radioButton1.Text = answer1[indexQuestion];
                radioButton2.Text = answer2[indexQuestion];
                radioButton3.Text = answer3[indexQuestion];
                radioButton4.Text = answer4[indexQuestion];
            }
            else if( typeQuestion[indexQuestion] == "3")
            {
                panel4.Visible = panel3.Visible = panel5.Visible = false;
                panel6.Visible = true;
                panel6.Size = new Size(824, 194);
                panel6.Location = new Point(9, 186);
                checkBox1.Text = answer1[indexQuestion];
                checkBox2.Text = answer2[indexQuestion];
                checkBox3.Text = answer3[indexQuestion];
                checkBox4.Text = answer4[indexQuestion];
            }
            else
            {
                panel4.Visible = panel3.Visible = panel6.Visible = false;
                panel5.Visible = true;
                panel5.Size = new Size(824, 194);
                panel5.Location = new Point(9, 186);
            }
        }

        int point = 1, indexQuestion = 1;

        private void button4_Click(object sender, EventArgs e)
        {
            if (typeQuestion[indexQuestion] == "1" && textBox2.Text.Trim().ToLower() == answerCorrect[indexQuestion].ToLower().Trim())
            {
                point++;
            }
            else if(typeQuestion[indexQuestion] == "4" && ((radioButton8.Checked && answerCorrect[indexQuestion] == "1") || (radioButton7.Checked && answerCorrect[indexQuestion] == "0")))
            {
                point++;
            }
            else
            {
                if (typeQuestion[indexQuestion] == "3")
                {
                    string answer = "";
                    if (checkBox1.Checked == true)
                        answer = "1";
                    else if (checkBox2.Checked == true)
                        answer = "2";
                    else if (checkBox3.Checked == true)
                        answer = "3";
                    else if (checkBox4.Checked == true)
                        answer = "4";

                    if (answer == answerCorrect[indexQuestion])
                        point++;
                }
                else
                {
                    string answer = "";
                    if (checkBox1.Checked == true)
                        answer += "1";
                    if (checkBox2.Checked == true)
                        answer += "2";
                    if (checkBox3.Checked == true)
                        answer += "3";
                    if (checkBox4.Checked == true)
                        answer += "4";
                    if (answer == answerCorrect[indexQuestion])
                        point++;
                }
            }
            indexQuestion++;
            if(indexQuestion == 10)
            {
                panel4.Visible = panel3.Visible = panel5.Visible = panel6.Visible = false;
                panel2.Visible = false;
                button3.Enabled = true;

                SqlConnection conn = new SqlConnection(ClassStrings.dbString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [Evaluari] values(@id, @date, @nota)", conn);
                cmd.Parameters.AddWithValue("@id", ClassStrings.idUser);
                cmd.Parameters.AddWithValue("@date", new DateTime());
                cmd.Parameters.AddWithValue("@nota", point);
                conn.Close();

                loadNote();

                MessageBox.Show("Ai obtinut: " + point.ToString() + " pct");
            }
            else
                loadQuestionToUser();

        }

        int medie = 0, k = 0;

        void loadNote()
        {
            chart1.Series["Medie"].Points.Clear();
            chart1.Series["Note"].Points.Clear();

            medie = 0;
            k = 0;

            dataGridView1.Rows.Clear();
            SqlConnection conn = new SqlConnection(ClassStrings.dbString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Evaluari] where IdElev = @id", conn);
            cmd.Parameters.AddWithValue("@id", ClassStrings.idUser);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[3].ToString(), reader[2].ToString());
                medie += Convert.ToInt32(reader[3]);
                chart1.Series["Note"].Points.AddXY(k, Convert.ToInt32(reader[3].ToString()));


                k++;
            }

            if(k != 0)
                medie /= k;
            for (int j = 0; j < k; j++)
            {
                chart1.Series["Medie"].Points.AddXY(j, medie);
            }

            conn.Close();
        }
        

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void eLearning1918_Elev_Load(object sender, EventArgs e)
        {
            loadNote();
        }

        Bitmap bit;
        private void button5_Click(object sender, EventArgs e)
        {
            bit = new Bitmap(dataGridView1.Width, dataGridView1.Height + 200);
            Graphics g = Graphics.FromImage(bit);

            g.FillRectangle(Brushes.White, new RectangleF(0, 0, dataGridView1.Width, dataGridView1.Height + 200));

            Font font = new Font(FontFamily.GenericSansSerif, 35, FontStyle.Bold);
            g.DrawString("Carnetul de note al elevului " +ClassStrings.numeUser, font, Brushes.Black, new Point(10, 40));

            dataGridView1.DrawToBitmap(bit, new Rectangle(0, 150, dataGridView1.Width, dataGridView1.Height));

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bit, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            indexQuestion = 1;
            point = 1;
            panel1.Visible = true;
            button3.Enabled = false;
            loadQuestions();
            panel2.Visible = true;
            loadQuestionToUser();
        }
    }
}
