using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_shift.Forms;
using E_shift.Models;

namespace E_shift
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 👇 Start with the LoginForm
            Application.Run(new Login());
        }
    }
}
