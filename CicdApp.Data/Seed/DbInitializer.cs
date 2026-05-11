using System.Linq;
using System.Threading.Tasks;
using CicdApp.Data.UnitOfWork;

namespace CicdApp.Data.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IUnitOfWork uow, ITestDataFactory factory)
        {
            var existingStages = await uow.BuildStages.GetAllAsync().ConfigureAwait(false);
            if (existingStages.Count > 0)
            {
                return;
            }

            var buildStages = factory.CreateBuildStages();
            foreach (var stage in buildStages)
            {
                await uow.BuildStages.AddAsync(stage).ConfigureAwait(false);
            }

            await uow.SaveChangesAsync().ConfigureAwait(false);

            var allStages = await uow.BuildStages.GetAllAsync().ConfigureAwait(false);
            foreach (var stage in allStages)
            {
                var hasErrors = !stage.IsSuccess;
                var issues = factory.CreateIssueLogsForBuildStage(stage.Id, hasErrors);
                foreach (var issue in issues)
                {
                    await uow.IssueLogs.AddAsync(issue).ConfigureAwait(false);
                }
            }

            var metrics = factory.CreateThreadSpeedMetrics();
            foreach (var metric in metrics)
            {
                await uow.ThreadSpeedMetrics.AddAsync(metric).ConfigureAwait(false);
            }

            await uow.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}