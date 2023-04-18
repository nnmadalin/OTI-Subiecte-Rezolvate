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
    public partial class autentificare : Form
    {
        public autentificare()
        {
            InitializeComponent();
        }

        public static string email_user = "", id_user ="";

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new form();
            form.Show();
            this.Hide();
        }

        bool check_trim()
        {
            if (textBox4.Text.Trim().ToString() == "")
                return false;
            if (textBox6.Text.Trim().ToString() == "")
                return false;
            
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(form.db);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from [Clienti] where email = @email and  parola = @pass", conn);
            cmd.Parameters.Add("@email", textBox6.Text.ToString());
            cmd.Parameters.Add("@pass", textBox4.Text.ToString());
            SqlDataReader read = cmd.ExecuteReader();

            if (read.HasRows)
            {
                read.Read();
                email_user = textBox6.Text;
                id_user = read["id_client"].ToString();
                var formular = new optiuni();
                formular.Show();
                this.Hide();
            }
            else
            {
                textBox4.Text = "";
                textBox6.Text = "";
                MessageBox.Show("Email sau parola gresita!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
