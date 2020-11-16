namespace RunCoverletReport.Options
{
    using System.ComponentModel;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Shell;

    using RunCoverletReport.Highlighting;

    public class OptionPageGrid : DialogPage, IOptionPageGrid
    {
        private Color coveredBorderColour = Colors.Green;
        private Color coveredColor = Color.FromArgb(50, 200, 255, 148);
        private Color coveredLinearEndColour = Colors.White;
        private string excludeAssembliesPattern = "[*.Tests?]*,[*.UITests?]*";
        private ColourStyle highlightStyle = ColourStyle.Default;
        private IntegrationType integrationType = IntegrationType.Collector;
        private Color partCoveredBorderColour = Colors.DarkOrange;
        private Color partCoveredColor = Color.FromArgb(50, 255, 195, 106);
        private Color partCoveredLinearEndColour = Colors.White;
        private bool restorePackages = true;
        private bool showBorders = true;
        private bool showHighlights = true;
        private Color uncoveredBorderColour = Colors.DarkRed;
        private Color uncoveredColor = Color.FromArgb(50, 255, 161, 161);
        private Color uncoveredLinearEndColour = Colors.White;
        private bool useMSBuild = true;

        [Category("2. Covered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for covered code border")]
        public Color CoveredBorderColour
        {
            get => this.coveredBorderColour;
            set => this.coveredBorderColour = value;
        }

        [Category("2. Covered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code highlight")]
        public Color CoveredColour
        {
            get => this.coveredColor;
            set => this.coveredColor = value;
        }

        [Category("2. Covered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for covered code when using linear highlighting")]
        public Color CoveredLinearEndColour { get => this.coveredLinearEndColour; set => this.coveredLinearEndColour = value; }

        [Category("5. Miscellaneous")]
        [DisplayName("Exclude Assemblies File Patterns")]
        [Description("Comma separated file patterns for assemblies and types to be excluded from code coverage. Follows Coverlet standards. For example [*.tests]*,[*.uitests]* will ignore all types in assemblies with a .tests or .uitests suffix. NOTE: Requires Coverlet.MSBuild integration = true")]
        public string ExcludeAssembliesPattern
        {
            get => this.excludeAssembliesPattern;
            set => this.excludeAssembliesPattern = value;
        }

        [Category("1. Highlighting")]
        [DisplayName("Highlight Style")]
        [Description("Choose between solid (default) highlighting, or use linear highlighting")]
        public ColourStyle HighlightStyle
        {
            get => this.highlightStyle; set => this.highlightStyle = value;
        }

        [Category("5. Miscellaneous")]
        [DisplayName("Integration type")]
        [Description("Set the integration type to match the Coverlet nuget packages you use. Either Coverlet.MSBuild or Coverlet.Collector.")]
        public IntegrationType IntegrationType
        {
            get => this.integrationType;
            set => this.integrationType = value;
        }

        [Category("4. Part Covered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for part covered code border")]
        public Color PartCoveredBorderColour
        {
            get => this.partCoveredBorderColour;
            set => this.partCoveredBorderColour = value;
        }

        [Category("4. Part Covered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color PartCoveredColour
        {
            get => this.partCoveredColor;
            set => this.partCoveredColor = value;
        }

        [Category("4. Part Covered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for part covered code when using linear highlighting")]
        public Color PartCoveredLinearEndColour { get => this.partCoveredLinearEndColour; set => this.partCoveredLinearEndColour = value; }

        [Category("5. Miscellaneous")]
        [DisplayName("Restore NuGet Packages")]
        [Description("Restore NuGet Packages before Test run")]
        public bool RestorePackages
        {
            get => this.restorePackages;
            set => this.restorePackages = value;
        }

        [Category("1. Highlighting")]
        [DisplayName("Show Borders")]
        [Description("Toggle whether or not borders are shown when highlighting code coverage")]
        public bool ShowBorders { get => this.showBorders; set => this.showBorders = value; }

        [Category("1. Highlighting")]
        [DisplayName("Show Highlights")]
        [Description("Toggle whether or not highlights are used to indicate code coverage")]
        public bool ShowHighlights { get => this.showHighlights; set => this.showHighlights = value; }

        [Category("3. Uncovered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for uncovered code border")]
        public Color UncoveredBorderColour
        {
            get => this.uncoveredBorderColour;
            set => this.uncoveredBorderColour = value;
        }

        [Category("3. Uncovered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color UncoveredColour
        {
            get => this.uncoveredColor;
            set => this.uncoveredColor = value;
        }

        [Category("3. Uncovered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for uncovered code when using linear highlighting")]
        public Color UncoveredLinearEndColour { get => this.uncoveredLinearEndColour; set => this.uncoveredLinearEndColour = value; }
    }
}