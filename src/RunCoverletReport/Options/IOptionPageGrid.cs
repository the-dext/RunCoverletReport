namespace RunCoverletReport.Options
{
    using System.Windows.Media;

    using RunCoverletReport.Highlighting;

    public interface IOptionPageGrid
    {
        Color CoveredBorderColour { get; set; }
        Color CoveredColour { get; set; }
        Color CoveredLinearEndColour { get; set; }
        string ExcludeAssembliesPattern { get; set; }
        ColourStyle HighlightStyle { get; set; }
        IntegrationType IntegrationType { get; set; }
        Color PartCoveredBorderColour { get; set; }
        Color PartCoveredColour { get; set; }
        Color PartCoveredLinearEndColour { get; set; }
        bool RestorePackages { get; set; }
        bool ShowBorders { get; set; }
        bool ShowHighlights { get; set; }
        Color UncoveredBorderColour { get; set; }
        Color UncoveredColour { get; set; }
        Color UncoveredLinearEndColour { get; set; }
    }
}