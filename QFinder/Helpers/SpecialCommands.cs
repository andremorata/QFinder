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
                    str.AppendLine("\r\nHelp: This dialog;");
                    str.AppendLine("\r\nExit: Terminates the app instance;");
                    str.AppendLine("\r\nEditIndexPaths or EID: Shows the index paths manager interface;");
                    str.AppendLine("\r\nEditIndexSchedule or EIS: Shows the automatic index scheduler interface;");
                    str.AppendLine("\r\nBuildIndex: Gets confirmation to start mapping or re-mapping files;");
                    str.AppendLine("\r\n\r\n");
                    str.AppendLine("Query Options:");
                    str.AppendLine("\r\nTo query files by its name, simply type few pieces of the file name between spaces. Ex: grandma's recipe;");
                    str.AppendLine("\r\nTo query files of a specific type, use the extensions that you want to filter followed by an '>' and then the terms to filter the file name. EX: 'docx>grandma's recipe';");
                    str.AppendLine("\r\nTo query files stored in a specific folder or path, type part of the path followed by '/' and then type the pieces of the file that you want to find. EX: recipes/grandma's cheesecake");
                    str.AppendLine("\r\nYou can also mix the query filters like 'docx>recipes/grandma's cheesecake';");
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
                case "buildindex":
                    if (MessageBox.Show("Are you sure that you want to map/re-map all files? This might take a while to complete.",
                        "Rebuild Index", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) App.Idx.BuildIndex();
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
