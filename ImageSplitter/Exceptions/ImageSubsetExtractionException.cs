using System;

using SkiaSharp;

namespace ImageSplitter.Exceptions
{
    public class ImageSubsetExtractionException : Exception
    {
        public int FailedRow { get; set; }
        public int FailedColumn { get; set; }

        public SKRectI DimensionUsed { get; set; }

        public ImageSubsetExtractionException(int row, int col, SKRectI dimension) : base()
        {
            FailedRow = row;
            FailedColumn = col;
            DimensionUsed = dimension;
        }

        public override string Message => $"Failed to get a sub-section of the original image at row {FailedRow} & column {FailedColumn}. " +
            $"The dimensions used were {DimensionUsed}. {base.Message}";
    }
}
