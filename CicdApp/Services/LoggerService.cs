using CicdApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicdApp.Services
{
    public class LoggerService
    {
        public string _logFilePath;

        public LoggerService(string torgetDir)
        {
            string fileName = GenerateLogFileName(torgetDir);
            string logFilePath = System.IO.Path.Combine(torgetDir, fileName);
            _logFilePath = logFilePath;
        }

        public string GenerateLogFileName(string torgetDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(torgetDir);
            string dirName = dirInfo.Name;
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            string fileName = $"CICD_{dirName}_{timestamp}.log";
            return fileName;
        }

        public void Log(string message, LogLevel level)
        {
            string DataTime = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]";
            string levelPart = $"[{level.ToString()}]";
            string fullMessage = $"{DataTime} {levelPart} {message}{Environment.NewLine}";
            System.IO.File.AppendAllText(_logFilePath, fullMessage);
        }
    }
}
