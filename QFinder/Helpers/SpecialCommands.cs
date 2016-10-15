using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QFinder.Helpers
{
    class SpecialCommands
    {
        public static bool RunStaticCommand(string cmd)
        {
            switch (cmd.Trim().ToLower())
            {
                case "help":
                    StringBuilder str = new StringBuilder();
                    str.AppendLine("Commands:");
                    str.AppendLine("Help: This dialog;");
                    str.AppendLine("Exit: Terminates the app instance;");
                    str.AppendLine("EditIndexPaths or EID: Shows the index paths manager interface;");
                    str.AppendLine("");
                    str.AppendLine("Query Options:");
                    str.AppendLine("To query files by its name, simply type few pieces of the file name between spaces. Ex: grandma's recipe;");
                    str.AppendLine("To query files of a specific type, use the extensions that you want to filter followed by an '>' and then the terms to filter the file name. EX: 'docx>grandma's recipe';");
                    str.AppendLine("To query files stored in a specific folder or path, type part of the path followed by '/' and then type the pieces of the file that you want to find. EX: recipes/grandma's cheesecake");
                    str.AppendLine("You can also mix the query filters like 'docx>recipes/grandma's cheesecake';");
                    MessageBox.Show(str.ToString(), "QFinder - Help");
                    return true;
                case "exit":
                    Application.Exit();
                    return true;
                case "editindexpaths":
                case "eid":
                    frmManageIndexFolders f = new frmManageIndexFolders();
                    f.ShowDialog(); return true;
                case "editindexschedule":
                case "eis":
                    frmIndexSchedule s = new frmIndexSchedule();
                    s.ShowDialog(); return true;
                default:
                    break;
            }
            return false;
        }
    }
}
