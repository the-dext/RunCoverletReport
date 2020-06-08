namespace RunCoverletReport.CoverageResults
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using RunCoverletReport.CoverageResults.Models;

    /// <summary>
    /// Defines the <see cref="CoverageResultsProvider" />.
    /// </summary>
    [Export]
    public class CoverageResultsProvider
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="CoverageResultsProvider"/> class from being created.
        /// </summary>
        static CoverageResultsProvider()
        {
            Initialise();
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

        /// <summary>
        /// The Initialise.
        /// </summary>
        public static void Initialise()
        {
            if (Instance == null)
            {
                Instance = new CoverageResultsProvider();
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
