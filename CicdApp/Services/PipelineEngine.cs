using System;
using System.Collections.Generic;
using System.Text;
using CicdApp.Models;

namespace CicdApp.Services
{
    public class PipelineEngine
    {
        private CommandRunnerService _commandRunner;
        private LoggerService _logger;
        public PipelineEngine(CommandRunnerService commandRunner, LoggerService logger)
        {
            _commandRunner = commandRunner;
            _logger = logger;
        }
        public void Run(PipelineConfig config, string targetDir)
        {
            foreach (PipelineStep step in config.pipeline)
            {
                _logger.Log(targetDir + " - Starting step: " + step.name, LogLevel.INFO);
                int result = _commandRunner.RunCommand(step.command, step.args, targetDir);
                if (result == 0)
                {
                    _logger.Log(targetDir + " - Step succeeded: " + step.name, LogLevel.SUCCESS);
                }
                else
                {
                    _logger.Log(targetDir + " - Step failed: " + step.name, LogLevel.ERROR);
                    if (step.stopOnFailure)
                    {
                        _logger.Log(targetDir + " - Stopping pipeline due to failure in step: " + step.name, LogLevel.ERROR);
                        break;
                    }
                }
            }
        }
    }
}