namespace RunCoverletReport
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using RunCoverletReport.CoverageResults.Models;

    /// <summary>
    /// Defines the <see cref="CoverageReader"/>.
    /// </summary>
    public class CoverageReader
    {
        /// <summary>
        /// The ReadFile.
        /// </summary>
        /// <param name="filename">The filename <see cref="string"/>.</param>
        /// <returns>The <see cref="FileCoverageResults"/>.</returns>
        public FileCoverageResults ReadFile(string filename)
        {
            if (!filename.ToLowerInvariant().EndsWith("cobertura.xml"))
            {
                throw new NotSupportedException("File must be coverage.cobertura.xml");
            }
            var doc = new XmlDocument();
            doc.Load(filename);

            var classResults = new Dictionary<string, ClassResult>();

            var classesNode = doc.SelectNodes("//class");
            foreach (XmlNode classNode in classesNode)
            {
                var className = classNode.Attributes["name"].Value;
                var classFileName = classNode.Attributes["filename"].Value;
                var lineCoverage = classNode.Attributes["line-rate"] == null ? 0 : double.Parse(classNode.Attributes["line-rate"].Value);
                var linesNode = classNode.SelectSingleNode("lines");

                var mergeClassResults = classResults.ContainsKey(classFileName);

                var lineResults = new Dictionary<int, LineResult>();
                foreach (XmlNode lineNode in linesNode.ChildNodes)
                {
                    if (lineNode.Name == "line")
                    {
                        var lineNumber = int.Parse(lineNode.Attributes["number"].Value);
                        var hits = int.Parse(lineNode.Attributes["hits"].Value);
                        var branch = bool.Parse(lineNode.Attributes["branch"].Value);
                        var conditionCoverage = lineNode.Attributes["condition-coverage"]?.Value ?? string.Empty;

                        if (lineResults.ContainsKey(lineNumber))
                        {
                            throw new InvalidOperationException($"The line number '{lineNumber}' has already used as a key for line results. Class filename '{classFileName}'.\r\ncoverage filename:'{filename}'");
                        }

                        lineResults.Add(lineNumber, new LineResult(lineNumber, branch, hits, conditionCoverage));
                    }
                }

                if (!mergeClassResults)
                {
                    classResults.Add(classFileName, new ClassResult(classFileName, className, lineResults));
                }
                else
                {
                    classResults[classFileName].AddLineResults(lineResults);
                }
            }

            return new FileCoverageResults(filename, classResults);
        }
    }
}