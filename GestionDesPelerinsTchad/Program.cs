using System;
using System.Windows.Forms;
using SGSP.Formes;

namespace SGSP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (DateTime.Now.Date <= DateTime.Parse("15/08/2016"))
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            //}
        }
    }
}
