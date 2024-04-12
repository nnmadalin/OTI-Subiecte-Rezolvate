using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace OTI2023judet
{
    public partial class Sarpe : Form
    {
        public Sarpe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new AlegeJoc();
            frm.Show();
            this.Hide();
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

        int direction = 3;

        //1 -> sus 2 -> dr 3 -> jos 4 -> st

        Point food;
        Point[] position = new Point[100];
        Point[] positionLast = new Point[100];
        int snakeLenght = 1, snakelenghtLast = 1;
        public static int score = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            timer1.Stop();
            MessageBox.Show("Din pacate ai pierdut! Felicitari pentru scor!");

            add_DB();
        }

        void add_DB()
        {
            using (SqlConnection conn = new SqlConnection(Form1.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] VALUES (@tip, @email, @punctaj)", conn);
                cmd.Parameters.AddWithValue("@tip", "1");
                cmd.Parameters.AddWithValue("@email", Form1.emailUser);
                cmd.Parameters.AddWithValue("@punctaj", score);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            var frm = new AlegeJoc();
            frm.Show();
            this.Hide();
        }

        private void Sarpe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.ToString().ToLower() == "w")
            {
                if (direction == 2 || direction == 4)
                    direction = 1;
            }
            else if (e.KeyChar.ToString().ToLower() == "s")
            {
                if (direction == 2 || direction == 4)
                    direction = 3;
            }
            else if (e.KeyChar.ToString().ToLower() == "a")
            {
                if (direction == 1 || direction == 3)
                    direction = 4;
            }
            else if (e.KeyChar.ToString().ToLower() == "d")
            {
                if (direction == 1 || direction == 3)
                    direction = 2;
            }
        }

        void randomFood()
        {
            Random random = new Random(DateTime.Now.Second);
            food = new Point(random.Next(0, 16), random.Next(0, 16));

            bool ok = false;
            while (ok == false)
            {
                ok = true;
               for(int i = 0; i < snakeLenght; i++)
               {
                    if (food == position[i])
                        ok = false;
               }
               if(ok == false)
                food = new Point(random.Next(0, 16), random.Next(0, 16));
            }
        }

        void load_bitmap()
        {
            Bitmap bit = new Bitmap(510, 510);

            using(Graphics gp = Graphics.FromImage(bit))
            {
                for(int i = 0; i < snakeLenght; i++)
                {
                    if(i == 0)
                        gp.FillEllipse(Brushes.White, new RectangleF(30 * position[i].X, 30 * position[i].Y, 30, 30));
                    else
                        gp.FillEllipse(Brushes.Green, new RectangleF(30 * position[i].X, 30 * position[i].Y, 30, 30));
                }

                gp.FillEllipse(Brushes.Red, new RectangleF(30 * food.X, 30 * food.Y, 30, 30));
            }

            

            pictureBox1.Image = bit;
        }

        bool check_crash()
        {
            for(int i = 1; i < snakeLenght - 1; i++)
            {
                if (position[i] == position[0])
                    return true;
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Point newPoint = new Point(0, 0);
            if (direction == 1)
            {
                newPoint = new Point(position[0].X, position[0].Y - 1);          
            }
            else if (direction == 2)
            {
                newPoint = new Point(position[0].X + 1, position[0].Y);
            }
            else if (direction == 3)
            {
                newPoint = new Point(position[0].X, position[0].Y + 1);
            }
            else if (direction == 4)
            {
                newPoint = new Point(position[0].X - 1, position[0].Y);
            }

            if (newPoint.X < 0 || newPoint.X > 17 || newPoint.Y < 0 || newPoint.Y > 17)
            {
                timer1.Stop();
                MessageBox.Show("Din pacate ai pierdut! Felicitari pentru scor!");
                add_DB();
                
            }
            else if (check_crash() == true)
            {
                timer1.Stop();
                MessageBox.Show("Din pacate ai pierdut! Felicitari pentru scor!");
                add_DB();
                
            }
            else
            {
                positionLast = position;
                snakelenghtLast = snakeLenght;

                for (int i = snakeLenght - 1; i >= 1; i--)
                {
                    position[i] = position[i - 1];
                }
                position[0] = newPoint;
                load_bitmap();
                if (newPoint == food)
                {
                    position[snakeLenght] = positionLast[snakeLenght - 1];
                    snakeLenght++;
                    score += 10;

                    timer1.Stop();

                    var frm = new Intrebare();

                    DialogResult dr = frm.ShowDialog();

                    if(dr == DialogResult.OK)
                    {
                        timer1.Start();
                    }

                    randomFood();
                }
            }

            label2.Text = "Punctaj: " + score.ToString();

            load_bitmap();
        }

        private void Sarpe_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            
            Random random = new Random(DateTime.Now.Second);

            position[0] = new Point(random.Next(3, 13), random.Next(3, 13));

            food = new Point(random.Next(0, 16), random.Next(0, 16));
             while (food == position[0])
             {
                food = new Point(random.Next(0, 16), random.Next(0, 16));
             }
            load_bitmap();
        }
    }
}
