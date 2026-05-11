using System;
using System.Collections.Generic;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Seed
{
    public class DefaultTestDataFactory : ITestDataFactory
    {
        public List<BuildStage> CreateBuildStages()
        {
            return new List<BuildStage>
            {
                new BuildStage
                {
                    ProjectName = "TestProjectA",
                    FolderPath = "C:\\projects\\A",
                    IsSuccess = true,
                    WhenStarted = DateTime.UtcNow.AddDays(-2),
                    StageName = "build",
                    DurationMs = 1250,
                    TotalErrors = 0,
                    TotalWarnings = 2,
                },
                new BuildStage
                {
                    ProjectName = "TestProjectB",
                    FolderPath = "C:\\projects\\B",
                    IsSuccess = false,
                    WhenStarted = DateTime.UtcNow.AddDays(-1),
                    StageName = "test",
                    DurationMs = 3400,
                    TotalErrors = 3,
                    TotalWarnings = 1,
                },
            };
        }

        public List<IssueLog> CreateIssueLogsForBuildStage(int buildStageId, bool hasErrors)
        {
            var issues = new List<IssueLog>();

            if (hasErrors)
            {
                issues.Add(new IssueLog
                {
                    BuildStageId = buildStageId,
                    LogLevel = "Error",
                    ErrorCode = "CS1002",
                    Message = "Type or namespace definition, or end-of-file expected",
                    Timestamp = DateTime.UtcNow.AddMinutes(-5),
                });
                issues.Add(new IssueLog
                {
                    BuildStageId = buildStageId,
                    LogLevel = "Error",
                    ErrorCode = "CS0246",
                    Message = "The type or namespace name 'ArrayList' could not be found",
                    Timestamp = DateTime.UtcNow.AddMinutes(-4),
                });
            }

            issues.Add(new IssueLog
            {
                BuildStageId = buildStageId,
                LogLevel = "Warning",
                ErrorCode = "CS0168",
                Message = "The variable 'ex' is declared but never used",
                Timestamp = DateTime.UtcNow.AddMinutes(-3),
            });

            return issues;
        }

        public List<ThreadSpeedMetric> CreateThreadSpeedMetrics()
        {
            return new List<ThreadSpeedMetric>
            {
                new ThreadSpeedMetric
                {
                    TestDescription = "Matrix Multiplication 2000x2000",
                    LogicalCores = 8,
                    TimeSingleThreadMs = 4500,
                    TimeParallelMs = 1200,
                    Efficiency = 3.75,
                    Timestamp = DateTime.UtcNow,
                    CpuModel = "Intel Core i7-10750H",
                    CpuCores = 6,
                    CpuThreads = 12,
                    RamGb = 16,
                    OperatingSystem = "Windows 11 Pro",
                },
                new ThreadSpeedMetric
                {
                    TestDescription = "Web API requests (1000 parallel)",
                    LogicalCores = 8,
                    TimeSingleThreadMs = 3200,
                    TimeParallelMs = 980,
                    Efficiency = 3.27,
                    Timestamp = DateTime.UtcNow,
                    CpuModel = "Intel Core i7-10750H",
                    CpuCores = 6,
                    CpuThreads = 12,
                    RamGb = 16,
                    OperatingSystem = "Windows 11 Pro",
                },
            };
        }
    }
}