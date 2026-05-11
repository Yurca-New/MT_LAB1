using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CicdApp.Services
{
    public class OutputParserService
    {
        private static readonly Regex ErrorRegex = new Regex(@"\s+error\s+([A-Za-z0-9]+):", RegexOptions.Compiled);
        private static readonly Regex WarningRegex = new Regex(@"\s+warning\s+([A-Za-z0-9]+):", RegexOptions.Compiled);
        private static readonly char[] SplitChars = new char[] { '\r', '\n' };

        public static (int ErrorCount, int WarningCount, List<(string Code, string Message, bool IsError)> Issues) Parse(string output)
        {
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            int errorCount = 0;
            int warningCount = 0;
            var issues = new List<(string Code, string Message, bool IsError)>();

            var lines = output.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (ErrorRegex.IsMatch(line))
                {
                    errorCount++;
                    var match = ErrorRegex.Match(line);
                    string code = match.Groups[1].Value;
                    issues.Add((code, line.Trim(), true));
                }
                else if (WarningRegex.IsMatch(line))
                {
                    warningCount++;
                    var match = WarningRegex.Match(line);
                    string code = match.Groups[1].Value;
                    issues.Add((code, line.Trim(), false));
                }
            }

            return (errorCount, warningCount, issues);
        }
    }
}