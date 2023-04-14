using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2014judet
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static bool enable = false;
        public static int time = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            enable = true;
            time = Convert.ToInt32(numericUpDown1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            enable = false;
        }

        private void actiunileMeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new actiunile_mele();
            form.Show();
        }

        private void home_Load(object sender, EventArgs e)
        {
            var form = new actiunile_mele();
            form.Show();
            form.Hide();

            var form2 = new grafic();
            form2.Show();
            form2.Hide();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            time = Convert.ToInt32(numericUpDown1.Value);
        }

        private void graficProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new grafic();
            form.Show();
        }
    }
}
