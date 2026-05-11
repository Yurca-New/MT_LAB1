using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CicdApp.Data.Data;
using CicdApp.Data.UnitOfWork;
using CicdApp.Data.Seed;
using CicdApp.Services;
using CicdApp.Models;

namespace CicdApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=cicd.db")
                .Options;

            await using var context = new AppDbContext(options);
            await using var uow = new UnitOfWork(context);

            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            var factory = new DefaultTestDataFactory();
            await DbInitializer.SeedAsync(uow, factory).ConfigureAwait(false);

            if (args.Length >= 2)
            {
                string targetDir = args[1];
                string configPath = args[0];

                var logger = new LoggerService(targetDir);
                var parser = new ConfigParserService();
                var engine = new PipelineEngine(logger, uow);

                var config = parser.ParseConfig(configPath);
                await engine.RunAsync(config, targetDir).ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("No arguments provided to run pipeline. Try: dotnet run -- config.json C:\\path");
            }

            var stages = await uow.BuildStages.GetAllAsync().ConfigureAwait(false);
            Console.WriteLine($"\n=== Build stages in database: {stages.Count} records ===");
            foreach (var s in stages)
            {
                Console.WriteLine($"{s.ProjectName} - {(s.IsSuccess ? "Success" : "Failure")} - Errors: {s.TotalErrors}, Warnings: {s.TotalWarnings}");
            }
        }
    }
}