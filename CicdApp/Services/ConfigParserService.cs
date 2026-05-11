using System.IO;
using System.Text.Json;
using CicdApp.Models;

namespace CicdApp.Services
{
    public class ConfigParserService
    {
        public PipelineConfig ParseConfig(string configPath)
        {
            string jsonString = File.ReadAllText(configPath);
            var result = JsonSerializer.Deserialize<PipelineConfig>(jsonString);
            return result ?? throw new InvalidDataException("Failed to deserialize config");
        }
    }
}