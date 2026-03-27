using System;
using System.Collections.Generic;
using System.Text;

namespace CicdApp.Models
{
    public class PipelineConfig
    {
        public List<PipelineStep> pipeline { get; set; } = new List<PipelineStep>();
        
        public void DisplayInfo()
        {
            foreach (var step in pipeline)
            {
                Console.WriteLine($"Name: {step.name}");
                Console.WriteLine($"Command: {step.command}");
                Console.WriteLine($"Args: {step.args}");
                Console.WriteLine($"StopOnFailure: {step.stopOnFailure}");
            }
        }
    }
}
