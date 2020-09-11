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
        private IntegrationType integrationType = IntegrationType.Collector;
        private bool restorePackages = true;

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
        [DisplayName("Integration type")]
        [Description("Set the integration type to match the Coverlet nuget packages you use. Either Coverlet.MSBuild or Coverlet.Collector.")]
        public IntegrationType IntegrationType
        {
            get { return this.integrationType; }
            set { this.integrationType = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Restore NuGet Packages")]
        [Description("Restore NuGet Packages before Test run")]
        public bool RestorePackages
        {
            get { return this.restorePackages; }
            set { this.restorePackages = value; }
        }
    }

    public enum IntegrationType
    {
        [Description("Coverlet.Collector")]
        Collector,

        [Description("Coverlet.MSBuild")]
        MSBuild,
    }
}