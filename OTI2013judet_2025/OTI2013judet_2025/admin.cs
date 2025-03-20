using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OTI2013judet_2025
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point _mouse;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.Location.X - _mouse.X, this.Location.Y + e.Location.Y - _mouse.Y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
        }

        private void admin_Load(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(Application.StartupPath + "/Resurse/Data/Clasament.txt");
            string lineReader;
            while((lineReader = reader.ReadLine()) != null)
            {
                string[] splitReader = lineReader.Split(' ');


                dataGridView1.Rows.Add(splitReader[0], splitReader[1], splitReader[2]);
            }
            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(Application.StartupPath + "/Resurse/Data/Clasament.txt");
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                writer.WriteLine(dataGridView1.Rows[i].Cells[0].Value + " " + dataGridView1.Rows[i].Cells[1].Value + " " + dataGridView1.Rows[i].Cells[2].Value);
            }
            writer.Flush();

            writer.Close();

            MessageBox.Show("Salvat cu succes!");
        }
    }
}
