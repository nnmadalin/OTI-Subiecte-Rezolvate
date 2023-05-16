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
    public partial class logare : Form
    {
        public logare()
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
            if(e.Button == MouseButtons.Left)
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

        string[] password = new string[50];
        public static string id_img = "", username = "";

        private void logare_Load(object sender, EventArgs e)
        {

            pictureBox1.ImageLocation = Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back1.jpg";
            pictureBox2.ImageLocation = Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back2.jpg";
            pictureBox3.ImageLocation = Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back3.jpg";
            pictureBox4.ImageLocation = Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back4.jpg";
            pictureBox5.ImageLocation = Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back5.jpg";
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OTI_2022_C#_resurse/Background/Back4.jpg");

            int k = 0;
            using (StreamReader reader = new StreamReader(Application.StartupPath + "/OTI_2022_C#_resurse/Useri.txt"))
            {
                string row;
                while((row = reader.ReadLine()) != null)
                {
                    string[] split = row.Split(' ');
                    comboBox1.Items.Add(split[0]);
                    password[k++] = split[1];
                }
            }
            comboBox1.SelectedIndex = 0;

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pct = sender as PictureBox;
            id_img = pct.Tag.ToString();
            username = comboBox1.SelectedItem.ToString();

            if(textBox1.Text == password[comboBox1.SelectedIndex])
            {
                var frm = new interfenteeco();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Parola gresita!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                textBox1.Clear();
            }

        }
    }
}
