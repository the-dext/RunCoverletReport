namespace RunCoverletReport.Options
{
    using System.ComponentModel;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Shell;

    using RunCoverletReport.Highlighting;

    public class OptionPageGrid : DialogPage, IOptionPageGrid
    {
        private bool useMSBuild = true;

        [Category("1. Highlighting")]
        [DisplayName("Border Style")]
        [Description("Choose between border styles")]
        public ColourStyle BorderStyle { get; set; } = ColourStyle.Default;

        [Category("2. Covered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for covered code border")]
        public Color CoveredBorderColour { get; set; } = Colors.Green;

        [Category("2. Covered Code")]
        [DisplayName("Border Linear End Colour")]
        [Description("ARGB end colour for border with Linear style")]
        public Color CoveredBorderLinearEndColour { get; set; } = Colors.Transparent;

        [Category("2. Covered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code highlight")]
        public Color CoveredColour { get; set; } = Color.FromArgb(50, 200, 255, 148);

        [Category("2. Covered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for covered code when using linear highlighting")]
        public Color CoveredLinearEndColour { get; set; } = Colors.Transparent;

        [Category("5. Miscellaneous")]
        [DisplayName("Exclude Assemblies File Patterns")]
        [Description("Comma separated file patterns for assemblies and types to be excluded from code coverage. Follows Coverlet standards. For example [*.tests]*,[*.uitests]* will ignore all types in assemblies with a .tests or .uitests suffix. NOTE: Requires Coverlet.MSBuild integration = true")]
        public string ExcludeAssembliesPattern { get; set; } = "[*.Tests?]*,[*.UITests?]*";

        [Category("1. Highlighting")]
        [DisplayName("Highlight Style")]
        [Description("Choose between solid (default) highlighting, or use linear highlighting")]
        public ColourStyle HighlightStyle { get; set; } = ColourStyle.Default;

        [Category("5. Miscellaneous")]
        [DisplayName("Integration type")]
        [Description("Set the integration type to match the Coverlet nuget packages you use. Either Coverlet.MSBuild or Coverlet.Collector.")]
        public IntegrationType IntegrationType { get; set; } = IntegrationType.Collector;

        [Category("4. Part Covered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for part covered code border")]
        public Color PartCoveredBorderColour { get; set; } = Colors.DarkOrange;

        [Category("4. Part Covered Code")]
        [DisplayName("Border Linear End Colour")]
        [Description("ARGB end colour for border with Linear style")]
        public Color PartCoveredBorderLinearEndColour { get; set; } = Colors.Transparent;

        [Category("4. Part Covered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color PartCoveredColour { get; set; } = Color.FromArgb(50, 255, 195, 106);

        [Category("4. Part Covered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for part covered code when using linear highlighting")]
        public Color PartCoveredLinearEndColour { get; set; } = Colors.Transparent;

        [Category("5. Miscellaneous")]
        [DisplayName("Restore NuGet Packages")]
        [Description("Restore NuGet Packages before Test run")]
        public bool RestorePackages { get; set; } = true;

        [Category("5. Miscellaneous")]
        [DisplayName("Use Run Settings")]
        [Description("Use .runsettings file on solution root folder")]
        public bool UseRunSettings { get; set; }

        [Category("3. Uncovered Code")]
        [DisplayName("Border Colour")]
        [Description("ARGB Colour for uncovered code border")]
        public Color UncoveredBorderColour { get; set; } = Colors.DarkRed;

        [Category("3. Uncovered Code")]
        [DisplayName("Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color UncoveredColour { get; set; } = Color.FromArgb(50, 255, 161, 161);

        [Category("3. Uncovered Code")]
        [DisplayName("Linear End Colour")]
        [Description("ARGB end colour for uncovered code when using linear highlighting")]
        public Color UncoveredLinearEndColour { get; set; } = Colors.Transparent;

        [Category("3. Uncovered Code")]
        [DisplayName("Border Linear End Colour")]
        [Description("ARGB end colour for border with Linear style")]
        public Color UncoveredBorderLinearEndColour { get; set; } = Colors.Transparent;
    }
}