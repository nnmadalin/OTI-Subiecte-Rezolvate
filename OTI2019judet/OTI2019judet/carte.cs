using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace OTI2019judet
{
    public partial class carte : Form
    {
        public carte()
        {
            InitializeComponent();
        }
        

        string url = Application.StartupPath + "/OJTI_2019_C#_resurse/cartipdf/" + menu.id + ".pdf";

        private void carte_Load(object sender, EventArgs e)
        {

            string encodedPath = Uri.EscapeDataString(url);

            webBrowser1.Navigate("file:///" + encodedPath);
        }
    }
}
