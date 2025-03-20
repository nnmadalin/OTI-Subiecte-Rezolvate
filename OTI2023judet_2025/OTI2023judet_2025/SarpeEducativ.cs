using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace OTI2023judet_2025
{
    public partial class SarpeEducativ : Form
    {
        public SarpeEducativ()
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
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        Point[] sarpe = new Point[150];
        Point lastHeadPosition;
        int lenghtSarpe = 1;
        int lastDirection = 1; //1 - sus 2-dreapta 3-jos 4-stanga
        bool isFoodSpawned = false;
        Point mancareSpawn;

        void generareMancare()
        {
            Random rand = new Random();

            bool ok = true;

            while(ok == true)
            {
                ok = false;
                mancareSpawn = new Point(rand.Next(0, 12) * 50, rand.Next(0, 12) * 50);

                for(int i = 0; i < lenghtSarpe; i++)
                {
                    if (mancareSpawn == sarpe[i])
                        ok = true;
                }
            }
        }

        bool checkStateGame()
        {
            for(int i = 1; i < lenghtSarpe; i++)
            {
                if (sarpe[i] == sarpe[0])
                    return true;
            }

            if (sarpe[0].X <= -50 || sarpe[0].X >= 12*50)
                return true;
            if (sarpe[0].Y <= -50 || sarpe[0].Y >= 12 * 50)
                return true;

            return false;
        }

        void generateGame()
        {
            Bitmap bit = new Bitmap(600, 600);
            Graphics g = Graphics.FromImage(bit);
            g.FillEllipse(Brushes.Red, new Rectangle(mancareSpawn.X, mancareSpawn.Y, 50, 50));

            g.FillEllipse(Brushes.White, new Rectangle(sarpe[0].X, sarpe[0].Y, 50, 50));

            for(int i = 1; i < lenghtSarpe; i++)
            {
                g.FillEllipse(Brushes.Green, new Rectangle(sarpe[i].X, sarpe[i].Y, 50, 50));
            }



            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            timer1.Start();
            timer2.Start();
        }

        void updatePositionSnake()
        {
            lastHeadPosition = sarpe[lenghtSarpe - 1];

            for (int i = lenghtSarpe - 1; i > 0; i--)
            {
                sarpe[i] = sarpe[i - 1];
            }
            if(lastDirection == 1)
            {
                sarpe[0].Y-=50;
            }
            else if (lastDirection == 3)
            {
                sarpe[0].Y+=50;
            }
            else if (lastDirection == 2)
            {
                sarpe[0].X+=50;
            }
            else if (lastDirection == 4)
            {
                sarpe[0].X-=50;
            }

            if (sarpe[0] == mancareSpawn)
            {
                sarpe[lenghtSarpe] = lastHeadPosition;
                lenghtSarpe++;
                isFoodSpawned = false;


                pct += 10;

                timer1.Stop();
                timer2.Stop();

                Intrebare intr = new Intrebare();
                intr.ShowDialog();

                timer1.Start();
                timer2.Start();

                label3.Text = "Punctaj : " + pct.ToString();
            }
        }
        void addInDB()
        {
            SqlConnection conn = new SqlConnection(initial.dbConnection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [Rezultate] values(1, @email, @pct)", conn);
            cmd.Parameters.AddWithValue("@email", initial.emailuser);
            cmd.Parameters.AddWithValue("@pct", pct);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().ToLower() == "a" || textBox1.Text.Trim().ToLower() == "s" || textBox1.Text.Trim().ToLower() == "d" || textBox1.Text.Trim().ToLower() == "w")
            {
                if (lastDirection == 1 || lastDirection == 3)
                {
                    if (textBox1.Text.Trim().ToLower() == "a")
                        lastDirection = 4;
                    else if (textBox1.Text.Trim().ToLower() == "d")
                        lastDirection = 2;
                }
                else if (lastDirection == 2 || lastDirection == 4)
                {
                    if (textBox1.Text.Trim().ToLower() == "w")
                        lastDirection = 1;
                    else if (textBox1.Text.Trim().ToLower() == "s")
                        lastDirection = 3;
                }
            }

            textBox1.Clear();
        }

        public static int pct = 0;

        private void timer2_Tick(object sender, EventArgs e)
        {
            updatePositionSnake();

            if (checkStateGame() == true)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("Ai pierdut");
                addInDB();
                this.Close();
            }
            else
            {
                if (isFoodSpawned == false)
                {
                    isFoodSpawned = true;
                    generareMancare();
                }

                generateGame();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
        private void SarpeEducativ_Load(object sender, EventArgs e)
        {
            Random rand = new Random();

            sarpe[0] = new Point(rand.Next(2, 10) * 50, rand.Next(2, 10) * 50);
            lastHeadPosition = sarpe[0];

            generateGame();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            addInDB();
            this.Close();
        }
    }
}
