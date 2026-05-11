namespace CicdApp.Models
{
    public class PipelineStep
    {
        public string Name { get; set; } = string.Empty;
        public string Command { get; set; } = string.Empty;
        public string Args { get; set; } = string.Empty;
        public bool StopOnFailure { get; set; }
    }
}