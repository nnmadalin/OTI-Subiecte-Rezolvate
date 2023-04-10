using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cia2009judet
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new calculator();
            form.ShowDialog();
        }

        private void rotireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new rotire();
            form.ShowDialog();
        }

        private void bDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new bd();
            form.ShowDialog();
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new despre();
            form.ShowDialog();
        }
    }
}
