using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace QFinder.Helpers
{
    public class ApplicationController : WindowsFormsApplicationBase
    {
        private Form mainForm;
        public ApplicationController(Form form)
        {
            mainForm = form;
            this.IsSingleInstance = true;
            this.StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            e.BringToForeground = true;
            mainForm.ShowInTaskbar = true;
            mainForm.WindowState = FormWindowState.Minimized;
            mainForm.Show();
            mainForm.WindowState = FormWindowState.Normal;
        }

        protected override void OnCreateMainForm()
        {
            this.MainForm = mainForm;
        }
    }
}
