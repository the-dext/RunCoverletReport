namespace RunCoverletReport.Options
{
    using System.Windows.Media;

    public interface IOptionPageGrid
    {
        Color CoveredBorderColour { get; set; }
        Color CoveredColour { get; set; }
        string ExcludeAssembliesPattern { get; set; }
        IntegrationType IntegrationType { get; set; }
        Color PartCoveredBorderColour { get; set; }
        Color PartCoveredColour { get; set; }
        bool RestorePackages { get; set; }
        Color UncoveredBorderColour { get; set; }
        Color UncoveredColour { get; set; }
    }
}