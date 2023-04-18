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

namespace OTI2016judet
{
    public partial class creare_cont : Form
    {
        public creare_cont()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new form();
            form.Show();
            this.Hide();
        }

        bool check_email()
        {
            string x = textBox6.Text;

            int check_ar = 0, check_dot = 0;

            for(int i = 0; i < x.Length; i++)
            {
                if (x[i] == '@' && check_ar == 0)
                    check_ar = i;

                if (x[i] == '.' && check_dot == 0)
                    check_dot = i;
            }

            if (check_dot < check_ar)
                return false;

            if (check_ar > 3 && check_ar < check_dot && check_dot + 2 < x.Length)
                return true;

            return false;
        }

        bool check_trim()
        {
            if (textBox1.Text.Trim().ToString() == "")
                return false;
            if (textBox2.Text.Trim().ToString() == "")
                return false;
            if (textBox3.Text.Trim().ToString() == "")
                return false;
            if (textBox4.Text.Trim().ToString() == "")
                return false;
            if (textBox5.Text.Trim().ToString() == "")
                return false;
            if (textBox6.Text.Trim().ToString() == "")
                return false;
            return true;
        }

        bool same_pass()
        {
            if (textBox4.Text.ToString() == textBox5.Text.ToString())
                return true;
            return false;
        }

        bool check_in_db_email()
        {
            SqlConnection conn = new SqlConnection(form.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from [Clienti] where email = @email", conn);
            cmd.Parameters.Add("@email", textBox6.Text.ToString());

            SqlDataReader read = cmd.ExecuteReader();

            if (read.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                conn.Close();
                return true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(check_trim() == true)
            {
                if(same_pass() == true)
                {
                    if (check_email() == true)
                    {
                        if(check_in_db_email() == true)
                        {
                            SqlConnection conn = new SqlConnection(form.db);
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("insert into [Clienti]  values (@parola, @nume, @prenume, @adresa, @email, @kcal)", conn);
                            cmd.Parameters.Add("@parola", textBox4.Text);
                            cmd.Parameters.Add("@nume", textBox1.Text);
                            cmd.Parameters.Add("@prenume", textBox2.Text);
                            cmd.Parameters.Add("@adresa", textBox3.Text);
                            cmd.Parameters.Add("@email", textBox6.Text);
                            cmd.Parameters.Add("@kcal", 2000);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Cont creat cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            var formular = new form();
                            formular.Show();
                            this.Hide();
                        }
                        else
                        {
                            textBox6.Text = "";
                            MessageBox.Show("Email deja in baza de date!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        textBox6.Text = "";
                        MessageBox.Show("Email scris incorect", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Parolele nu corespund", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Completati toate casetele!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
