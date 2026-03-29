using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CicdApp.Services
{
    public class CommandRunnerService
    {
        public int RunCommand(string command, string arg, string trgetDir)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = command;
            processStartInfo.Arguments = arg;
            processStartInfo.WorkingDirectory = trgetDir;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
                return process.ExitCode;
            }
        }
    }
}
