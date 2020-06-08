using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using RunCoverletReport.CoverageResults;
using RunCoverletReport.CoverageResults.Models;
using RunCoverletReport.Highlighting;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace RunCoverletReport.TextAdornments
{
    /// <summary>
    /// CoverageAdornment places red boxes behind all the "a"s in the editor window
    /// </summary>
    internal sealed class CoverageAdornment
    {
        private readonly ITextDocumentFactoryService textDocumentFactory;

        /// <summary>
        /// The layer of the adornment.
        /// </summary>
        private readonly IAdornmentLayer layer;

        /// <summary>
        /// Text view where the adornment is created.
        /// </summary>
        private readonly IWpfTextView view;
        private ITextDocument TextDocument;

        private ClassResult CoverageResultsForThisFile;
        private bool HasFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverageAdornment"/> class.
        /// </summary>
        /// <param name="view">Text view to create the adornment for</param>
        public CoverageAdornment(IWpfTextView view, ITextDocumentFactoryService textDocumentFactory)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.textDocumentFactory = textDocumentFactory;

            this.layer = view.GetAdornmentLayer("CoverageAdornment");

            this.view = view;
            this.view.LayoutChanged += this.OnLayoutChanged;

            // store the file name
            this.HasFileName = this.textDocumentFactory.TryGetTextDocument(this.view.TextBuffer, out this.TextDocument);

            // subscribe to new results available...
            CoverageResultsProvider.NewResultsAvailable += this.OnNewResultsAvailable;
            CoverageResultsProvider.ShowSyntaxHighlightingChanged += this.OnShowSyntaxHighlightingChanged;
        }

        ~CoverageAdornment()
        {
            CoverageResultsProvider.NewResultsAvailable -= this.OnNewResultsAvailable;
            CoverageResultsProvider.ShowSyntaxHighlightingChanged -= this.OnShowSyntaxHighlightingChanged;
        }

        private void OnShowSyntaxHighlightingChanged(object sender, FileCoverageResults e)
        {
            if (CoverageResultsProvider.ShowSyntaxHighlighting)
            {
                ShowCoverageForWholeFile();
            }
            else
            {
                RemoveHighlightingForFile();
            }
        }

        private void RemoveHighlightingForFile()
        {
            this.layer.RemoveAllAdornments();
        }

        private void OnNewResultsAvailable(object sender, FileCoverageResults e)
        {
            ShowCoverageForWholeFile();
        }

        private void ShowCoverageForWholeFile()
        {
            if (!CoverageResultsProvider.ShowSyntaxHighlighting || !this.HasFileName)
            {
                return;
            }

            // update all lines
            this.CoverageResultsForThisFile = CoverageResultsProvider.Instance?.CoverageResults?.FindFile(this.TextDocument.FilePath);

            if (this.CoverageResultsForThisFile == null)
            {
                return;
            }

            var textViewLines = this.view.TextViewLines;
            foreach (var line in textViewLines)
            {
                this.CreateVisuals(line);
            }
        }

        /// <summary>
        /// Handles whenever the text displayed in the view changes by adding the adornment to any reformatted lines
        /// </summary>
        /// <remarks><para>This event is raised whenever the rendered text displayed in the <see cref="ITextView"/> changes.</para>
        /// <para>It is raised whenever the view does a layout (which happens when DisplayTextLineContainingBufferPosition is called or in response to text or classification changes).</para>
        /// <para>It is also raised whenever the view scrolls horizontally or when its size changes.</para>
        /// </remarks>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        internal void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            if (!CoverageResultsProvider.ShowSyntaxHighlighting || !this.HasFileName)
            {
                return;
            }

            this.CoverageResultsForThisFile = CoverageResultsProvider.Instance?.CoverageResults?.FindFile(this.TextDocument.FilePath);
            if (this.CoverageResultsForThisFile == null)
            {
                return;
            }

            foreach (var line in e.NewOrReformattedLines)
            {
                this.CreateVisuals(line);
            }
        }

        /// <summary>
        /// Adds the scarlet box behind the 'a' characters within the given line
        /// </summary>
        /// <param name="line">Line to add the adornments</param>
        private void CreateVisuals(ITextViewLine currline)
        {
            // Loop through each character, and place a box around any 'a'
            var lineNumber = currline.Start.GetContainingLine().LineNumber + 1;

            this.CoverageResultsForThisFile.LineResults.TryGetValue(lineNumber, out var lineResult);

            if (lineResult == null)
            {
                return;
            }

            var geometry = this.view.TextViewLines.GetMarkerGeometry(currline.Extent);
            if (geometry != null)
            {
                var drawing = GetCoverageHighlightDrawing(lineResult, geometry);
                drawing.Freeze();

                var drawingImage = new DrawingImage(drawing);
                drawingImage.Freeze();

                var image = new Image
                {
                    Source = drawingImage,
                };

                // Align the image with the top of the bounds of the text geometry
                Canvas.SetLeft(image, geometry.Bounds.Left);
                Canvas.SetTop(image, geometry.Bounds.Top);

                this.layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, currline.Extent, null, image, null);
            }
        }

        private GeometryDrawing GetCoverageHighlightDrawing(LineResult lineResult, Geometry geometry)
        {
            switch (lineResult.Result)
            {
                case LineResult.CoverageResultType.Covered:
                    return SyntaxHighlighter.CreateCoveredHighlight(geometry);
                case LineResult.CoverageResultType.PartCovered:
                    return SyntaxHighlighter.CreatePartCoveredHighlight(geometry);
                case LineResult.CoverageResultType.UnCovered:
                    return SyntaxHighlighter.CreateUnCoveredHighlight(geometry);
                default:
                    throw new NotSupportedException("Coverage result type is not one of the supported values");
            }
        }
    }
}