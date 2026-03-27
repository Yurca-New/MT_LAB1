using CicdApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CicdApp.Services
{
    public class ConfigParserService
    {
        public PipelineConfig ParseConfig(string configPath)
        {
            string jsonString = System.IO.File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<PipelineConfig>(jsonString);
        }
    }
}
