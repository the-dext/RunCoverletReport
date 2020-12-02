namespace RunCoverletReport.Options
{
    using System.Windows.Media;

    using RunCoverletReport.Highlighting;

    public interface IOptionPageGrid
    {
        ColourStyle BorderStyle { get; set; }
        Color CoveredBorderColour { get; set; }
        Color CoveredBorderLinearEndColour { get; set; }
        Color CoveredColour { get; set; }
        Color CoveredLinearEndColour { get; set; }
        string ExcludeAssembliesPattern { get; set; }
        ColourStyle HighlightStyle { get; set; }
        IntegrationType IntegrationType { get; set; }
        Color PartCoveredBorderColour { get; set; }
        Color PartCoveredBorderLinearEndColour { get; set; }
        Color PartCoveredColour { get; set; }
        Color PartCoveredLinearEndColour { get; set; }
        bool RestorePackages { get; set; }
        Color UncoveredBorderColour { get; set; }
        Color UncoveredColour { get; set; }
        Color UncoveredLinearEndColour { get; set; }
        Color UncoveredBorderLinearEndColour { get; set; }
    }
}