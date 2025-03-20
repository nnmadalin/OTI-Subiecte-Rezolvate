using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2022judet_2025
{
    public partial class Vizualizare : Form
    {
        public Vizualizare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
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

        Bitmap bitBackup = new Bitmap(640, 480);

        void loadMap()
        {
            if (comboBox1.Items.Count == 0 || comboBox1.SelectedIndex < 0)
                return;


            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Masurare] WHERE IdHarta = @id", conn);
            cmd.Parameters.AddWithValue("@id", comboBox1.SelectedIndex + 1);
            SqlDataReader sqldata = cmd.ExecuteReader();

            Bitmap bit = new Bitmap(640, 480);
            Graphics graphics = Graphics.FromImage(bit);
            while (sqldata.Read())
            {
                if (Convert.ToDateTime(sqldata["DataMasurare"]).Date == (dateTimePicker1.Value).Date)
                {
                    
                    if ((comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) < 20)
                    {
                        graphics.DrawEllipse(new Pen(Brushes.Green, 2), new Rectangle(Convert.ToInt32(sqldata["PozitieX"]) - 10, Convert.ToInt32(sqldata["PozitieY"]) - 10, 20, 20));
                        graphics.DrawString(sqldata["ValoareMasurare"].ToString(), new Font(FontFamily.GenericSansSerif, 12), Brushes.Green, new Point(Convert.ToInt32(sqldata["PozitieX"]) - 12, Convert.ToInt32(sqldata["PozitieY"]) - 10));
                    }
                    else if ((comboBox2.SelectedIndex == 2 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) >= 20 && Convert.ToInt32(sqldata["ValoareMasurare"]) <= 40)
                    {
                        graphics.DrawEllipse(new Pen(Brushes.Orange, 2), new Rectangle(Convert.ToInt32(sqldata["PozitieX"]) - 10, Convert.ToInt32(sqldata["PozitieY"]) - 10, 20, 20));
                        graphics.DrawString(sqldata["ValoareMasurare"].ToString(), new Font(FontFamily.GenericSansSerif, 12), Brushes.Orange, new Point(Convert.ToInt32(sqldata["PozitieX"]) - 12, Convert.ToInt32(sqldata["PozitieY"]) - 10));
                    }
                    else if ((comboBox2.SelectedIndex == 3 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) > 40)
                    {
                        graphics.DrawEllipse(new Pen(Brushes.Red, 2), new Rectangle(Convert.ToInt32(sqldata["PozitieX"]) - 10, Convert.ToInt32(sqldata["PozitieY"]) - 10, 20, 20));
                        graphics.DrawString(sqldata["ValoareMasurare"].ToString(), new Font(FontFamily.GenericSansSerif, 12), Brushes.Red, new Point(Convert.ToInt32(sqldata["PozitieX"]) - 12, Convert.ToInt32(sqldata["PozitieY"]) - 10));
                    }
                }
            }

            pictureBox1.BackgroundImage = Image.FromFile(classStrings.pathEXE + "/Harti/" + Harti[comboBox1.SelectedIndex]);
            pictureBox2.BackgroundImage = Image.FromFile(classStrings.pathEXE + "/Harti/" + Harti[comboBox1.SelectedIndex]);
            pictureBox1.Image = bit;
            pictureBox2.Image = bit;

            bitBackup = bit;

            conn.Close();
        }

        string[] Harti = new string[100];

        private void Vizualizare_Load(object sender, EventArgs e)
        {
            label3.Text = "Nume" + classStrings.nameUser;
            comboBox2.SelectedIndex = 0;


            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Harti]", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            int k = 0;

            while(reader.Read())
            {
                comboBox1.Items.Add(reader[1]);
                Harti[k++] = reader[2].ToString();
            }

            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMap();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            loadMap();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        public static int valueAddPoint = -1;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int x = pictureBox1.PointToClient(Cursor.Position).X;
            int y = pictureBox1.PointToClient(Cursor.Position).Y;
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Masurare] WHERE IdHarta = @id", conn);
            cmd.Parameters.AddWithValue("@id", comboBox1.SelectedIndex + 1);
            SqlDataReader sqldata = cmd.ExecuteReader();

            bool ok = true;

            while (sqldata.Read())
            {
                if (Convert.ToDateTime(sqldata["DataMasurare"]).Date == (dateTimePicker1.Value).Date)
                {
                    if(Convert.ToInt32(sqldata["PozitieX"]) - 10 <= x && Convert.ToInt32(sqldata["PozitieY"]) - 10 <= y &&
                        Convert.ToInt32(sqldata["PozitieX"]) + 10 >= x && Convert.ToInt32(sqldata["PozitieY"]) + 10 >= y)
                    {
                        ok = false;
                    }
                }
            }

            if(ok == true)
            {
                AdaugaMasurare frm = new AdaugaMasurare();
                frm.ShowDialog();

               if(valueAddPoint != -1)
                {
                    cmd = new SqlCommand("INSERT INTO [Masurare] VALUES (@id, @pozX, @pozY, @val, @date)", conn);
                    cmd.Parameters.AddWithValue("@id", comboBox1.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@pozX", x);
                    cmd.Parameters.AddWithValue("@pozY", y);
                    cmd.Parameters.AddWithValue("@val", valueAddPoint);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                    cmd.ExecuteNonQuery();

                    loadMap();
                    valueAddPoint = -1;
                }
            }


            conn.Close();
        }

        Point pGasit;


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            loadMap();
            int x = pictureBox2.PointToClient(Cursor.Position).X;
            int y = pictureBox2.PointToClient(Cursor.Position).Y;


            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Masurare] WHERE IdHarta = @id", conn);
            cmd.Parameters.AddWithValue("@id", comboBox1.SelectedIndex + 1);
            SqlDataReader sqldata = cmd.ExecuteReader();

            Bitmap bit = new Bitmap(640, 480);
            Graphics graphics = Graphics.FromImage(bitBackup);

            int idGasit = -1;

            while (sqldata.Read())
            {
                if (Convert.ToDateTime(sqldata["DataMasurare"]).Date == (dateTimePicker1.Value).Date)
                {
                    if (Convert.ToInt32(sqldata["PozitieX"]) - 10 <= x && Convert.ToInt32(sqldata["PozitieY"]) - 10 <= y &&
                        Convert.ToInt32(sqldata["PozitieX"]) + 10 >= x && Convert.ToInt32(sqldata["PozitieY"]) + 10 >= y)
                    {
                        idGasit = Convert.ToInt32(sqldata[0]);
                        pGasit = new Point(Convert.ToInt32(sqldata[2]), Convert.ToInt32(sqldata[3]));
                        break;
                    }
                }
            }

            if(idGasit != -1)
            {
                int k = 0;
                cmd = new SqlCommand("SELECT * FROM [Masurare] WHERE IdHarta = @id and IdMasurare != @idM ORDER BY ValoareMasurare DESC", conn);
                cmd.Parameters.AddWithValue("@id", comboBox1.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@idM", idGasit);
                sqldata = cmd.ExecuteReader();
                while (sqldata.Read())
                {
                    if (k == 2)
                        break;
                    if (Convert.ToDateTime(sqldata["DataMasurare"]).Date == (dateTimePicker1.Value).Date)
                    {
                        if ((comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) < 20)
                        {
                            graphics.DrawLine(Pens.Green, pGasit, new Point(Convert.ToInt32(sqldata["PozitieX"]), Convert.ToInt32(sqldata["PozitieY"])));

                            k++;
                        }
                        else if ((comboBox2.SelectedIndex == 2 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) >= 20 && Convert.ToInt32(sqldata["ValoareMasurare"]) <= 40)
                        {
                            graphics.DrawLine(Pens.Orange, pGasit, new Point(Convert.ToInt32(sqldata["PozitieX"]), Convert.ToInt32(sqldata["PozitieY"])));
                            k++;

                        }
                        else if ((comboBox2.SelectedIndex == 3 || comboBox2.SelectedIndex == 0) && Convert.ToInt32(sqldata["ValoareMasurare"]) > 40)
                        {
                            graphics.DrawLine(Pens.Red, pGasit, new Point(Convert.ToInt32(sqldata["PozitieX"]), Convert.ToInt32(sqldata["PozitieY"])));
                            k++;

                        }
                    }

                }
            }


            pictureBox2.Image = bitBackup;


            conn.Close();
        }
    }
}
