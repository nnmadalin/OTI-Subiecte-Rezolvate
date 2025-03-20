using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2019judet_2025
{
    public partial class AfiseazaCarte : Form
    {
        public AfiseazaCarte()
        {
            InitializeComponent();
        }

        private void AfiseazaCarte_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(classStrings.resursePath + "/cartipdf/" + classStrings.bookSelected + ".pdf");
        }
    }
}
