using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.UI;
using DBI_ShuffleTool.Model;


namespace QuestionSufferTool
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
