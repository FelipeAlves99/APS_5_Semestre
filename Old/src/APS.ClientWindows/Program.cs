using APS.ClientWindows.Views;
using System;
using System.Windows.Forms;

namespace APS.ClientWindows
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGroupChat());
        }
    }
}
