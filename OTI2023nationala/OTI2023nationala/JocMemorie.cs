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
using System.Data.SqlTypes;
using System.Data.Sql;

namespace OTI2023nationala
{
    public partial class JocMemorie : Form
    {
        public JocMemorie()
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

        Point mouse;

        int generateNumberCard(int n)
        {
            if (n <= 2)
                return 1;
            else
                return generateNumberCard(n - 1) + generateNumberCard(n - 2);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - mouse.X, this.Location.Y + e.Location.Y - mouse.Y);
            }
        }

        int  nGame = 3, timer = 100;
        
        bool isSelectTop = false, isSelectBottom = false;
        int idSelectTop = -1, idSelectBottom = -1, points = 0;
        bool[] isClickedTop = new bool[20];
        bool[] isClickedBottom = new bool[20];
        DateTime[] dateSelectTop = new DateTime[100];
        DateTime[] dateSelectBottom = new DateTime[100];

        string[] nameImage = { "avion", "bloc", "caine", "caprioara", "iepure", "leu", "lup", "masina", "minge", "patine", "pisica", "taur", "urs", "vulpe" };
        string[] randomImageRowTop = new string[100];
        string[] randomImageRowBottom = new string[100];

        private void JocMemorie_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(button3.Enabled == false)
            {
                for (int i = 0; i < generateNumberCard(nGame); i++)
                {
                    DateTime now = DateTime.Now;

                    if (dateSelectTop[i].AddSeconds(1) < now)
                    {
                        foreach (Control ctrl in panel2.Controls)
                        {
                            if (ctrl.Tag.ToString() == i.ToString() && ctrl.Name.Contains("TOP") == true && isClickedTop[i] == false)
                            {
                                ((PictureBox)ctrl).Image = null;
                            }
                        }
                    }
                }

                for (int i = 0; i < generateNumberCard(nGame); i++)
                {
                    DateTime now = DateTime.Now;

                    if (dateSelectBottom[i].AddSeconds(1) < now)
                    {
                        foreach (Control ctrl in panel2.Controls)
                        {
                            if (ctrl.Tag.ToString() == i.ToString() && ctrl.Name.Contains("BOTTOM") == true && isClickedBottom[i] == false)
                            {
                                ((PictureBox)ctrl).Image = null;
                            }
                        }
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer--;

            label3.Text = timer.ToString();

            if(timer == 0)
            {
                timer2.Stop();
                points = 0;
                MessageBox.Show("Ai Pierduit! :(");
                nGame = 3;
                timer = 100;
                label3.Text = timer.ToString();

                button3.Enabled = true;
                panel2.Controls.Clear();

                SqlConnection conn = new SqlConnection(Form1.db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@type, @email, @pct, @data)", conn);
                cmd.Parameters.AddWithValue("@type", "0");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@pct", points);
                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                conn.Close();

                var frm2 = new AlegeJoc();
                frm2.Show();
                this.Hide();

            }

            bool isFinish = true;

            for(int i = 0; i < generateNumberCard(nGame); i++)
            {
                if (isClickedTop[i] == false)
                    isFinish = false;
            }

            if(isFinish == true)
            {
                if (timer >= 85)
                    points += 20;
                else if (timer >= 10)
                    points += (10 + (timer / 10));
                else
                    points += timer;

                timer2.Stop();
                var frm = new winForm();
                frm.ShowDialog();
                nGame++;
                timer = 100;
                label3.Text = timer.ToString();

                button3.Enabled = true;
                panel2.Controls.Clear();

                if(nGame == 7)
                {
                    MessageBox.Show("Ai castigat! :)");

                    SqlConnection conn = new SqlConnection(Form1.db);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@type, @email, @pct, @data)", conn);
                    cmd.Parameters.AddWithValue("@type", "0");
                    cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                    cmd.Parameters.AddWithValue("@pct", points);
                    cmd.Parameters.AddWithValue("@data", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    var frm2 = new AlegeJoc();
                    frm2.Show();
                    this.Hide();

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            loadGame();

            timer2.Enabled = true;
            timer2.Start();
        }

        private void pictureBoxTop_Click(object sender, EventArgs e)
        {
            PictureBox pct = sender as PictureBox;

            int id = Convert.ToInt32(pct.Tag);
            if(isClickedTop[id] == false)
            {
                isSelectTop = true;
                idSelectTop = id;

                pct.Image = Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Imagini/" + randomImageRowTop[id] + ".png");
                dateSelectTop[id] = DateTime.Now;

                if (isSelectBottom == true)
                {
                    isSelectTop = false;
                    isSelectBottom = false;

                    if (randomImageRowTop[idSelectTop] == randomImageRowBottom[idSelectBottom])
                    {
                        isClickedTop[idSelectTop] = true;
                        isClickedBottom[idSelectBottom] = true;

                        Bitmap bit = new Bitmap(100, 100);
                        Graphics g = Graphics.FromImage(bit);
                        g.DrawString(id.ToString() + " - " +randomImageRowBottom[idSelectBottom], new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(10, 50));

                        
                        foreach (Control ctrl in panel2.Controls)
                        {
                            if (ctrl.Tag.ToString() == idSelectBottom.ToString() && ctrl.Name.Contains("BOTTOM") == true)
                            {
                                ((PictureBox)ctrl).Image = bit;
                            }
                        }
                    }
                }
            }
        }

        private void pictureBoxBottom_Click(object sender, EventArgs e)
        {
            PictureBox pct = sender as PictureBox;

            int id = Convert.ToInt32(pct.Tag);
            if (isClickedBottom[id] == false)
            {
                isSelectBottom = true;
                idSelectBottom = id;

                Bitmap bit = new Bitmap(100, 100);
                Graphics g = Graphics.FromImage(bit);
                g.DrawString(randomImageRowBottom[id], new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(10, 50));

                pct.Image = bit;
                dateSelectBottom[id ] = DateTime.Now;

                if(isSelectTop == true)
                {
                    isSelectTop = false;
                    isSelectBottom = false;

                    if(randomImageRowTop[idSelectTop] == randomImageRowBottom[idSelectBottom])
                    {
                        isClickedTop[idSelectTop] = true;
                        isClickedBottom[idSelectBottom] = true;

                        bit = new Bitmap(100, 100);
                        g = Graphics.FromImage(bit);
                        g.DrawString(idSelectTop + " - " + randomImageRowBottom[id], new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(10, 50));

                        pct.Image = bit;

                        foreach (Control ctrl in panel2.Controls)
                        {
                            if (ctrl.Tag.ToString() == idSelectTop.ToString() && ctrl.Name.Contains("TOP") == true)
                            {
                                ((PictureBox)ctrl).Image = Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Imagini/" + randomImageRowTop[idSelectTop] + ".png"); ;
                            }
                        }
                    }
                }
            }
        }
        
        void loadGame()
        {
            isSelectTop = false; isSelectBottom = false;
            idSelectTop = -1; idSelectBottom = -1;
            isClickedTop = new bool[20];
            isClickedBottom = new bool[20];
            dateSelectTop = new DateTime[100];
            dateSelectBottom = new DateTime[100];
           

            Random rand = new Random();
            nameImage = nameImage.OrderBy(x => rand.Next()).ToArray();

            randomImageRowTop = new string[generateNumberCard(nGame)];
            randomImageRowBottom = new string[generateNumberCard(nGame)];

            for (int i = 0; i < generateNumberCard(nGame); i++)
            {
                randomImageRowTop[i] = nameImage[i];
                randomImageRowBottom[i] = nameImage[i];
            }
            randomImageRowTop = randomImageRowTop.OrderBy(x => rand.Next()).ToArray();
            randomImageRowBottom = randomImageRowBottom.OrderBy(x => rand.Next()).ToArray();

            panel2.Controls.Clear();

            Point poz = new Point(20, 20);

            for (int i = 0; i < generateNumberCard(nGame); i++)
            {
                PictureBox pct = new PictureBox();
                pct.Size = new Size(100, 100);
                pct.Location = poz;
                pct.BackColor = Color.Azure;
                pct.SizeMode = PictureBoxSizeMode.StretchImage;
                pct.Name = "TOP_"+i.ToString();
                pct.Tag = i.ToString();
                pct.Click += pictureBoxTop_Click;

                poz.X += 105;

                panel2.Controls.Add(pct);
            }

            poz = new Point(20, 125);

            for (int i = 0; i < generateNumberCard(nGame); i++)
            {
                PictureBox pct = new PictureBox();
                pct.Size = new Size(100, 100);
                pct.Location = poz;
                pct.BackColor = Color.Azure;
                pct.SizeMode = PictureBoxSizeMode.StretchImage;
                pct.Name = "BOTTOM_" + i.ToString();
                pct.Tag = i.ToString();
                pct.Click += pictureBoxBottom_Click;
                poz.X += 105;

                panel2.Controls.Add(pct);
            }
        }
    }
}
