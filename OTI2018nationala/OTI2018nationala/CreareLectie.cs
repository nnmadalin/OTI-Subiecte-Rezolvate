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
using System.IO;

namespace OTI2018nationala
{
    public partial class CreareLectie : Form
    {
        public CreareLectie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowCount++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(tableLayoutPanel1.RowCount > 1)
                tableLayoutPanel1.RowCount--;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnCount++;            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.ColumnCount > 1)
                tableLayoutPanel1.ColumnCount--;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnCount - 1].Width += 10;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1].Height > 20)
                tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1].Height -= 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnCount - 1].Width > 20)
                tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnCount - 1].Width -= 10;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1].Height += 10;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.RemoveAt(tableLayoutPanel1.Controls.Count - 1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ///
            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(txt);
        }

        private void button11_Click(object sender, EventArgs e)
        {

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                PictureBox pct = new PictureBox();
                pct.Dock = DockStyle.Fill;
                pct.SizeMode = PictureBoxSizeMode.StretchImage;
                pct.ImageLocation = openFileDialog1.FileName;
                tableLayoutPanel1.Controls.Add(pct);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() != "")
            {
                if(textBox2.Text.Trim() != "")
                {
                    Bitmap bit = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                    tableLayoutPanel1.DrawToBitmap(bit, new Rectangle(0, 0, tableLayoutPanel1.Width, tableLayoutPanel1.Height));

                    saveFileDialog1.InitialDirectory = Application.StartupPath + "/Resurse_C#/ContinutLectii/";
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        bit.Save(saveFileDialog1.FileName);

                        using (SqlConnection conn = new SqlConnection(home.db))
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("insert into Lectii values (@id, @titlu, @reg, @data, @nume)", conn);
                            cmd.Parameters.Add("@id", autentificare.id);
                            cmd.Parameters.Add("@titlu", textBox1.Text);
                            cmd.Parameters.Add("@reg", textBox2.Text);
                            cmd.Parameters.Add("@data", DateTime.Now);
                            cmd.Parameters.Add("@nume", Path.GetFileNameWithoutExtension(saveFileDialog1.FileName));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Salvat");

                            conn.Close();
                        }
                    }
                }
                else
                    MessageBox.Show("Completati casetele!");
            }
            else
            {
                MessageBox.Show("Completati casetele!");
            }
        }
    }
}
