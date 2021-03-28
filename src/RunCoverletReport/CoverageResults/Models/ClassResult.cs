namespace RunCoverletReport.CoverageResults.Models
{
    using System.Collections.Generic;
    using System.Diagnostics;

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
                if (LineResults.ContainsKey(result.Value.LineNumber))
                {
                    //take the most optimistic coverage record.
                    var takeNewResult = false;
                    var existingResult = LineResults[result.Value.LineNumber];
                    var newResult = result.Value;

                    //some coverage better than none
                    if ((newResult.Result == LineResult.CoverageResultType.Covered || newResult.Result == LineResult.CoverageResultType.PartCovered)
                        && existingResult.Result == LineResult.CoverageResultType.UnCovered)
                    {
                        takeNewResult = true;
                    }
                    
                    //more coverage better than some
                    if ((newResult.Result == LineResult.CoverageResultType.Covered)
                        && existingResult.Result == LineResult.CoverageResultType.PartCovered)
                    {
                        takeNewResult = true;
                    }

                    //more hits better than less hits
                    if (newResult.Hits > existingResult.Hits)
                    {
                        takeNewResult = true;
                    }

                    if (takeNewResult)
                    {
                        Trace.TraceInformation($"{nameof(RunCoverletReport)} - {nameof(LineResult)} previously recorded in this session " +
                            $"for '{FileName}':{result.Value.LineNumber} will be discarded: {existingResult}");
                        LineResults[newResult.LineNumber] = newResult;
                    }
                    else
                    {
                        Trace.TraceError($"{nameof(RunCoverletReport)} - {nameof(LineResult)} additionally recorded " +
                            $"for '{FileName}':{result.Value.LineNumber} will be discarded: {newResult}");
                    }
                                        
                    continue;
                }
                this.LineResults.Add(result.Value.LineNumber, result.Value);
            }
        }
    }
}
