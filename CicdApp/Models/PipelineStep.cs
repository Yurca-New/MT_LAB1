using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;

namespace CicdApp.Models
{
    public class PipelineStep
    {
        public string name { get; set; }
        public string command { get; set; }
        public string args { get; set; }
        public bool stopOnFailure { get; set; }
    }
}
