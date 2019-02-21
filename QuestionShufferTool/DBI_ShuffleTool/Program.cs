using System;
using System.Windows.Forms;
using DBI_ShuffleTool.UI;

namespace DBI_ShuffleTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShuffleTool());
        }
    }
}
