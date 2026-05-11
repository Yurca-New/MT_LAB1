using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CicdApp.Models;
using CicdApp.Data.UnitOfWork;
using CicdApp.Data.Entities;

namespace CicdApp.Services
{
    public class PipelineEngine
    {
        private readonly LoggerService logger;
        private readonly IUnitOfWork unitOfWork;

        public PipelineEngine(LoggerService logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task RunAsync(PipelineConfig config, string targetDir)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var buildStage = new BuildStage
            {
                ProjectName = Path.GetFileName(targetDir),
                FolderPath = targetDir,
                WhenStarted = DateTime.UtcNow,
                StageName = "full_pipeline",
                DurationMs = 0,
                IsSuccess = true,
                TotalErrors = 0,
                TotalWarnings = 0,
            };

            await this.unitOfWork.BuildStages.AddAsync(buildStage).ConfigureAwait(false);
            await this.unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var stopwatch = Stopwatch.StartNew();

            foreach (var step in config.Pipeline)
            {
                this.logger.Log(targetDir + " - Starting step: " + step.Name, LogLevel.INFO);

                var result = await CommandRunnerService.RunCommandAsync(step.Command, step.Args, targetDir).ConfigureAwait(false);
                var fullOutput = result.Output + result.Error;
                var (errorCount, warningCount, issues) = OutputParserService.Parse(fullOutput);

                buildStage.TotalErrors += errorCount;
                buildStage.TotalWarnings += warningCount;

                foreach (var issue in issues)
                {
                    var issueLog = new IssueLog
                    {
                        BuildStageId = buildStage.Id,
                        LogLevel = issue.IsError ? "Error" : "Warning",
                        ErrorCode = issue.Code,
                        Message = issue.Message.Length > 4000 ? issue.Message.Substring(0, 4000) : issue.Message,
                        Timestamp = DateTime.UtcNow,
                    };
                    await this.unitOfWork.IssueLogs.AddAsync(issueLog).ConfigureAwait(false);
                }

                if (result.ExitCode == 0)
                {
                    this.logger.Log(targetDir + " - Step succeeded: " + step.Name, LogLevel.SUCCESS);
                }
                else
                {
                    this.logger.Log(targetDir + " - Step failed: " + step.Name, LogLevel.ERROR);
                    buildStage.IsSuccess = false;
                    if (step.StopOnFailure)
                    {
                        this.logger.Log(targetDir + " - Stopping pipeline due to failure in step: " + step.Name, LogLevel.ERROR);
                        break;
                    }
                }
            }

            stopwatch.Stop();
            buildStage.DurationMs = stopwatch.ElapsedMilliseconds;

            await this.unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            this.logger.Log(targetDir + " - Pipeline finished. Saved to database.", LogLevel.INFO);
        }
    }
}