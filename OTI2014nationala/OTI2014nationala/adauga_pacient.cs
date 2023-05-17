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

namespace OTI2014nationala
{
    public partial class adauga_pacient : Form
    {
        public adauga_pacient()
        {
            InitializeComponent();
        }

        private void adauga_pacient_Load(object sender, EventArgs e)
        {
            int k = 1;
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale", conn);
                SqlDataReader read = cmd.ExecuteReader();
                
                while (read.Read())
                    k++;

                conn.Close();
            }
            textBox1.Text = k.ToString();
            textBox4.Text = "0";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = ((int)((DateTime.Now - dateTimePicker1.Value).TotalDays / 365)).ToString();
        }

        bool check_trim()
        {
            if (textBox2.Text.Trim() == "")
                return false;
            if (textBox3.Text.Trim() == "")
                return false;
            if (textBox5.Text.Trim() == "")
                return false;
            return true;
        }

        bool check_email()
        {
            int poz = 0;
            for(int i = 0; i < textBox5.Text.Length; i++)
            {
                if(textBox5.Text[i].ToString() == "@")
                {
                    poz = i;
                }
            }
            if (poz + 5 <= textBox5.Text.Length)
                return true;
            
            return false;
        }

        bool check_db()
        {
            using (SqlConnection conn = new SqlConnection(medic.db))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from DatePersonale where Email = @email", conn);
                cmd.Parameters.Add("@email", textBox5.Text);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    conn.Close();
                    return false;
                }
                conn.Close();

            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check_trim())
            {
                if (check_email())
                {
                    if (check_db())
                    {
                        using (SqlConnection conn = new SqlConnection(medic.db))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("insert into DatePersonale values (@nume, @prenume, @gen, @varsta, @data, @email)", conn);
                            cmd.Parameters.Add(@"nume", textBox2.Text);
                            cmd.Parameters.Add(@"prenume", textBox3.Text);
                            if (radioButton1.Checked == true)
                                cmd.Parameters.Add(@"gen", "M");
                            else
                                cmd.Parameters.Add(@"gen", "F");
                            cmd.Parameters.Add(@"varsta", textBox4.Text);
                            cmd.Parameters.Add(@"data", dateTimePicker1.Value);
                            cmd.Parameters.Add(@"email", textBox5.Text);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Pacient adaugat!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            conn.Close();
                            var frm = new medic();
                            frm.Show();
                            this.Hide();
                        }
                    }
                    else
                        MessageBox.Show("Email deja in baza de date!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                    MessageBox.Show("Email invalid!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
                MessageBox.Show("Completati toate casetele", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            
        }
    }
}
