using System;
using System.Windows.Forms;
using ToolsDev.LBTW.CardTools.Forms;

namespace ToolsDev
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
            Application.Run(new LBTWCardTools());
            //Application.Run(new AuthImportToday());
        }
    }
}
