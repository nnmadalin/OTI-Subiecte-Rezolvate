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

namespace OTI2022judet
{
    public partial class vizualizare : Form
    {
        public vizualizare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
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

        string path = "0";

        void load_map()
        {
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Harti] where NumeHarta = @nume", conn);
                if(comboBox1.SelectedItem == null)
                    cmd.Parameters.Add("@nume", 1);
                else
                    cmd.Parameters.Add("@nume", comboBox1.SelectedItem.ToString());
                SqlDataReader read = cmd.ExecuteReader();
                try
                {

                    if (read.Read())
                    {
                        pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/" + read["FisierHarta"].ToString());
                        pictureBox2.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/" + read["FisierHarta"].ToString());
                        path = "1";
                    }
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            }
        }

        void make_cerc(int dx, int dy, string x)
        {
            Bitmap bit = new Bitmap(pictureBox1.BackgroundImage);
            Graphics g = Graphics.FromImage(bit);

            Font font = new Font("Arial", 12, FontStyle.Bold);

            if (Convert.ToInt32(x) < 20)
            {
                g.DrawEllipse(Pens.Green, dx, dy, 20, 20);
                g.DrawString(x, font, Brushes.Green, dx, dy);
            }
            else if (Convert.ToInt32(x) >= 20 && Convert.ToInt32(x) <= 40)
            {
                g.DrawEllipse(Pens.Yellow, dx, dy, 20, 20);
                g.DrawString(x, font, Brushes.Yellow, dx, dy);
            }
            else
            { 
                g.DrawEllipse(Pens.Red, dx, dy, 20, 20);
                g.DrawString(x, font, Brushes.Red, dx, dy);
            }

            pictureBox1.BackgroundImage = bit;
            pictureBox2.BackgroundImage = bit;
        }

        void make_line(Point a, Point b)
        {
            Bitmap bit = new Bitmap(pictureBox2.BackgroundImage);
            Graphics g = Graphics.FromImage(bit);

            g.DrawLine(Pens.Red, b, a);

            pictureBox2.BackgroundImage = bit;
        }

        void load_db()
        {
            
            if (path == "1")
            {
                load_map();
                using (SqlConnection conn = new SqlConnection(autentificare.db))
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from [Masurare] where IdHarta = @id", conn);
                    if (comboBox1.SelectedIndex != null)
                        cmd.Parameters.Add("@id", Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1);
                    SqlDataReader read = cmd.ExecuteReader();
                    try
                    {
                        while (read.Read())
                        {
                            DateTime time = Convert.ToDateTime(read["DataMasurare"].ToString());
                            if (dateTimePicker1.Value.Day == time.Day && dateTimePicker1.Value.Month == time.Month && dateTimePicker1.Value.Year == time.Year)
                            {
                                int dx = Convert.ToInt32(read["PozitieX"].ToString());
                                int dy = Convert.ToInt32(read["PozitieY"].ToString());
                                if (comboBox2.SelectedIndex == 1 && Convert.ToInt32(read["ValoareMasurare"].ToString()) < 20)
                                {
                                    make_cerc(dx, dy, read["ValoareMasurare"].ToString());
                                }
                                else if (comboBox2.SelectedIndex == 2 && Convert.ToInt32(read["ValoareMasurare"].ToString()) >= 20 && Convert.ToInt32(read["ValoareMasurare"].ToString()) <= 40)
                                {
                                    make_cerc(dx, dy, read["ValoareMasurare"].ToString());
                                }
                                else if (comboBox2.SelectedIndex == 3 && Convert.ToInt32(read["ValoareMasurare"].ToString()) > 40)
                                {
                                    make_cerc(dx, dy, read["ValoareMasurare"].ToString());
                                }
                                else if (comboBox2.SelectedIndex == 0)
                                {
                                    make_cerc(dx, dy, read["ValoareMasurare"].ToString());
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    conn.Close();
                }
            }
        }

        bool cauta_poz(int x, int y)
        {
            if (path == "1")
            {
                using (SqlConnection conn = new SqlConnection(autentificare.db))
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from [Masurare] where IdHarta = @id", conn);
                    if (comboBox1.SelectedIndex != null)
                        cmd.Parameters.Add("@id", Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1);
                    SqlDataReader read = cmd.ExecuteReader();
                    try
                    {
                        while (read.Read())
                        {
                            DateTime time = Convert.ToDateTime(read["DataMasurare"].ToString());
                            if (dateTimePicker1.Value.Day == time.Day && dateTimePicker1.Value.Month == time.Month && dateTimePicker1.Value.Year == time.Year)
                            {
                                int dx = Convert.ToInt32(read["PozitieX"].ToString());
                                int dy = Convert.ToInt32(read["PozitieY"].ToString());

                                if (comboBox2.SelectedIndex == 1 && Convert.ToInt32(read["ValoareMasurare"].ToString()) < 20)
                                {
                                    if (x >= dx && x <= dx + 20)
                                    {
                                        if (y >= dy && y <= dy + 20)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else if (comboBox2.SelectedIndex == 2 && Convert.ToInt32(read["ValoareMasurare"].ToString()) >= 20 && Convert.ToInt32(read["ValoareMasurare"].ToString()) <= 40)
                                {
                                    if (x >= dx && x <= dx + 20)
                                    {
                                        if (y >= dy && y <= dy + 20)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else if (comboBox2.SelectedIndex == 3 && Convert.ToInt32(read["ValoareMasurare"].ToString()) > 40)
                                {
                                    if (x >= dx && x <= dx + 20)
                                    {
                                        if (y >= dy && y <= dy + 20)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else if (comboBox2.SelectedIndex == 0)
                                {
                                    if (x >= dx && x <= dx + 20)
                                    {
                                        if (y >= dy && y <= dy + 20)
                                        {
                                            return false;
                                        }
                                    }
                                }


                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    conn.Close();
                }
                return true;
            }
            return false;
        }

        private void vizualizare_Load(object sender, EventArgs e)
        {
            label2.Text = "Utilizator: " + autentificare.username;
            panel2.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Background/back12.jpg");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/default_harta.png");
            pictureBox1.ErrorImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/default_harta.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.BackgroundImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/default_harta.png");
            pictureBox2.ErrorImage = Image.FromFile(Application.StartupPath + @"/OJTI_2022_C#_Resurse/Harti/default_harta.png");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            comboBox2.SelectedIndex = 0;

            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Harti]", conn);
                SqlDataReader read = cmd.ExecuteReader();
                try
                {
                    while (read.Read())
                    {
                        comboBox1.Items.Add(read["NumeHarta"].ToString());
                    }
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_map();
            load_db();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            load_db();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            load_db();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_db();
        }

        public static string idharta, coord_dx, coord_dy, data;

        bool is_point(Point a, Point b)
        {
            if(a.X >= b.X && a.X <= b.X + 20){
                if (a.Y >= b.Y && a.Y <= b.Y + 20)
                    return true;
                return false;
            }
            return false;
        }

        void lines_paint(int x, int y)
        {
            pictureBox2.BackgroundImage = pictureBox1.BackgroundImage;
            int[] val = new int[1000];
            Point[] pozi = new Point[1000];
            int k = 0;
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [Masurare] where IdHarta = @id", conn);
                if (comboBox1.SelectedIndex != null)
                    cmd.Parameters.Add("@id", Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1);
                SqlDataReader read = cmd.ExecuteReader();
                try
                {
                    while (read.Read())
                    {
                        DateTime time = Convert.ToDateTime(read["DataMasurare"].ToString());
                        if (dateTimePicker1.Value.Day == time.Day && dateTimePicker1.Value.Month == time.Month && dateTimePicker1.Value.Year == time.Year)
                        {
                            int dx = Convert.ToInt32(read["PozitieX"].ToString());
                            int dy = Convert.ToInt32(read["PozitieY"].ToString());

                            if (comboBox2.SelectedIndex == 1 && Convert.ToInt32(read["ValoareMasurare"].ToString()) < 20)
                            {
                                val[k] = Convert.ToInt32(read["ValoareMasurare"].ToString());
                                pozi[k].X = dx;
                                pozi[k++].Y = dy;
                            }
                            else if (comboBox2.SelectedIndex == 2 && Convert.ToInt32(read["ValoareMasurare"].ToString()) >= 20 && Convert.ToInt32(read["ValoareMasurare"].ToString()) <= 40)
                            {
                                val[k] = Convert.ToInt32(read["ValoareMasurare"].ToString());
                                pozi[k].X = dx;
                                pozi[k++].Y = dy;
                            }
                            else if (comboBox2.SelectedIndex == 3 && Convert.ToInt32(read["ValoareMasurare"].ToString()) > 40)
                            {
                                val[k] = Convert.ToInt32(read["ValoareMasurare"].ToString());
                                pozi[k].X = dx;
                                pozi[k++].Y = dy;
                            }
                            else if (comboBox2.SelectedIndex == 0)
                            {
                                val[k] = Convert.ToInt32(read["ValoareMasurare"].ToString());
                                pozi[k].X = dx;
                                pozi[k++].Y = dy;
                            }


                        }

                    }
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            }

            int maxi = 0, p = 0;

            for(int i = 0; i < k; i++)
            {
                if(val[i] > maxi && is_point(new Point(x, y), pozi[i]) == false)
                {
                    maxi = val[i];
                    p = i;
                }
            }
            make_line(new Point(x, y), pozi[p]);
            val[p] = -1;

            maxi = 0;  p = 0;
            for (int i = 0; i < k; i++)
            {
                if (val[i] > maxi && is_point(new Point(x, y), pozi[i]) == false)
                {
                    maxi = val[i];
                    p = i;
                }
            }
            make_line(new Point(x, y), pozi[p]);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            int dx = pictureBox1.PointToClient(Cursor.Position).X;
            int dy = pictureBox1.PointToClient(Cursor.Position).Y;

            if (path == "1" && cauta_poz(dx, dy) == false)
            {
                lines_paint(dx, dy);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int dx = pictureBox1.PointToClient(Cursor.Position).X;
            int dy = pictureBox1.PointToClient(Cursor.Position).Y;

            if (path == "1")
            {
                if (cauta_poz(dx, dy) == false)
                {
                    MessageBox.Show("Pozitie deja existenta", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    idharta = (comboBox1.SelectedIndex + 1).ToString();
                    coord_dx = dx.ToString();
                    coord_dy = dy.ToString();
                    data = dateTimePicker1.Value.ToString();

                    var frm = new AdaugareMasurare();
                    frm.ShowDialog();
                    load_db();
                }
            }
        }
    }
}
