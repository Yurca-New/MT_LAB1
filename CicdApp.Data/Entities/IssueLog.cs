using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CicdApp.Data.Entities
{
    [Table("IssueLogs")]
    public class IssueLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BuildStageId { get; set; }

        [Required]
        [MaxLength(20)]
        public string LogLevel { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ErrorCode { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(BuildStageId))]
        public virtual BuildStage? BuildStage { get; set; }
    }
}