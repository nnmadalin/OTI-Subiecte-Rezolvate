using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2023nationala
{
    public partial class winForm : Form
    {
        public winForm()
        {
            InitializeComponent();
        }

        int index = 1;

        private void winForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();

            Random rand = new Random();



            for(int i = 1; i <= 20; i++)
            {
                PictureBox pct = new PictureBox();
                pct.Location = new Point(rand.Next(0, 863), rand.Next(0, 547));
                pct.Size = new Size(150, 150);
                pct.SizeMode = PictureBoxSizeMode.StretchImage;

                this.Controls.Add(pct);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach(Control pct in this.Controls)
            {
                if(pct is PictureBox)
                {
                    string number;
                    if (index < 10)
                        number = "0" + index.ToString();
                    else
                        number = index.ToString();
                    ((PictureBox)pct).Image = Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Artificii/artificie_"+ number+".png");
                }
            }
            index++;
            if (index == 34)
                index = 1;
        }
    }
}
