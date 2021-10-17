using System;
using System.IO;

namespace JerloPH_CSharp
{
    public static class Logs
    {
        private static string DIR_START = "";
        private static string FILE_LOG = "";
        private static string FILE_LOG_ERR = "";
        private static string FILE_LOG_DEBUG = "";

        public static void Initialize(string _startPath, string _mainlog, string _errlog, string _debuglog)
        {
            DIR_START = _startPath;
            FILE_LOG = _mainlog;
            FILE_LOG_ERR = _errlog;
            FILE_LOG_DEBUG = _debuglog;
        }

        public static void LogString(string file, string log)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(file))
                    file = "AppLog.log";

                string content = log;
                try
                {
                    if (!String.IsNullOrWhiteSpace(log) && !String.IsNullOrWhiteSpace(DIR_START))
                        content = log.Replace(DIR_START.Replace(@"/", @"\"), "<root>");
                }
                catch { }

                using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter s = new StreamWriter(fs))
                    {
                        s.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]: {content}");
                        s.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public static void Debug(string content)
        {
            LogString(FILE_LOG_DEBUG, content);
        }
        public static void App(string content)
        {
            LogString(FILE_LOG, content);
        }
        public static void Err(Exception ex)
        {
            if (ex != null)
                LogString(FILE_LOG_ERR, ex.ToString());
        }
        public static void Err(string log)
        {
            LogString(FILE_LOG_ERR, log);
        }
    }
}
