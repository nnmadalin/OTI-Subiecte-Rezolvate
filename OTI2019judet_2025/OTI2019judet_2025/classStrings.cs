using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2019judet_2025
{
    internal class classStrings
    {
        public static string dbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FreeBook.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets = true";

        public static string resursePath = Application.StartupPath + "/OJTI_2019_C#_resurse";

        public static string idUser, numeUser, emailUser, bookSelected;

    }
}
