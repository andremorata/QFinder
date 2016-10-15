using QFinder;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace QFinder.Helpers
{
    public static class Log
    {
        /// <summary>
        /// LogToEventViewer
        /// </summary>
        public static void Write(EventLogEntryType type, string sEvent)
        {
            if (App.LogEnabled)
            {
                try
                {
                    string sSource;
                    string sLog;

                    sSource = "QFinder";
                    sLog = "Application";

                    if (!EventLog.SourceExists(sSource))
                        EventLog.CreateEventSource(sSource, sLog);

                    EventLog.WriteEntry(sSource, sEvent, type, 234);
                }
                catch (Exception ex)
                {
                    LogToFile(new Exception(sEvent, ex), "LogToEventViewer");
                }
            }
        }

        public static void Write(string sEvent)
        {
            if (App.LogEnabled)
            {
                Write(EventLogEntryType.Information, sEvent);
            }
        }

        public static string ProcessException(Exception ex, string original = null)
        {
            try
            {
                var str = new StringBuilder();
                str.AppendLine("");
                str.AppendLine("======================= " + DateTime.Now + " =======================");
                str.AppendLine("Message: " + ex.Message);
                str.AppendLine("Trace: " + ex.StackTrace);

                if (ex.InnerException == null) return str.ToString();
                str.AppendLine("INNER EXCEPTION: ");
                str.Append(ProcessException(ex.InnerException, str.ToString()));
                str.AppendLine("==================================================");

                return str.ToString();
            }
            catch (Exception exc) { return "The exception could not be processed. " + exc.Message; }
        }

        public static void LogToFile(Exception exc, string source)
        {
            if (App.LogEnabled)
            {
                try
                {
                    string dir = @"C:\QFinder";
                    string logFile = dir + @"\ErrorLog.txt";

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (!File.Exists(logFile))
                        File.CreateText(logFile);

                    // Open the log file for append and write the log
                    StreamWriter sw = new StreamWriter(logFile, true);
                    sw.WriteLine("********** {0} **********", DateTime.Now);
                    if (exc.InnerException != null)
                    {
                        sw.Write("Inner Exception Type: ");
                        sw.WriteLine(exc.InnerException.GetType().ToString());
                        sw.Write("Inner Exception: ");
                        sw.WriteLine(exc.InnerException.Message);
                        sw.Write("Inner Source: ");
                        sw.WriteLine(exc.InnerException.Source);
                        if (exc.InnerException.StackTrace != null)
                        {
                            sw.WriteLine("Inner Stack Trace: ");
                            sw.WriteLine(exc.InnerException.StackTrace);
                        }
                    }
                    sw.Write("Exception Type: ");
                    sw.WriteLine(exc.GetType().ToString());
                    sw.WriteLine("Exception: " + exc.Message);
                    sw.WriteLine(Convert.ToString("Source: ") + source);
                    sw.WriteLine("Stack Trace: ");
                    if (exc.StackTrace != null)
                    {
                        sw.WriteLine(exc.StackTrace);
                        sw.WriteLine();
                    }
                    sw.Close();

                }
                catch { }
            }
        }
    }
}