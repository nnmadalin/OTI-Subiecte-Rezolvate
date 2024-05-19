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
using System.Data.SqlTypes;
using System.Data.Sql;
using MessagingToolkit.QRCode.Codec.Data;

namespace OTI2023nationala
{
    public partial class prim : Form
    {
        public prim()
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

        Point mouse;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - mouse.X, this.Location.Y + e.Location.Y - mouse.Y);
            }
        }

        private void prim_Load(object sender, EventArgs e)
        {

            
        }

        bool isPrim(int n)
        {
            if (n == 1 || n == 0)
                return false;
            if (n == 2)
                return true;
            if (n % 2 == 0)
                return false;
            for(int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Form1.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Rezultate]", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            string emailUser = "";
            int maxDist = 0, value = 0, vprim = 0;

            while (reader.HasRows && reader.Read())
            {
                int x = Convert.ToInt32(reader[3]);
                int init = x;
                int maxim = 0;
                while (true)
                {
                    if(isPrim(x) == true)
                    {
                        maxim = x - init;

                        break;
                    }
                    x++;
                }

                if(maxim > maxDist)
                {
                    value = x;
                    vprim = init;
                    maxDist = maxim;
                    emailUser = reader[2].ToString();
                }
            }


            string txtEncode = emailUser + " | " + vprim.ToString() + " | " + value.ToString();

            MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            encoder.QRCodeScale = 8;
            Bitmap bmp = encoder.Encode(txtEncode);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(Image.FromFile(Application.StartupPath + "/ONTI_2023_C#_Resurse/Prim/Logo_C#.png"), new Rectangle(bmp.Width / 2 - 25, bmp.Height / 2 - 25, 50, 50));

            pictureBox1.Image = bmp;

            conn.Close();
        }
    }
}
