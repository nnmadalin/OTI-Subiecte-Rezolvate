using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OTI2017judet
{
    public partial class genereaza_poster : Form
    {
        public genereaza_poster()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new home();
            form.Show();
            this.Hide();
        }

        void add_locatie()
        {
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Localitati]", conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox1.Items.Add(read["Nume"].ToString());
                }

                conn.Close();
            }
        }

        void add_image(int id)
        {
            comboBox2.Items.Clear();
            using (SqlConnection conn = new SqlConnection(home.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Imagini] where IDLocalitate = @id", conn);
                cmd.Parameters.Add("@id", id);

                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox2.Items.Add(read["CaleFisier"].ToString());
                }

                conn.Close();
            }
        }

        private void genereaza_poster_Load(object sender, EventArgs e)
        { 
            add_locatie();
            comboBox1.SelectedIndex = 0;
            add_image(comboBox1.SelectedIndex + 1);
            comboBox2.SelectedIndex = 0;
            string denumire = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            pictureBox1.ImageLocation = home.path_image + denumire;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex
            
            add_image(comboBox1.SelectedIndex + 1);
            comboBox2.SelectedIndex = 0;
            string denumire = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            pictureBox1.ImageLocation = home.path_image + denumire;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count < 10)
            {
                if (comboBox2.SelectedIndex != null)
                {
                    listBox1.Items.Add(comboBox2.Items[comboBox2.SelectedIndex]);
                }
            }
            else
            {
                MessageBox.Show("Maxim 10 poze poti adauga!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string denumire = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            pictureBox1.ImageLocation = home.path_image + denumire;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(1500, 1800);
            Graphics g = Graphics.FromImage(bit);

            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 1500, 3000));

            if(textBox1.Text.Trim() != null)
            {
                Font font = new Font(FontFamily.GenericSansSerif, 80, FontStyle.Bold);
                g.DrawString(textBox1.Text, font, Brushes.Black, new Point(10, 40));
            }

            if(listBox1.Items.Count != 0)
            {
                int x = 20, y = 200;
                int width = 450, height = 350;

                for(int i = 0; i < listBox1.Items.Count; i++)
                {
                    string path = home.path_image + listBox1.Items[i].ToString();
                    g.DrawImage(Image.FromFile(path), x, y, width, height);

                    if(x > 800)
                    {
                        x = 20;
                        y += height + 50;
                    }
                    else
                    {
                        x += width + 40;
                    }
                }

                string path_save = "";

                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog1.FileName = "";
                DialogResult dialog = saveFileDialog1.ShowDialog();
                if(dialog == DialogResult.OK)
                {
                    path_save = saveFileDialog1.FileName;
                    bit.Save(path_save);
                    MessageBox.Show("Poster salvat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
