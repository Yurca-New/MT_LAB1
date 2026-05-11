using System;
using System.Threading.Tasks;
using CicdApp.Data.Data;
using CicdApp.Data.Repositories;

namespace CicdApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private bool disposed = false;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.BuildStages = new BuildStageRepository(this.context);
            this.IssueLogs = new IssueLogRepository(this.context);
            this.ThreadSpeedMetrics = new ThreadSpeedMetricRepository(this.context);
        }

        public IBuildStageRepository BuildStages { get; }
        public IIssueLogRepository IssueLogs { get; }
        public IThreadSpeedMetricRepository ThreadSpeedMetrics { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                this.context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore().ConfigureAwait(false);
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (this.context != null)
            {
                await this.context.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}