using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CicdApp.Data.Entities
{
    [Table("ThreadSpeedMetrics")]
    public class ThreadSpeedMetric
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string TestDescription { get; set; } = string.Empty;

        public int LogicalCores { get; set; }

        public long TimeSingleThreadMs { get; set; }

        public long TimeParallelMs { get; set; }

        public double Efficiency { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(100)]
        public string CpuModel { get; set; } = string.Empty;

        public int CpuCores { get; set; }

        public int CpuThreads { get; set; }

        public int RamGb { get; set; }

        [MaxLength(100)]
        public string OperatingSystem { get; set; } = string.Empty;
    }
}