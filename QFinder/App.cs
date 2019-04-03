using QFinder.Helpers;
using System;
using System.Windows.Forms;

namespace QFinder
{
    static class App
    {

        public static Data.DB DB { get; set; }
        public static Indexing.Index Idx { get; set; }
        public static bool LogEnabled { get; set; }
        public static DateTime IndexLastRun { get; set; }
        public static DateTime AppStartedDateTime { get; set; }
        

        [STAThread]
        static void Main()
        {
            try
            {
                AppStartedDateTime = DateTime.Now;
                LogEnabled = true;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var controller = new ApplicationController(new frmFind());
                controller.Run(Environment.GetCommandLineArgs());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "QFinder - An error occurred... :-( ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
