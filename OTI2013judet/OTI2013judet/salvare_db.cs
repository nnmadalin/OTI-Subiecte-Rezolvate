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

namespace OTI2013judet
{
    public partial class salvare_db : Form
    {
        public salvare_db()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Nu ai puse numele!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Clasament.mdf;Integrated Security=True;Connect Timeout=3";
                SqlConnection conn = new SqlConnection(db);
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into [Table] values (@nume, @timp, @nr)", conn);
                cmd.Parameters.Add("@nume", textBox1.Text);
                TimeSpan timeSpan = new TimeSpan(0, 0, Convert.ToInt32(joc.sec));
                cmd.Parameters.Add("@timp", timeSpan);
                cmd.Parameters.Add("@nr", joc_nou.number_img);
                var res = cmd.ExecuteNonQuery();

                Console.WriteLine(res);

                MessageBox.Show("Salvat cu succes");


                conn.Close();

                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }
    }
}
