using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunCoverletReport.CoverageResults.Models
{
    public class ConditionCoverage
    {
        // <condition number = "0" type="jump" coverage="100%"/>
        public int ConditionNumber { get; set; }
        public string Type { get; set; }
        public string Coverage { get; set; }
    }
}
