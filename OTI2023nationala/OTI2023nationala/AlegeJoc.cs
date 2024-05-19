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

namespace OTI2023nationala
{
    public partial class AlegeJoc : Form
    {
        public AlegeJoc()
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

        string[] dateSort1 = new string[100];
        int[] valueSort1 = new int[100];
        string[] dateSort2 = new string[100];
        int[] valueSort2 = new int[100];

        int k1 = 0, k2 = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new JocMemorie();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new JocLitere();
            frm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new prim();
            frm.Show();
            this.Hide();
        }

        private void AlegeJoc_Load(object sender, EventArgs e)
        {
            label2.Text += Form1.nameUser + " (" + Form1.emailUser + ") !";

            SqlConnection conn = new SqlConnection(Form1.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Rezultate] WHERE EmailUtilizator=@email", conn);
            cmd.Parameters.AddWithValue("@email", Form1.emailUser);

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.HasRows && reader.Read())
            {
                if (reader[1].ToString() == "0")
                {
                    dateSort1[k1] = Convert.ToDateTime(reader[4]).ToString("dd.MM.yyyy");
                    valueSort1[k1] = Convert.ToInt32(reader[3]);

                    k1++;
                }
                else
                {
                    dateSort2[k2] = Convert.ToDateTime(reader[4]).ToString("dd.MM.yyyy");
                    valueSort2[k2] = Convert.ToInt32(reader[3]);

                    k2++;
                }
            }

            for(int i = 0; i < k1; i++)
            {
                if (dateSort1[i] != "-1")
                {
                    int maxim = valueSort1[i];
                    for (int j = i + 1; j < k1; j++)
                    {
                        if (dateSort1[i] == dateSort1[j])
                        {
                            maxim = Math.Max(maxim, valueSort1[j]);
                            dateSort1[j] = "-1";
                        }
                    }
                    chart1.Series[0].Points.AddXY(dateSort1[i], maxim);
                }
            }

            for (int i = 0; i < k2; i++)
            {
                if (dateSort2[i] != "-1")
                {
                    int maxim = valueSort2[i];
                    for (int j = i + 1; j < k2; j++)
                    {
                        if (dateSort2[i] == dateSort2[j])
                        {
                            maxim = Math.Max(maxim, valueSort2[j]);
                            dateSort2[j] = "-1";
                        }
                    }
                    chart1.Series[1].Points.AddXY(dateSort2[i], maxim);
                }
            }

            conn.Close();
        }
    }
}
