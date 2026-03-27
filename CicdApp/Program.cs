using CicdApp.Models;
using CicdApp.Services;
ConfigParserService parser = new ConfigParserService();
PipelineConfig config = parser.ParseConfig("C:\\Users\\Yurca\\Downloads\\MT_LAB1\\MT_LAB1\\CicdApp\\config.json");
config.DisplayInfo();