using System;
using System.Windows.Forms;

using ControllerNS;

namespace RSSI_model
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

            var controller = new Controller(10, 10);

            Application.Run(controller.View.Form);
        }
    }
}
