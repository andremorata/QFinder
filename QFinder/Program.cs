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

        [STAThread]
        static void Main()
        {
            try
            {
                DB = new Data.DB();
                DB.Folder = Application.StartupPath + "\\Data";
                if (!DB.Check()) DB.CreateDB();
                DB.CheckDbStructure();
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //create a controller and Pass an instance of your application main form
                var controller = new QFinder.ApplicationController(new frmFind());

                //Run indexing subsystem
                Idx = new Index.Index();
                Idx.BuildIndex();

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
