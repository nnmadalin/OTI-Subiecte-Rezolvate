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
using System.IO;

namespace OTI2013judet_2025
{
    public partial class joc : Form
    {
        public joc()
        {
            InitializeComponent();
        }

        Image[] imageInit;
        int[] pozImage;

        Boolean isStopGame = false;

        int startPicture = 0;
        int timer = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point _mouse;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private static Image cropImage (Image img, Rectangle cropArea)
        {
            Bitmap bit = new Bitmap(cropArea.Width, cropArea.Height);
            using ( Graphics g = Graphics.FromImage(bit))
            {
                g.DrawImage(img, new Rectangle(0, 0, bit.Width, bit.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bit;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        void loadImage4()
        {
            alegeJoc alegeJoc = new alegeJoc();

            Image img = Image.FromFile(alegeJoc.locationImage);
            int k = 0;

            imageInit = new Image[4];


            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    imageInit[k] = cropImage(img, new Rectangle(img.Width / 2 * j, img.Height / 2 * i, img.Width / 2, img.Height / 2));
                    k++;
                }
            }

            pozImage = new int[] { 0, 1, 2, 3};


            Random random = new Random();

            pozImage = pozImage.OrderBy(pozImage => random.Next()).ToArray();

            pictureBox1.Image = imageInit[pozImage[0]];
            pictureBox2.Image = imageInit[pozImage[1]];
            pictureBox4.Image = imageInit[pozImage[2]];
            pictureBox5.Image = imageInit[pozImage[3]];
        }

        void loadImage9()
        {
            alegeJoc alegeJoc = new alegeJoc();

            Image img = Image.FromFile(alegeJoc.locationImage);
            int k = 0;

            imageInit = new Image[9];


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    imageInit[k] = cropImage(img, new Rectangle(img.Width / 3 * j, img.Height / 3 * i, img.Width / 3, img.Height / 3));
                    k++;
                }
            }

            pozImage = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8};


            Random random = new Random();

            pozImage = pozImage.OrderBy(pozImage => random.Next()).ToArray();

            pictureBox1.Image = imageInit[pozImage[0]];
            pictureBox2.Image = imageInit[pozImage[1]];
            pictureBox3.Image = imageInit[pozImage[2]];
            pictureBox4.Image = imageInit[pozImage[3]];
            pictureBox5.Image = imageInit[pozImage[4]];
            pictureBox6.Image = imageInit[pozImage[5]];
            pictureBox7.Image = imageInit[pozImage[6]];
            pictureBox8.Image = imageInit[pozImage[7]];
            pictureBox9.Image = imageInit[pozImage[8]];
        }

        private void joc_Load(object sender, EventArgs e)
        {
            alegeJoc alegeJoc = new alegeJoc();

            if(alegeJoc.tipPatrat == "4")
            {
                this.Height = 318;

                pictureBox3.Visible = pictureBox6.Visible = pictureBox9.Visible = pictureBox7.Visible = pictureBox8.Visible = false;
                pictureBox4.Tag = 2;
                pictureBox5.Tag = 3;
                loadImage4();
            }
            else
            {
                loadImage9();
            }
        }

        private void mouseUpPicture(object sender, MouseEventArgs e)
        {

            Point locationMouse = this.PointToClient(Cursor.Position);

            int finishPicture = -1;


            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Visible == true  && locationMouse.X >= c.Location.X  && locationMouse.X <= c.Location.X + c.Width && locationMouse.Y >= c.Location.Y && locationMouse.Y <= c.Location.Y + c.Height)
                {
                    finishPicture = Convert.ToInt32(c.Tag);
                }
            }


            Console.WriteLine(finishPicture);
            Console.WriteLine("");

            if (finishPicture != -1 && finishPicture != startPicture)
            {
                int aux = pozImage[finishPicture];
                pozImage[finishPicture] = pozImage[startPicture];
                pozImage[startPicture] = aux;

                for (int i = 0; i < 4; i++)
                    Console.WriteLine(pozImage[i]);

                Console.WriteLine("");

                foreach (Control c in this.Controls)
                {
                    if (c is PictureBox && Convert.ToInt32(c.Tag) == startPicture)
                    {
                        (c as PictureBox).Image = imageInit[pozImage[startPicture]];
                    }
                    if (c is PictureBox && Convert.ToInt32(c.Tag) == finishPicture)
                    {
                        (c as PictureBox).Image = imageInit[pozImage[finishPicture]];
                    }
                }

            }
        }

        private void mouseDownPicture(object sender, MouseEventArgs e)
        {
            if(timer1.Enabled == false && isStopGame == false)
            {
                timer1.Enabled = true;
            }

            startPicture = Convert.ToInt32((sender as PictureBox).Tag);
            Console.WriteLine(startPicture);
        }

        void writeInFile()
        {
            alegeJoc alegeJoc = new alegeJoc();

            StreamWriter writer = new StreamWriter(Application.StartupPath + "/Resurse/Data/Clasament.txt", true);
            writer.WriteLine(alegeJoc.numeJucator + " " + (timer / 60).ToString() + ":" + (timer % 60).ToString()  + " " + alegeJoc.tipPatrat);
            writer.Flush();

            writer.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            label3.Text = "Timp: " + (timer / 60).ToString() + ":" + (timer % 60).ToString();

            alegeJoc alegeJoc = new alegeJoc();

            Boolean ok = true;
            for (int i = 0; i < Convert.ToInt32(alegeJoc.tipPatrat); i++)
            {
                if (pozImage[i] != i)
                {
                    ok = false;
                }
            }

            if (ok == true)
            {
                isStopGame = true;
                timer1.Enabled = false;
                MessageBox.Show("Ai terminat cu succes jocul!");
                writeInFile();

                this.Hide();
                alegeJoc.Show();
            }

        }
    }
}
