using System.Collections.Generic;
using System.Windows.Documents;

namespace RunCoverletReport.CoverageResults.Models
{
    /// <summary>
    /// Defines the <see cref="LineResult" />.
    /// </summary>
    public partial class LineResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineResult"/> class.
        /// </summary>
        /// <param name="lineNumber">The lineNumber<see cref="int"/>.</param>
        /// <param name="branch">The branch<see cref="bool"/>.</param>
        /// <param name="hits">The hits<see cref="int"/>.</param>
        /// <param name="conditionCoverage">The conditionCoverage<see cref="string"/>.</param>
        public LineResult(int lineNumber, bool branch, int hits, string conditionCoverage = null)
        {
            this.LineNumber = lineNumber;
            this.Branch = branch;
            this.Hits = hits;
            this.ConditionCoverages = (string.IsNullOrWhiteSpace(conditionCoverage) ? null : conditionCoverage);
            this.Conditions = new List<ConditionCoverage>();
        }

        /// <summary>
        /// Gets a value indicating whether Branch.
        /// </summary>
        public bool Branch { get; }

        /// <summary>
        /// Gets the ConditionCoverage.
        /// </summary>
        public new List<ConditionCoverage> Conditions { get; }

        /// <summary>
        /// Gets the Hits.
        /// </summary>
        public int Hits { get; }
        public string ConditionCoverages { get; }

        /// <summary>
        /// Gets the LineNumber.
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// branch lines have part coverage if coverlet condition says "100%"
        /// </summary>
        public bool HasPartCoverage
        {
            get
            {
                if (!this.Branch || this.ConditionCoverages.Contains("100%"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public CoverageResultType Result
        {
            get
            {
                if (this.Hits > 0)
                {
                    if (this.HasPartCoverage)
                    {
                        return CoverageResultType.PartCovered;
                    }
                    else
                    {
                        return CoverageResultType.Covered;
                    }
                }
                else
                {
                    return CoverageResultType.UnCovered;
                }
            }
        }
    }
}
