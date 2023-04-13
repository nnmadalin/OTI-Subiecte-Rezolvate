using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIA2012judet
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //location: 13, 281
        //size: 915, 288

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        string[] question = new string[1000];
        string[] answer1 = new string[1000];
        string[] answer2 = new string[1000];
        string[] answer3 = new string[1000];
        string[] answer4 = new string[1000];

        string[] answer_save = new string[1000];

        int[] point = new int[1000];
        string[] correct = new string[1000];
        bool[] tip = new bool[1000]; // false - radio       true - checkbox

        bool enable = true;

        int index = 0, nmax = 0, tpoint = 0;

        void read_q()
        {
            StreamReader streamReader = new StreamReader(Application.StartupPath + "/data/intrebari.txt");
            string read;
            int k = 0, cline = 0;
            while ((read = streamReader.ReadLine()) != null)
            {
                
                if(cline == 0)
                {
                    question[k] = read;
                }
                else if(cline == 1)
                {
                    answer1[k] = read;
                }
                else if (cline == 2)
                {
                    answer2[k] = read;
                }
                else if (cline == 3)
                {
                    answer3[k] = read;
                }
                else if (cline == 4)
                {
                    answer4[k] = read;
                }
                else if (cline == 5)
                {
                    correct[k] = read;
                }
                else if (cline == 6)
                {
                    point[k] = Convert.ToInt32(read);
                }
                else if (cline == 7)
                {
                    if (read == "0")
                        tip[k] = false;
                    else
                        tip[k] = true;
                    cline = -1;
                    nmax = k;
                    k++;
                }
                cline++;
            }
        }

        void load_question()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            radioButton1.Checked = false;
            radioButton1.Text = "";
            radioButton1.Enabled = true;

            radioButton2.Checked = false;
            radioButton2.Text = "";
            radioButton2.Enabled = true;

            radioButton3.Checked = false;
            radioButton3.Text = "";
            radioButton3.Enabled = true;

            radioButton4.Checked = false;
            radioButton4.Text = "";
            radioButton4.Enabled = true;

            checkBox1.Checked = false;
            checkBox1.Text = "";
            checkBox1.Enabled = true;

            checkBox2.Checked = false;
            checkBox2.Text = "";
            checkBox2.Enabled = true;

            checkBox3.Checked = false;
            checkBox3.Text = "";
            checkBox3.Enabled = true;

            checkBox4.Checked = false;
            checkBox4.Text = "";
            checkBox4.Enabled = true;

            if (enable == false)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }


            if (answer_save[index] != null)
            {
                label2.Text = "Raspuns validat!";
                string[] spl = answer_save[index].Split(',');
                if (tip[index] == false)
                {
                    for(int i = 0; i < spl.Length; i++)
                    {
                        if (spl[i] == "1")
                            radioButton1.Checked = true;
                        if (spl[i] == "2")
                            radioButton2.Checked = true;
                        if (spl[i] == "3")
                            radioButton3.Checked = true;
                        if (spl[i] == "4")
                            radioButton4.Checked = true;
                    }
                }
                else
                {
                    for (int i = 0; i < spl.Length; i++)
                    {
                        if (spl[i] == "1")
                            checkBox1.Checked = true;
                        if (spl[i] == "2")
                            checkBox2.Checked = true;
                        if (spl[i] == "3")
                            checkBox3.Checked = true;
                        if (spl[i] == "4")
                            checkBox4.Checked = true;
                    }
                }
            }
            else
                label2.Text = "";

            textBox1.Text = question[index];
            if (tip[index] == false)
            {
                panel2.Visible = true;
                panel2.Location = new Point(13, 281);
                panel2.Size = new Size(915, 288);
                radioButton1.Text = answer1[index];
                radioButton2.Text = answer2[index];
                radioButton3.Text = answer3[index];
                radioButton4.Text = answer4[index];
            }
            else
            {
                panel3.Visible = true;
                panel3.Location = new Point(13, 281);
                panel3.Size = new Size(915, 288);
                checkBox1.Text = answer1[index];
                checkBox2.Text = answer2[index];
                checkBox3.Text = answer3[index];
                checkBox4.Text = answer4[index];
            }
        }

        private void test_Load(object sender, EventArgs e)
        {
            read_q();
            load_question();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            button4.Enabled = false;
            panel4.Visible = true;
            enable = false;
            load_question();

            for (int i = 0; i <= nmax; i++)
            {
                if (answer_save[i] == correct[i])
                {
                    tpoint += point[i];
                }
            }

            textBox2.Text = "Ati obtinut " + tpoint.ToString() + " puncte";
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string r = "";
            
            if (tip[index] == false)
            {

                if(radioButton1.Checked == true)
                {
                    r = "1";
                }
                if (radioButton2.Checked == true)
                {
                    r = "2";
                }
                if (radioButton3.Checked == true)
                {
                    r = "3";
                }
                if (radioButton4.Checked == true)
                {
                    r = "4";
                }
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    r += "1";
                }
                if (checkBox2.Checked == true)
                {
                    if (r != "")
                        r += ",";
                    r += "2";
                }
                if (checkBox3.Checked == true)
                {
                    if (r != "")
                        r += ",";
                    r += "3";
                }
                if (checkBox4.Checked == true)
                {
                    if (r != "")
                        r += ",";
                    r += "4";
                }
            }
            label2.Text = "Raspuns validat!";
            answer_save[index] = r;
            MessageBox.Show("Raspuns salvat!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("Sunteti la prima intrebare!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                index--;
            load_question();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (index == nmax)
            {
                MessageBox.Show("Sunteti la ultima intrebare!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                index++;
            load_question();
        }
    }
}
