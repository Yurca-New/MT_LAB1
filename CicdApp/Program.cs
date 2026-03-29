using CicdApp.Models;
using CicdApp.Services;
namespace CicdApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the configuration file.");
                return;
            }
            Console.WriteLine(args[0]);
            ConfigParserService parser = new ConfigParserService();
            PipelineConfig config = parser.ParseConfig(args[0]);
            config.DisplayInfo();

            LoggerService logger = new LoggerService(args[1]);
            Console.WriteLine(logger._logFilePath);
            logger.Log("Pipeline execution started.", LogLevel.INFO);
        }
    }
}