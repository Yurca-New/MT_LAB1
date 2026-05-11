using CicdApp.Data.Data;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Repositories
{
    public class ThreadSpeedMetricRepository : Repository<ThreadSpeedMetric>, IThreadSpeedMetricRepository
    {
        public ThreadSpeedMetricRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}