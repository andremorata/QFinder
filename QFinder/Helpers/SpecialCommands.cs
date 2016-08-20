using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFinder.Helpers
{
    class SpecialCommands
    {
        public static bool RunStaticCommand(string cmd)
        {
            switch (cmd.ToLower())
            {
                case "editindexpaths":
                    frmManageIndexFolders f = new frmManageIndexFolders();
                    f.ShowDialog(); return true;
                default:
                    break;
            }
            return false;
        }
    }
}
