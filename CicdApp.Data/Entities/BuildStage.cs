using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CicdApp.Data.Entities
{
    [Table("BuildStages")]
    public class BuildStage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FolderPath { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }

        public DateTime WhenStarted { get; set; }

        [MaxLength(100)]
        public string StageName { get; set; } = string.Empty;

        public long DurationMs { get; set; }

        public int TotalErrors { get; set; }

        public int TotalWarnings { get; set; }

        public virtual ICollection<IssueLog> IssueLogs { get; } = new List<IssueLog>();
    }
}