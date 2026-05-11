using System.Collections.Generic;

namespace CicdApp.Models
{
    public class PipelineConfig
    {
        public List<PipelineStep> Pipeline { get; set; } = new List<PipelineStep>();
    }
}