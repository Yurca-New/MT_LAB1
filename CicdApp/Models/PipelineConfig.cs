using CicdApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CicdApp.Models
{
    public class PipelineConfig
    {
        public List<PipelineStep> pipeline { get; set; } = new List<PipelineStep>();
    }
}
