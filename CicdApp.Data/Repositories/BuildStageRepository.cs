using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CicdApp.Data.Data;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Repositories
{
    public class BuildStageRepository : Repository<BuildStage>, IBuildStageRepository
    {
        public BuildStageRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<BuildStage>> GetByStatusAsync(bool isSuccess)
        {
            return await this.dbSet.Where(bs => bs.IsSuccess == isSuccess).ToListAsync().ConfigureAwait(false);
        }
    }
}