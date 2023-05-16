using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OTI2022nationala
{
    public partial class interfenteeco : Form
    {
        public interfenteeco()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void interfenteeco_Load(object sender, EventArgs e)
        {
            label1.Text = "Interferențe ECO – " + logare.username;
            panel2.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OTI_2022_C#_resurse/Wood/Wood2.jpeg");
            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back" + logare.id_img + ".jpg");
            load_triangle(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            draw_line();
            backup = pictureBox1.Image;
        }

        string path = "";
        int pozitie_triangle = 0;
        bool selectat_deflector = false;
        Image backup;

        void draw_line()
        {
            if (checkBox1.Checked == false)
            {
                pictureBox1.Image = null;
            }
            else
            {
                Bitmap bit = null;
                if (backup != null)
                    bit = new Bitmap(backup);
                else
                    bit = new Bitmap(1000, 600);
                Graphics g = Graphics.FromImage(bit);
                int x = 0, y = 0;
                for (int i = 0; i < 10; i++)
                {
                    x = 0;
                    for (int j = 0; j < 20; j++)
                    {
                        Pen white = new Pen(Brushes.White, 2);
                        g.DrawRectangle(white, new Rectangle(x, y, 50, 60));
                        x += 50;
                    }
                    y += 60;
                }
                pictureBox1.Image = bit;               
            }
            if (path != "")
                load_text();
        }

        void add_image(string path, Point dxy) 
        {
            Image image = Image.FromFile(path);
            Bitmap bit;
            if (pictureBox1.Image == null)
                bit = new Bitmap(1000, 600);
            else
                bit = new Bitmap(backup);

            Graphics g = Graphics.FromImage(bit);
            g.DrawImage(image, (dxy.X - 1) * 50, (dxy.Y - 1) * 60, 50, 60);
            pictureBox1.Image = bit;
        }

        void load_text()
        {
            path = openFileDialog1.FileName;
            using (StreamReader read = new StreamReader(path))
            {
                string row;
                while ((row = read.ReadLine()) != null)
                {
                    string[] split = row.Split(' ');
                    Point dxy = new Point(Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));
                    string path_image = "";
                    if (split[0] == "Meduza1")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/Meduze/Meduza1.png";
                    }
                    else if (split[0] == "Meduza2")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/Meduze/Meduza2.png";
                    }
                    else if (split[0] == "Meduza3")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/Meduze/Meduza3.png";
                    }
                    else if (split[0] == "Meduza4")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/Meduze/Meduza4.png";
                    }
                    else if (split[0] == "Robot")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/Robot/Robot.png";
                    }
                    else if (split[0] == "Hartie")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/MaterialeReciclabile/Hartie.png";
                    }
                    else if (split[0] == "Plastic")
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/MaterialeReciclabile/Plastic.png";
                    }
                    else
                    {
                        path_image = Application.StartupPath + @"/OTI_2022_C#_resurse/MaterialeReciclabile/Sticla.png";
                    }

                    add_image(path_image, dxy);
                }
            }
        }

        void load_triangle(int poz)
        {
            if(poz == 0)
            {
                Bitmap bit = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(bit);
                g.FillPolygon(Brushes.White, new Point[] { new Point(0, 0), new Point(50, 0), new Point(0, 50) });
                pictureBox2.Image = bit;
            }
            else if (poz == 1)
            {
                Bitmap bit = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(bit);
                g.FillPolygon(Brushes.White, new Point[] { new Point(50, 0), new Point(0, 0), new Point(50, 50) });
                pictureBox2.Image = bit;
            }
            else if (poz == 2)
            {
                Bitmap bit = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(bit);
                g.FillPolygon(Brushes.White, new Point[] { new Point(50, 50), new Point(0, 50), new Point(50, 0) });
                pictureBox2.Image = bit;
            }
            else if (poz == 3)
            {
                Bitmap bit = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(bit);
                g.FillPolygon(Brushes.White, new Point[] { new Point(0, 50), new Point(0, 0), new Point(50, 50) });
                pictureBox2.Image = bit;
            }
        }

        void draw_on_picture(int x, int y)
        {
            Bitmap bit = null;            
            if (backup != null)
                bit = new Bitmap(backup);
            else
                bit = new Bitmap(1000, 600);

            Graphics g = Graphics.FromImage(bit);
            g.DrawImage(pictureBox2.Image, x, y, 50, 60);

            pictureBox1.Image = bit;
            GC.Collect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "/OTI_2022_C#_resurse/";
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                pictureBox1.Image = null;
                if(checkBox1.Checked == true)
                {
                    draw_line();
                }
                load_text();
                backup = pictureBox1.Image;
            }
            selectat_deflector = false;
            pictureBox1.Image = backup;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pozitie_triangle++;
            if (pozitie_triangle == 4)
                pozitie_triangle = 0;
            load_triangle(pozitie_triangle);
            selectat_deflector = false;
            pictureBox1.Image = backup;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selectat_deflector = true;

            if(pictureBox1.Image != null)
                backup = pictureBox1.Image;
            MessageBox.Show("Ai selectat un deflector!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point point = pictureBox1.PointToClient(Cursor.Position);
            int col = 0, lin = 0;

            for (int i = 0; i < 20; i++)
            {
                if (Convert.ToInt32(point.X.ToString()) >= 50 * i)
                    col = 50 * i;

            }

            for (int i = 0; i < 10; i++)
            {
                if (Convert.ToInt32(point.Y.ToString()) >= 60 * i)
                    lin = 60 * i;
            }
            if (selectat_deflector == true)
            {
                draw_on_picture(col, lin);
                backup = pictureBox1.Image;
                selectat_deflector = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = pictureBox1.PointToClient(Cursor.Position);
            int col = 0, lin = 0;

            for (int i = 0; i < 20; i++)
            {
                if (Convert.ToInt32(point.X.ToString()) >= 50 * i)
                    col = 50 * i;
                
            }

            for (int i = 0; i < 10; i++)
            {
                if (Convert.ToInt32(point.Y.ToString()) >= 60 * i)
                    lin = 60 * i;
            }
            //Console.WriteLine(col.ToString() + " " + lin.ToString());
            if (selectat_deflector == true)
            {
                draw_on_picture(col, lin);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            selectat_deflector = false;
            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back" + logare.id_img + ".jpg");
            pictureBox1.Image = backup;

        }
    }
}
