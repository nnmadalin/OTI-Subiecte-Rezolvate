using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2022judet_2025
{
    public partial class AdaugaMasurare : Form
    {
        public AdaugaMasurare()
        {
            InitializeComponent();
        }

        private void AdaugaMasurare_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vizualizare.valueAddPoint = Convert.ToInt32(numericUpDown1.Value);
            MessageBox.Show("Valoare Adaugata cu succes!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}
