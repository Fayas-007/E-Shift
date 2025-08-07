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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //// Create the User object with given details
            //var user = new User
            //{
            //    UserID = 2,
            //    Username = "manager1",
            //    Password = "hmSFeWz6jXwM9xEWQCBbgwdkM1R1d1EdgfgDCumezqU=",
            //    Role = "manager",
            //};

            //// Pass the User object to EmployeeDashboard
            //Application.Run(new EmployeeDashboard(user));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 👇 Start with the LoginForm
            Application.Run(new Login());
        }
    }
}
