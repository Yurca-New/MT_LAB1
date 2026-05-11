using System;
using System.Globalization;
using System.IO;
using CicdApp.Models;

namespace CicdApp.Services
{
    public class LoggerService
    {
        private readonly string logFilePath;

        public LoggerService(string targetDir)
        {
            string fileName = GenerateLogFileName(targetDir);
            this.logFilePath = Path.Combine(targetDir, fileName);
        }

        private static string GenerateLogFileName(string targetDir)
        {
            var dirInfo = new DirectoryInfo(targetDir);
            string dirName = dirInfo.Name;
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
            return $"CICD_{dirName}_{timestamp}.log";
        }

        public void Log(string message, LogLevel level)
        {
            string dateTime = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}]";
            string levelPart = $"[{level.ToString()}]";
            string fullMessage = $"{dateTime} {levelPart} {message}{Environment.NewLine}";
            File.AppendAllText(this.logFilePath, fullMessage);
        }
    }
}