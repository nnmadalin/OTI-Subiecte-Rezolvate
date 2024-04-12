using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace OTI2023judet
{
    public partial class Intrebare : Form
    {
        public Intrebare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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

        int corectAnswer = 0, pointAnswer = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true && corectAnswer == 1)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Sarpe.score += pointAnswer;
            }
            else if (radioButton2.Checked == true && corectAnswer == 2)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Sarpe.score += pointAnswer;
            }
            else if (radioButton3.Checked == true && corectAnswer == 3)
            {
                MessageBox.Show("Felicitări, ai răspuns corect!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Sarpe.score += pointAnswer;
            }
            else
            {
                string answerCorectfind = "";

                if (corectAnswer == 1)
                    answerCorectfind = radioButton1.Text;
                else if (corectAnswer == 2)
                    answerCorectfind = radioButton2.Text;
                else if (corectAnswer == 3)
                    answerCorectfind = radioButton3.Text;

                MessageBox.Show(" Răspunsul tău este greșit! Răspunsul corect este: " + answerCorectfind, "Informare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Intrebare_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Form1.db))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM [Itemi]", conn);
                SqlDataReader read = cmd.ExecuteReader();

                int maxQuestion = 1;

                while(read.HasRows && read.Read())
                {
                    maxQuestion++;
                }

                Random random = new Random(DateTime.Now.Second + DateTime.Now.Minute + DateTime.Now.Millisecond + DateTime.Now.Hour);

                int question = random.Next(1, maxQuestion);

                cmd = new SqlCommand("SELECT * FROM [Itemi] WHERE idItem = @id", conn);
                cmd.Parameters.AddWithValue("@id", question);
                read = cmd.ExecuteReader();

                if (read.HasRows && read.Read())
                {
                    textBox1.Text = read[1].ToString();
                    radioButton1.Text = read[2].ToString();
                    radioButton2.Text = read[3].ToString();
                    radioButton3.Text = read[4].ToString();

                    corectAnswer = Convert.ToInt32(read[5].ToString());
                    pointAnswer = Convert.ToInt32(read[6].ToString());
                }

                conn.Close();
            }
        }
    }
}
