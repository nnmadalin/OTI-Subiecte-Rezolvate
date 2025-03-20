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

namespace OTI2023judet_2025
{
    public partial class Intrebare : Form
    {
        public Intrebare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        string raspCorect;
        int point = 0;

        private void Intrebare_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(initial.dbConnection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Itemi] ORDER BY NEWID()", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBox1.Text = reader[1].ToString();
            radioButton1.Text = reader[2].ToString();
            radioButton2.Text = reader[3].ToString();
            radioButton3.Text = reader[4].ToString();

            raspCorect = reader[5].ToString();
            point = Convert.ToInt32(reader[6]);

            conn.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if(raspCorect == "1" && radioButton1.Checked == true)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SarpeEducativ.pct += point;

            }
            else if (raspCorect == "2" && radioButton2.Checked == true)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SarpeEducativ.pct += point;

            }
            else if (raspCorect == "3" && radioButton3.Checked == true)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SarpeEducativ.pct += point;
            }
            else
            {
                MessageBox.Show("Răspunsul tău este greșit! Răspunsul corect este " + raspCorect, "Informare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
    }
}
