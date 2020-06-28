using RunCoverletReport.CoverageResults;
using System.Windows.Media;

namespace RunCoverletReport.Highlighting
{
    public class SyntaxHighlighter
    {
        public static GeometryDrawing CreateCoveredHighlight(Geometry geometry)
        {
            var coveredBrush = new SolidColorBrush(CoverageResultsProvider.Instance.Options.CoveredColour);
            coveredBrush.Freeze();

            var penBrush = new SolidColorBrush(Colors.Green);
            penBrush.Freeze();
            var coveredPen = new Pen(penBrush, 2);
            coveredPen.Freeze();

            return new GeometryDrawing(coveredBrush, coveredPen, geometry);
        }

        public static GeometryDrawing CreatePartCoveredHighlight(Geometry geometry)
        {
            var partCoveredBrush = new SolidColorBrush(CoverageResultsProvider.Instance.Options.PartCoveredColour);
            partCoveredBrush.Freeze();

            var penBrush3 = new SolidColorBrush(Colors.DarkOrange);
            penBrush3.Freeze();
            var partCoveredPen = new Pen(penBrush3, 2);
            partCoveredPen.Freeze();

            return new GeometryDrawing(partCoveredBrush, partCoveredPen, geometry);
        }

        internal static GeometryDrawing CreateUnCoveredHighlight(Geometry geometry)
        {
            var unCoveredBrush = new SolidColorBrush(CoverageResultsProvider.Instance.Options.UncoveredColour);
            unCoveredBrush.Freeze();

            var penBrush3 = new SolidColorBrush(Colors.DarkRed);
            penBrush3.Freeze();
            var unCoveredPen = new Pen(penBrush3, 2);
            unCoveredPen.Freeze();

            return new GeometryDrawing(unCoveredBrush, unCoveredPen, geometry);
        }
    }
}