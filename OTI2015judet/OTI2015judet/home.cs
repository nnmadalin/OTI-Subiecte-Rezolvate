using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2015judet
{
    public partial class home : Form
    {
        private int childFormNumber = 0;

        public home()
        {
            InitializeComponent();
        }

        public static string db = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30";

        private void home_Load(object sender, EventArgs e)
        {
            if(auth.normal == false)
            {
                administrareToolStripMenuItem.Enabled = false;
            }
            else
            {
                turistiToolStripMenuItem.Enabled = false;
            }
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void turistiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            q = 3;
        }

        private void administrareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            q = 1;
        }
        public static int q = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(q == 1)
            {
                var us = new admin();
                us.MdiParent = this;
                us.Show();
            }
            else if(q == 2)
            {
                var us = new list_croaziera();
                us.MdiParent = this;
                us.Show();
                
            }
            else if (q == 3)
            {
                var us = new normal();
                us.MdiParent = this;
                us.Show();

            }
            else if (q == 4)
            {
                var us = new show_map();
                us.MdiParent = this;
                us.Show();

            }
            q = 0;
        }
    }
}
