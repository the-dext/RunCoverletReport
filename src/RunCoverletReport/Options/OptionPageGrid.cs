namespace RunCoverletReport.Options
{
    using System.ComponentModel;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Shell;

    public class OptionPageGrid : DialogPage, IOptionPageGrid
    {
        private Color coveredBorderColour = Colors.DarkRed;
        private Color coveredColor = Color.FromArgb(50, 200, 255, 148);
        private string excludeAssembliesPattern = "[*.Tests?]*,[*.UITests?]*";
        private IntegrationType integrationType = IntegrationType.Collector;
        private Color partCoveredBorderColour = Colors.DarkOrange;
        private Color partCoveredColor = Color.FromArgb(50, 255, 195, 106);
        private bool restorePackages = true;
        private Color uncoveredBorderColour = Colors.Green;
        private Color uncoveredColor = Color.FromArgb(50, 255, 161, 161);
        private bool useMSBuild = true;

        public Color CoveredBorderColour
        {
            get => this.coveredBorderColour;
            set => this.coveredBorderColour = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Covered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color CoveredColour
        {
            get => this.coveredColor;
            set => this.coveredColor = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Exclude Assemblies File Patterns")]
        [Description("Comma separated file patterns for assemblies and types to be excluded from code coverage. Follows Coverlet standards. For example [*.tests]*,[*.uitests]* will ignore all types in assemblies with a .tests or .uitests suffix. NOTE: Requires Coverlet.MSBuild integration = true")]
        public string ExcludeAssembliesPattern
        {
            get => this.excludeAssembliesPattern;
            set => this.excludeAssembliesPattern = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Integration type")]
        [Description("Set the integration type to match the Coverlet nuget packages you use. Either Coverlet.MSBuild or Coverlet.Collector.")]
        public IntegrationType IntegrationType
        {
            get => this.integrationType;
            set => this.integrationType = value;
        }

        public Color PartCoveredBorderColour
        {
            get => this.partCoveredBorderColour;
            set => this.partCoveredBorderColour = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Part Covered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color PartCoveredColour
        {
            get => this.partCoveredColor;
            set => this.partCoveredColor = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Restore NuGet Packages")]
        [Description("Restore NuGet Packages before Test run")]
        public bool RestorePackages
        {
            get => this.restorePackages;
            set => this.restorePackages = value;
        }

        public Color UncoveredBorderColour
        {
            get => this.uncoveredBorderColour;
            set => this.uncoveredBorderColour = value;
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Highlight - Uncovered Code Colour")]
        [Description("ARGB Colour for covered code")]
        public Color UncoveredColour
        {
            get => this.uncoveredColor;
            set => this.uncoveredColor = value;
        }
    }
}