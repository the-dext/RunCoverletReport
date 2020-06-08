namespace RunCoverletReport.CoverageResults.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ClassResult" />.
    /// </summary>
    public class ClassResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassResult"/> class.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <param name="className">The className<see cref="string"/>.</param>
        /// <param name="coverage">The coverage<see cref="double"/>.</param>
        /// <param name="lineResults">The lineResults<see cref="Dictionary{int, LineResult}"/>.</param>
        public ClassResult(string fileName, string className, Dictionary<int, LineResult> lineResults)
        {
            this.FileName = fileName;
            this.ClassName = className;
            this.LineResults = lineResults;
        }

        /// <summary>
        /// Gets the ClassName.
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Gets the FileName.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the LineResults.
        /// </summary>
        public Dictionary<int, LineResult> LineResults { get; }


        /// <summary>
        /// Adds additional line results to cover the scenario where coverage file lists same file twice.
        /// </summary>
        /// <param name="lineResults">The line results.</param>
        public void AddLineResults(Dictionary<int, LineResult> lineResults)
        {
            foreach (var result in lineResults)
            {
                this.LineResults.Add(result.Value.LineNumber, result.Value);
            }
        }
    }
}
