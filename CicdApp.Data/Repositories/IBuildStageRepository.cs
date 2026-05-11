using System.Collections.Generic;
using System.Threading.Tasks;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Repositories
{
    public interface IBuildStageRepository : IRepository<BuildStage>
    {
        Task<IReadOnlyList<BuildStage>> GetByStatusAsync(bool isSuccess);
    }
}