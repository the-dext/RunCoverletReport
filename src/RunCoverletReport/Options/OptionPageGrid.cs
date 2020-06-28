namespace RunCoverletReport.Options
{
    using Microsoft.VisualStudio.Shell;
    using System.ComponentModel;
    using System.Windows.Media;

    public class OptionPageGrid : DialogPage
    {
        private Color coveredColor = Color.FromArgb(50, 200, 255, 148);
        private Color uncoveredColor = Color.FromArgb(50, 255, 161, 161);
        private Color partCoveredColor = Color.FromArgb(50, 255, 195, 106);
        private string excludeAssembliesPattern = "[*.Tests?]*,[*.UITests?]*";
        private bool useMSBuild = true;

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Covered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color CoveredColour
        {
            get { return this.coveredColor; }
            set { this.coveredColor = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Uncovered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color UncoveredColour
        {
            get { return this.uncoveredColor; }
            set { this.uncoveredColor = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Part Covered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color PartCoveredColour
        {
            get { return this.partCoveredColor; }
            set { this.partCoveredColor = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Exclude Assemblies File Patterns")]
        [Description("Comma separated file patterns for assemblies and types to be excluded from code coverage. Follows Coverlet standards. For example [*.tests]*,[*.uitests]* will ignore all types in assemblies with a .tests or .uitests suffix. NOTE: Requires Coverlet.MSBuild integration = true")]
        public string ExcludeAssembliesPattern
        {
            get { return this.excludeAssembliesPattern; }
            set { this.excludeAssembliesPattern = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Use Coverlet.MSBuild integration")]
        [Description("Use Coverlet.MSBuild integration instead of Coverlet.Collector (requires Coverlet.MSBuild packages to be installed into your unit test projects")]
        public bool UseMSBuild
        {
            get { return this.useMSBuild; }
            set { this.useMSBuild = value; }
        }
    }
}