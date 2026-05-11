using System;
using Microsoft.EntityFrameworkCore;
using CicdApp.Data.Entities;

namespace CicdApp.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BuildStage> BuildStages { get; set; }
        public DbSet<IssueLog> IssueLogs { get; set; }
        public DbSet<ThreadSpeedMetric> ThreadSpeedMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<BuildStage>()
                .HasMany(bs => bs.IssueLogs)
                .WithOne(il => il.BuildStage)
                .HasForeignKey(il => il.BuildStageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BuildStage>().HasIndex(bs => bs.WhenStarted);
            modelBuilder.Entity<IssueLog>().HasIndex(il => il.BuildStageId);
            modelBuilder.Entity<ThreadSpeedMetric>().HasIndex(tsm => tsm.Timestamp);
        }
    }
}