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

namespace OTI2019judet_2025
{
    public partial class MeniuFreeBook : Form
    {
        public MeniuFreeBook()
        {
            InitializeComponent();
        }

        void loadCartiInGrid()
        {
            int k = 0;
            dataGridView1.Rows.Clear();

            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [carti]", conn);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while(sqlDataReader.Read())
            {
                bool ok = true;

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM [imprumut] WHERE id_carte = @id", conn);
                cmd2.Parameters.AddWithValue("@id", sqlDataReader[0]);

                SqlDataReader sqlDataReader2 = cmd2.ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    if (Convert.ToDateTime(sqlDataReader2[3]).AddDays(30) >= DateTime.Now)
                    {
                        ok = false;
                    }
                }

                if(ok == true)
                {
                    carti[k++] = sqlDataReader[0].ToString();
                    dataGridView1.Rows.Add(sqlDataReader[1].ToString(), sqlDataReader[2].ToString(), sqlDataReader[3].ToString(), null);
                }
            }

            conn.Close();
        }

        int countBooksAvailable = 0;

        void loadCartiMEInGrid()
        {
            countBooksAvailable = 0;
            int k = 0;
            dataGridView2.Rows.Clear();

            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [carti]", conn);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                bool ok = true;

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM [imprumut] WHERE id_carte = @id and email = @email", conn);
                cmd2.Parameters.AddWithValue("@id", sqlDataReader[0]);
                cmd2.Parameters.AddWithValue("@email", classStrings.emailUser);

                SqlDataReader sqlDataReader2 = cmd2.ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    dataGridView2.Rows.Add(k + 1, sqlDataReader[1].ToString(), sqlDataReader[2].ToString(), Convert.ToDateTime(sqlDataReader2[3]), Convert.ToDateTime(sqlDataReader2[3]).AddDays(30));
                    if (Convert.ToDateTime(sqlDataReader2[3]).AddDays(30) >= DateTime.Now)
                    {
                        countBooksAvailable++;
                        dataGridView2.Rows[k].DefaultCellStyle.BackColor = Color.Green;
                    }
                    else
                    {
                        dataGridView2.Rows[k].DefaultCellStyle.BackColor = Color.Red;

                    }

                    k++;
                }
            }

            progressBar1.Value = countBooksAvailable;
            label2.Text = "Disponibilitate imprumutiri: " + countBooksAvailable.ToString() + "/3";

            conn.Close();
        }

        string[] carti = new string[1000];

        private void MeniuFreeBook_Load(object sender, EventArgs e)
        {
            loadCartiInGrid();
            loadCartiMEInGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && countBooksAvailable < 3)
            {
                SqlConnection conn = new SqlConnection(classStrings.dbString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [imprumut] VALUES (@id_carte, @email, @data)", conn);
                cmd.Parameters.AddWithValue("@id_carte", carti[e.RowIndex]); 
                cmd.Parameters.AddWithValue("@email", classStrings.emailUser); 
                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Ai rezervat cartea!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadCartiInGrid();
                loadCartiMEInGrid();

                conn.Close();
            }
            else if (e.ColumnIndex == 3 && countBooksAvailable >= 3)
            {
                MessageBox.Show("Ai atins nr maxim de carti!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            classStrings.bookSelected = "-1";
            SqlConnection conn = new SqlConnection(classStrings.dbString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [carti]", conn);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while(sqlDataReader.Read())
            {
                if (sqlDataReader[1].ToString() == dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString())
                {
                    classStrings.bookSelected = sqlDataReader[0].ToString();
                    break;
                }
            }

            conn.Close();

            if(classStrings.bookSelected != "-1")
            {
                AfiseazaCarte frm = new AfiseazaCarte();
                frm.ShowDialog();
                
            }
            
        }
    }
}
