using System;
using System.Threading.Tasks;
using CicdApp.Data.Repositories;

namespace CicdApp.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IBuildStageRepository BuildStages { get; }
        IIssueLogRepository IssueLogs { get; }
        IThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

        Task<int> SaveChangesAsync();
    }
}