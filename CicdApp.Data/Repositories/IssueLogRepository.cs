using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CicdApp.Data.Data;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Repositories
{
    public class IssueLogRepository : Repository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<IssueLog>> GetByBuildStageIdAsync(int buildStageId)
        {
            return await this.dbSet.Where(il => il.BuildStageId == buildStageId).ToListAsync().ConfigureAwait(false);
        }
    }
}