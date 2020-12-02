namespace RunCoverletReport.Highlighting
{
    using System.Windows.Media;

    using RunCoverletReport.CoverageResults;

    public class SyntaxHighlighter
    {
        public GeometryDrawing CreateCoveredHighlight(Geometry geometry)
        {
            var borderPenOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.CoveredBorderColour,
                Style = CoverageResultsProvider.Instance.Options.BorderStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.CoveredBorderLinearEndColour,
            };

            var fillBrushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.CoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.CoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(borderPenOptions);
            var fillBrush = new BrushFactory().CreateBrush(fillBrushOptions);

            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }

        public GeometryDrawing CreatePartCoveredHighlight(Geometry geometry)
        {
            var borderPenOptionspenOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.PartCoveredBorderColour,
                Style = CoverageResultsProvider.Instance.Options.BorderStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.PartCoveredBorderLinearEndColour,
            };

            var fillBrushOptionsbrushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.PartCoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.PartCoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(borderPenOptionspenOptions);
            var fillBrush = new BrushFactory().CreateBrush(fillBrushOptionsbrushOptions);
            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }

        internal GeometryDrawing CreateUnCoveredHighlight(Geometry geometry)
        {
            var borderPenOptions = new PenHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.UncoveredBorderColour,
                Style = CoverageResultsProvider.Instance.Options.BorderStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.UncoveredBorderLinearEndColour,
            };

            var fillBrushOptionsbrushOptions = new BrushHighlightOptions
            {
                BaseColour = CoverageResultsProvider.Instance.Options.UncoveredColour,
                Style = CoverageResultsProvider.Instance.Options.HighlightStyle,
                LinearEndColour = CoverageResultsProvider.Instance.Options.UncoveredLinearEndColour,
            };

            var borderPen = new PenFactory().CreatePen(borderPenOptions);
            var fillBrush = new BrushFactory().CreateBrush(fillBrushOptionsbrushOptions);
            return new GeometryDrawing(fillBrush, borderPen, geometry);
        }
    }
}