namespace RunCoverletReport.Highlighting
{
    using System;
    using System.Windows.Media;

    public class BrushFactory
    {
        public Brush CreateBrush(BrushHighlightOptions options)
        {
            switch (options.Style)
            {
                case ColourStyle.Linear:
                    return this.CreateLinearBrush(options.BaseColour, options.LinearEndColour);

                case ColourStyle.None:
                    return this.CreateTransparentBrush();

                case ColourStyle.Default:
                case ColourStyle.Solid:
                default:
                    return this.CreateSolidBrush(options.BaseColour);
            }
        }

        private Brush CreateLinearBrush(Color baseColour, Color linearEndColour)
        {
            var brush = new LinearGradientBrush(baseColour, linearEndColour, 0);
            brush.Freeze();
            return brush;
        }

        private Brush CreateSolidBrush(Color baseColour)
        {
            var brush = new SolidColorBrush(baseColour);
            brush.Freeze();
            return brush;
        }

        private Brush CreateTransparentBrush()
        {
            var brush = new SolidColorBrush(Colors.Transparent);
            brush.Freeze();
            return brush;
        }
    }
}