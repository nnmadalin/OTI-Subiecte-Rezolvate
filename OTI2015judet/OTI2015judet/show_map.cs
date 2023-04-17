using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2015judet
{
    public partial class show_map : Form
    {
        public show_map()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            home.q = 3;
        }

        private void show_map_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Application.StartupPath + "/Resurse_C#/MareaNeagra.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            string p = normal.arr[normal.id];

            string[] spl = p.Split(',');

            
        }
    }
}
