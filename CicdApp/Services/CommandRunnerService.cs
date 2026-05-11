using System.Diagnostics;
using System.Threading.Tasks;
using CicdApp.Models;

namespace CicdApp.Services
{
    public class CommandRunnerService
    {
        public static async Task<CommandResult> RunCommandAsync(string command, string args, string workingDirectory)
        {
            var result = new CommandResult();

            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
                string error = await process.StandardError.ReadToEndAsync().ConfigureAwait(false);

                await process.WaitForExitAsync().ConfigureAwait(false);

                result.ExitCode = process.ExitCode;
                result.Output = output;
                result.Error = error;
            }

            return result;
        }
    }
}