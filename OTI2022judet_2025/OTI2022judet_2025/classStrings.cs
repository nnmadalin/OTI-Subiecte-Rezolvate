using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTI2022judet_2025
{
    internal class classStrings
    {
        public static string dbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Poluare.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=true";

        public static string pathEXE = Application.StartupPath + "/OJTI_2022_C#_Resurse";

        public static string idUser, nameUser, emailUser;
    }
}
