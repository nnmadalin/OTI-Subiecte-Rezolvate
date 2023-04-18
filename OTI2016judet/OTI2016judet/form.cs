using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2016judet
{
    public partial class form : System.Windows.Forms.Form
    {
        public form()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=3";

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Application.StartupPath + "/Resurse_C#/good-food-3.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var show = new creare_cont();
            show.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var show = new autentificare();
            show.Show();
            this.Hide();
        }
    }
}
