namespace RunCoverletReport.Highlighting
{
    using System;
    using System.Windows.Media;

    internal class PenFactory
    {
        public Pen CreatePen(PenHighlightOptions options)
        {
            switch (options.Style)
            {
                case ColourStyle.Linear:
                    return this.CreateLinearPen(options.BaseColour, options.LinearEndColour);

                case ColourStyle.None:
                    return this.CreateTransparentPen();

                case ColourStyle.Default:
                case ColourStyle.Solid:
                default:
                    return this.CreateSolidPen(options.BaseColour);
            }
        }

        private Pen CreateLinearPen(Color startColour, Color endColour)
        {
            var penBrush = new LinearGradientBrush(startColour, endColour, 0);
            penBrush.Freeze();
            var pen = new Pen(penBrush, 2);
            pen.Freeze();
            return pen;
        }

        private Pen CreateSolidPen(Color baseColour)
        {
            var penBrush = new SolidColorBrush(baseColour);
            penBrush.Freeze();
            var pen = new Pen(penBrush, 2);
            pen.Freeze();
            return pen;
        }

        private Pen CreateTransparentPen()
        {
            var penBrush = new SolidColorBrush(Colors.Transparent);
            penBrush.Freeze();
            var pen = new Pen(penBrush, 2);
            pen.Freeze();
            return pen;
        }
    }
}