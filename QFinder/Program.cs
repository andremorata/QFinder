using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QFinder
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //create a controller and Pass an instance of your application main form
            var controller = new QFinder.ApplicationController(new frmFind());

            //Run application
            controller.Run(Environment.GetCommandLineArgs());
        }
    }
}
