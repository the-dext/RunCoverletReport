using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RunCoverletReport.Options
{
    public class OptionValues
    {
        private RunCoverletReportPackage runCoverletReportPackage;

        public OptionValues(RunCoverletReportPackage runCoverletReportPackage)
        {
            this.runCoverletReportPackage = runCoverletReportPackage;
        }

        public Color CoveredColor { get => this.runCoverletReportPackage.CoveredColour; }
        public Color UncoveredColor { get => this.runCoverletReportPackage.UncoveredColour; }
        public Color PartCoveredColor { get => this.runCoverletReportPackage.PartCoveredColour; }
    }
}
