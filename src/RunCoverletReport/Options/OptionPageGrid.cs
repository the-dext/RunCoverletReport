namespace RunCoverletReport.Options
{
    using Microsoft.VisualStudio.Shell;
    using System.ComponentModel;
    using System.Windows.Media;

    public class OptionPageGrid : DialogPage
    {
        private Color coveredColor = Color.FromArgb(50, 200, 255, 148);
        private Color uncoveredColor = Color.FromArgb(50, 255, 161, 161);
        private Color partCoveredColor = Color.FromArgb(50, 255, 195, 106);

        [Category("Run Coverlet Report")]
        [DisplayName("Covered Code Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color CoveredColour
        {
            get { return coveredColor; }
            set { coveredColor = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Uncovered Code Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color UncoveredColour
        {
            get { return uncoveredColor; }
            set { uncoveredColor = value; }
        }

        [Category("Run Coverlet Report")]
        [DisplayName("Part Covered Code Highlight Colour")]
        [Description("ARGB Colour for covered code")]
        public Color PartCoveredColour
        {
            get { return partCoveredColor; }
            set { partCoveredColor = value; }
        }
    }
}
