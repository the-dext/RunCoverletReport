namespace RunCoverletReport.CoverageResults
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Windows.Media;
    using RunCoverletReport.CoverageResults.Models;
    using RunCoverletReport.Options;

    /// <summary>
    /// Defines the <see cref="CoverageResultsProvider" />.
    /// </summary>
    [Export]
    public class CoverageResultsProvider
    {
        private RunCoverletReportPackage runCoverletReportPackage;

        /// <summary>
        /// Prevents a default instance of the <see cref="CoverageResultsProvider"/> class from being created.
        /// </summary>
        public CoverageResultsProvider(RunCoverletReportPackage runCoverletReportPackage)
        {
            this.runCoverletReportPackage = runCoverletReportPackage;
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static CoverageResultsProvider Instance { get; private set; }

        /// <summary>
        /// Gets the CoverageResults.
        /// </summary>
        public FileCoverageResults CoverageResults { get; private set; }

        private static bool _showSyntaxHighlighting;
        public static bool ShowSyntaxHighlighting
        {
            get => _showSyntaxHighlighting;
            set
            {
                _showSyntaxHighlighting = value;
                Instance.OnShowSyntaxHighlightingChanged();
            }
        }

        public OptionPageGrid Options { get => this.runCoverletReportPackage.OptionsPage; }

        /// <summary>
        /// The Initialise.
        /// </summary>
        public static void Initialise(RunCoverletReportPackage runCoverletReportPackage)
        {
            if (Instance == null)
            {
                Instance = new CoverageResultsProvider(runCoverletReportPackage);
                _showSyntaxHighlighting = true;
            }
        }

        /// <summary>
        /// The SetResults.
        /// </summary>
        /// <param name="results">The results<see cref="FileCoverageResults"/>.</param>
        public void SetResults(FileCoverageResults results)
        {
            this.CoverageResults = results;

            this.OnNewResultsAvailable(results);
        }

        public static event EventHandler<FileCoverageResults> NewResultsAvailable;
        public static event EventHandler<FileCoverageResults> ShowSyntaxHighlightingChanged;

        public void OnShowSyntaxHighlightingChanged()
        {
            EventHandler<FileCoverageResults> handler = ShowSyntaxHighlightingChanged;
            if (handler != null)
            {
                try
                {
                    handler(this, this.CoverageResults);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        protected virtual void OnNewResultsAvailable(FileCoverageResults e)
        {
            EventHandler<FileCoverageResults> handler = NewResultsAvailable;
            if (handler != null)
            {
                try
                {
                    handler(this, e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
