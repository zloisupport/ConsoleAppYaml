using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayEuwRusClient
{
   
    public enum LogTarget
    {
        File
    }
    public abstract class LogBase
    {
       
        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        private static bool isfirstLog = true;
        public string filePath = @"Launch.log";
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath,true))
            {
              
                if (isfirstLog)
                {
                    string currentDateTime = DateTime.Now.ToString("dd/MM/yy H:m:ss");
                    streamWriter.WriteLine($"[{currentDateTime}] {message}");
                    isfirstLog = false;
                }
                streamWriter.WriteLine($"{message}");
    
                streamWriter.Close();
            }
        }
    }

    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
}
