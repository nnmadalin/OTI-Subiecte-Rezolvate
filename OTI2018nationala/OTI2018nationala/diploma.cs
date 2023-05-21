using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2018nationala
{
    public partial class diploma : Form
    {
        public diploma()
        {
            InitializeComponent();
        }

        private void diploma_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Resurse_C#/sigiliu.jpg");
            label2.Text = "Se acorda elevului " + autentificare.nume;
            if (ghiceste_regiunea.nota == 10)
                label2.Text += " premiul I!";
            else if (ghiceste_regiunea.nota == 9)
                label2.Text += " premiul II!";
            else if (ghiceste_regiunea.nota == 8)
                label2.Text += " premiul III!";
            else if (ghiceste_regiunea.nota >= 5)
                label2.Text += " mentiune!";
            else 
                label2.Text += " diploma de participare!";
        }
    }
}
