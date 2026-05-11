using System.Collections.Generic;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Seed
{
    public interface ITestDataFactory
    {
        List<BuildStage> CreateBuildStages();
        List<IssueLog> CreateIssueLogsForBuildStage(int buildStageId, bool hasErrors);
        List<ThreadSpeedMetric> CreateThreadSpeedMetrics();
    }
}