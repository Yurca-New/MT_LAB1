using CicdApp.Models;
using CicdApp.Services;
namespace CicdApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetDir = args[1];
            string configPath = args[0];
            LoggerService logger = new LoggerService(targetDir);
            CommandRunnerService commandRunner = new CommandRunnerService();
            ConfigParserService parser = new ConfigParserService();
            PipelineEngine pipelineEngine = new PipelineEngine(commandRunner, logger);
            PipelineConfig config = parser.ParseConfig(configPath);

            pipelineEngine.Run(config, targetDir);
        }
    }
}