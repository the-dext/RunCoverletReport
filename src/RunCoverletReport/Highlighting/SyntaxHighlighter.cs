namespace RunCoverletReport.Highlighting
{
    using System.Windows.Media;

    using RunCoverletReport.CoverageResults;

    public class SyntaxHighlighter
    {
        public GeometryDrawing CreateCoveredHighlight(Geometry geometry)
        {
            var penOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.CoveredBorderColour,
                Style = ColourStyle.Solid,
            };

            var brushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.CoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.CoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(penOptions);
            var fillBrush = new BrushFactory().CreateBrush(brushOptions);

            // var fillBrush = new SolidColorBrush(CoverageResultsProvider.Instance.Options.CoveredColour);

            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }

        public GeometryDrawing CreatePartCoveredHighlight(Geometry geometry)
        {
            var penOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.PartCoveredBorderColour,
                Style = ColourStyle.Solid,
            };

            var brushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.PartCoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.PartCoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(penOptions);
            var fillBrush = new BrushFactory().CreateBrush(brushOptions);
            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }

        internal GeometryDrawing CreateUnCoveredHighlight(Geometry geometry)
        {
            var penOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.UncoveredBorderColour,
                Style = ColourStyle.Solid,
            };

            var brushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.UncoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.UncoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(penOptions);
            var fillBrush = new BrushFactory().CreateBrush(brushOptions);
            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }
    }
}