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
    public partial class AdaugareMasurare : Form
    {
        public AdaugareMasurare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(autentificare.db))
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into [Masurare] values (@id, @px, @py, @val, @data)", conn);
                cmd.Parameters.Add("@id", vizualizare.idharta);
                cmd.Parameters.Add("@px", vizualizare.coord_dx);
                cmd.Parameters.Add("@py", vizualizare.coord_dy);
                cmd.Parameters.Add("@val", numericUpDown1.Value);
                cmd.Parameters.Add("@data", vizualizare.data);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Adaugat cu succes!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ceva nu a mers bine!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conn.Close();
            }
        }
    }
}
