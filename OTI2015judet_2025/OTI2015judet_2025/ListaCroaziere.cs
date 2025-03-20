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

namespace OTI2015judet_2025
{
    public partial class ListaCroaziere : Form
    {
        public ListaCroaziere()
        {
            InitializeComponent();
        }

        string porturi;
        void afisareDB()
        {
            dataGridView1.Rows.Clear();

            SqlConnection conn = new SqlConnection(Administrare.connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Croaziere WHERE Tip_Croaziera = @tip", conn);
            cmd.Parameters.Add("@tip", Convert.ToInt32(comboBox1.SelectedIndex == 0 ? 3 : (comboBox1.SelectedIndex == 1 ? 5 : 8)));


            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                string[] split = read[2].ToString().Split(',');

                porturi = "";

                for(int i = 0; i < split.Length; i++)
                {
                    porturi = porturi +  Administrare.Ports[i] + ", ";
                }

                dataGridView1.Rows.Add(read[0].ToString(), porturi, read[3].ToString(), read[4].ToString(), read[5].ToString(), read[6].ToString());
            }

            conn.Close();
        }

        private void ListaCroaziere_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            afisareDB();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            afisareDB();
        }
    }
}
