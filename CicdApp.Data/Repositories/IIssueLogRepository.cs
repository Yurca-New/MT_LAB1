using System.Collections.Generic;
using System.Threading.Tasks;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Repositories
{
    public interface IIssueLogRepository : IRepository<IssueLog>
    {
        Task<IReadOnlyList<IssueLog>> GetByBuildStageIdAsync(int buildStageId);
    }
}