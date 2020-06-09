using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RunCoverletReport.Highlighting
{
    public class SyntaxHighlighter
    {
        private static SolidColorBrush CoveredBrush;
        private static Pen CoveredPen;
        private static SolidColorBrush PartCoveredBrush;
        private static Pen PartCoveredPen;
        private static Pen UnCoveredPen;
        private static SolidColorBrush UnCoveredBrush;

        public static GeometryDrawing CreateCoveredHighlight(Geometry geometry)
        {
            if (CoveredBrush == null)
            // Create the pen and brush to color the box behind covered and uncovered text
            {
                CoveredBrush = new SolidColorBrush(Colours.Covered());
                CoveredBrush.Freeze();
            }

            if (CoveredPen == null)
            {
                var penBrush = new SolidColorBrush(Colors.Green);
                penBrush.Freeze();
                CoveredPen = new Pen(penBrush, 2);
                CoveredPen.Freeze();
            }

            return new GeometryDrawing(CoveredBrush, CoveredPen, geometry);
        }

        public static GeometryDrawing CreatePartCoveredHighlight(Geometry geometry)
        {
            if (PartCoveredBrush == null)
            {
                PartCoveredBrush = new SolidColorBrush(Colours.PartCovered());
                PartCoveredBrush.Freeze();
            }

            if (PartCoveredPen == null)
            {
                var penBrush3 = new SolidColorBrush(Colors.DarkOrange);
                penBrush3.Freeze();
                PartCoveredPen = new Pen(penBrush3, 2);
                PartCoveredPen.Freeze();
            }

            return new GeometryDrawing(PartCoveredBrush, PartCoveredPen, geometry);
        }

        internal static GeometryDrawing CreateUnCoveredHighlight(Geometry geometry)
        {
            if (UnCoveredBrush == null)
            {
                UnCoveredBrush = new SolidColorBrush(Colours.UnCovered());
                UnCoveredBrush.Freeze();
            }

            if (UnCoveredPen == null)
            {
                var penBrush3 = new SolidColorBrush(Colors.DarkRed);
                penBrush3.Freeze();
                UnCoveredPen = new Pen(penBrush3, 2);
                UnCoveredPen.Freeze();
            }

            return new GeometryDrawing(UnCoveredBrush, UnCoveredPen, geometry);
        }
    }
}
