using QFinder.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QFinder
{
    static class Program
    {

        public static Data.DB DB { get; set; }
        public static Index.Index Idx { get; set; }
        public static bool LogEnabled { get; internal set; }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //create a controller and Pass an instance of your application main form
                var controller = new ApplicationController(new frmFind());

                //Run application
                controller.Run(Environment.GetCommandLineArgs());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "QFinder - An error occurred... :-( ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
