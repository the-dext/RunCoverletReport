namespace RunCoverletReport
{
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="Colours" />.
    /// </summary>
    public static class Colours
    {
        /// <summary>
        /// The color to use for covered files.
        /// </summary>
        /// <returns>The <see cref="Color"/>.</returns>
        public static Color Covered()
        {
            return Color.FromArgb(100, 200, 255, 148);
        }

        /// <summary>
        /// The color to use for uncovered files.
        /// </summary>
        /// <returns>The <see cref="Color"/>.</returns>
        public static Color UnCovered()
        {
            return Color.FromArgb(100, 255, 161, 161);
        }

        /// <summary>
        /// The color to use for part covered files.
        /// </summary>
        /// <returns></returns>
        public static Color PartCovered()
        {
            return Color.FromArgb(100, 255, 195, 106);
        }
    }
}
