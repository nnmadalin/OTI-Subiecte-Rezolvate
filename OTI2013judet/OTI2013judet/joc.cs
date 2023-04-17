using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2013judet
{
    public partial class joc : Form
    {
        public joc()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new normal();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        string image_initial = "-1";
        public static string sec = "0";

        void move_enable_image(object sender, MouseEventArgs e)
        {
            if (finish == false)
            {
                PictureBox pct = sender as PictureBox;
                image_initial = pct.Tag.ToString();
                this.Cursor = Cursors.Cross;

                if (timer1.Enabled == false)
                {
                    timer1.Start();
                }
            }
        }

        //de ver daca e hover pe ceva :)

        void move_disable_image(object sender, MouseEventArgs e)
        {
            if (image_initial != "-1" && finish == false)
            {
                int x = this.PointToClient(Cursor.Position).X;
                int y = this.PointToClient(Cursor.Position).Y;
                bool fns = false;
                foreach (Control box in Controls)
                {
                    if (box is PictureBox)
                    {
                        if (box.Location.X <= x  && (box.Location.X + box.Width) >= x )
                        {
                            if (box.Location.Y <= y && (box.Location.Y + box.Height) >= y)
                            {
                                Bitmap bit = new Bitmap(((PictureBox)box).Image);
                                foreach (Control pct_send in Controls)
                                {
                                    if (pct_send is PictureBox)
                                    {
                                        if (pct_send.Tag.ToString() == image_initial && pct_send.Tag != box.Tag && box.Name != pct_send.Name)
                                        {
                                            Bitmap bit_send = new Bitmap(((PictureBox)pct_send).Image);

                                            ((PictureBox)box).Image = bit_send;
                                            ((PictureBox)pct_send).Image = bit;

                                            int a = Convert.ToInt32(box.Tag.ToString()) - 1;
                                            int b = Convert.ToInt32(pct_send.Tag.ToString()) - 1;

                                            int temp = rand[a];

                                            rand[a] = rand[b];
                                            rand[b] = temp;

                                            image_initial = "-1";
                                            this.Cursor = Cursors.Default;
                                            fns = true;
                                        }
                                        if (fns == true)
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    if (fns == true)
                        break;
                }
            }
            this.Cursor = Cursors.Default;
            image_initial = "-1";
        }
        

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
        }
        int[] rand = new int[11];
        bool finish = false;

        private void joc_Load(object sender, EventArgs e)
        {
            if(joc_nou.number_img == "4")
            {
                pictureBox3.Visible = false;
                pictureBox6.Visible = false;
                pictureBox9.Visible = false;

                pictureBox8.Visible = false;
                pictureBox7.Visible = false;

                Bitmap bit = new Bitmap(joc_nou.img_file);               

                rand = new int[] {1, 2, 3, 4};
                int seed = Convert.ToInt32(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString());
                var random = new Random(seed);

                bool org = true;
                while (org == true) 
                {
                    org = true;
                    rand = rand.OrderBy(x => random.Next()).ToArray();
                    for (int i = 1; i < rand.Length; i++)
                    {
                        if (rand[i] - 1 != rand[i - 1])
                        {
                            org = false;
                            break;
                        }
                    }
                }


                pictureBox1.Tag = "1";
                pictureBox2.Tag = "2";
                pictureBox4.Tag = "3";
                pictureBox5.Tag = "4";
                pictureBox3.Tag = "dasd";

                for (int i = 0; i < 4; i++)
                {
                    
                    foreach (Control box in Controls)
                    {
                        
                        if (box is PictureBox && box.Tag.ToString() == (i + 1).ToString())
                        {
                            if (rand[i] == 1)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(0, 0, bit.Width / 2, bit.Height / 2));
                            }
                            else if (rand[i] == 2)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width / 2, 0, bit.Width / 2, bit.Height / 2));
                            }
                            else if (rand[i] == 3)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(0, bit.Height / 2, bit.Width / 2, bit.Height / 2));
                            }
                            else if (rand[i] == 4)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width / 2, bit.Height / 2, bit.Width / 2, bit.Height / 2));
                            }
                        }
                    }
                }

            }
            else
            {
                Bitmap bit = new Bitmap(joc_nou.img_file);
                            

                rand = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9};
                int seed = Convert.ToInt32(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString());
                var random = new Random(seed);

                bool org = true;
                while (org == true) 
                {
                    org = true;
                    rand = rand.OrderBy(x => random.Next()).ToArray();
                    for (int i = 1; i < rand.Length; i++)
                    {
                        if (rand[i] - 1 != rand[i - 1])
                        {
                            org = false;
                            break;
                        }
                    }                    
                }

                for (int i = 0; i < 9; i++)
                {

                    foreach (Control box in Controls)
                    {

                        if (box is PictureBox && box.Tag.ToString() == (i + 1).ToString())
                        {
                            if (rand[i] == 1)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(0, 0, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 2)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width / 3, 0, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 3)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width - bit.Width / 3, 0, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 4)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(0, bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 5)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width / 3, bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 6)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width - bit.Width / 3, bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 7)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(0, bit.Height - bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 8)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width / 3, bit.Height - bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                            else if (rand[i] == 9)
                            {
                                ((PictureBox)box).Image = cropImage(bit, new Rectangle(bit.Width - bit.Width / 3, bit.Height - bit.Height / 3, bit.Width / 3, bit.Height / 3));
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < rand.Length; i++)
            {
                Console.Write(rand[i] + " ");
            }
            Console.WriteLine("");

        }
        int timer = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = timer.ToString() + " sec";
            timer++;

            
            for (int i = 0; i < rand.Length; i++)
            {
                Console.Write(rand[i] + " ");
            }
            Console.WriteLine("");

            bool org = true;
            for (int i = 1; i < rand.Length; i++)
            {
                if (rand[i] - 1 != rand[i - 1])
                {
                    org = false;
                    break;
                }
            }
            if(org == true)
            {
                timer1.Stop();
                MessageBox.Show("Ai terminat cu succes!");
                finish = true;

                sec = timer.ToString();

                var frm = new salvare_db();
                frm.ShowDialog();

                var form = new normal();
                form.Show();

                this.Hide();
            }

        }
    }
}
