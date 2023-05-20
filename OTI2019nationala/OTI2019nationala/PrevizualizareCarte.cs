using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2019nationala
{
    public partial class PrevizualizareCarte : Form
    {
        public PrevizualizareCarte()
        {
            InitializeComponent();
        }

        private void PrevizualizareCarte_Load(object sender, EventArgs e)
        {
            textBox1.Text = BibliotecarBiblioteca.titlu_carte;
            textBox2.Text = BibliotecarBiblioteca.autor_carte;
            textBox3.Text = BibliotecarBiblioteca.pag_carte;

            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "/Resurse/Imagini/carti/" + BibliotecarBiblioteca.id_carte + ".jpg");
        }

        Bitmap crop_img(Image img, Rectangle rect)
        {
            Bitmap bit = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(bit);
            g.DrawImage(img, new Rectangle(0, 0, bit.Width, bit.Height), rect, GraphicsUnit.Pixel);
            return bit;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Image img = pictureBox1.BackgroundImage;
            e.Graphics.DrawImage(img, new Rectangle(150, 150, 300, 300));

            Image st1 = crop_img(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2));
            Image st2 = crop_img(img, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2));
            Image st3 = crop_img(img, new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2));
            Image st4 = crop_img(img, new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2));

            e.Graphics.DrawImage(st1, new Rectangle(150 - 75, 150 - 75, 150, 150));
            e.Graphics.DrawImage(st2, new Rectangle(450 - 75, 150 - 75, 150, 150));
            e.Graphics.DrawImage(st3, new Rectangle(150 - 75, 450 - 75, 150, 150));
            e.Graphics.DrawImage(st4, new Rectangle(450 - 75, 450 - 75, 150, 150));

            img = st1;

            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37, 75 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2)), new Rectangle(150 + 37, 75 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37, 150 + 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(150 + 37, 150 + 37, 75, 75));

            img = st2;

            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37, 75 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2)), new Rectangle(488, 75 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37, 151 + 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(488, 151 + 37, 75, 75));

            img = st3;

            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37, 375 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37 + 150, 375 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37, 375 - 37 + 150, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(75 - 37 + 150, 375 - 37 + 150, 75, 75));

            img = st4;

            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37, 375 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37 + 150, 375 - 37, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37, 375 - 37 + 150, 75, 75));
            e.Graphics.DrawImage(crop_img(img, new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2)), new Rectangle(375 - 37 + 150, 375 - 37 + 150, 75, 75));
        }

        bool zoom = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if(zoom == false)
            {
                zoom = true;
                Random random = new Random();
                int x = Convert.ToInt32(random.Next(2, 10));
                pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "/Resurse/Imagini/carti/" + BibliotecarBiblioteca.id_carte + ".jpg");
                Image img = pictureBox1.BackgroundImage;
                pictureBox1.BackgroundImage = crop_img(img, new Rectangle(400 / x, 400 / x, 400/x, 400/x));
            }
            else
            {
                pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "/Resurse/Imagini/carti/" + BibliotecarBiblioteca.id_carte + ".jpg");
                zoom = false;
            }
        }
    }
    
}
