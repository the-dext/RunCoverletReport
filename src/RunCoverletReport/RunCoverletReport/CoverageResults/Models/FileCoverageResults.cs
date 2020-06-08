namespace RunCoverletReport.CoverageResults.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FileCoverageResults" />.
    /// </summary>
    public class FileCoverageResults
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCoverageResults"/> class.
        /// </summary>
        /// <param name="filename">The filename<see cref="string"/>.</param>
        /// <param name="classResults">The classResults<see cref="Dictionary{string, ClassResult}"/>.</param>
        public FileCoverageResults(string filename, Dictionary<string, ClassResult> classResults)
        {
            this.FileName = filename;
            this.ClassResults = classResults;
        }

        /// <summary>
        /// Gets the ClassResults.
        /// </summary>
        public Dictionary<string, ClassResult> ClassResults { get; }

        /// <summary>
        /// Gets the FileName.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The FindFile.
        /// </summary>
        /// <param name="filename">The filename<see cref="string"/>.</param>
        /// <returns>The <see cref="ClassResult"/>.</returns>
        internal ClassResult FindFile(string filename)
        {
            foreach (var classResult in this.ClassResults)
            {
                if (filename.EndsWith(classResult.Key))
                {
                    return classResult.Value;
                }
            }
            return null;
        }
    }
}
