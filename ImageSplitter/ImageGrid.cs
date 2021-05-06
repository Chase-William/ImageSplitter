using System;
using System.IO;

using SkiaSharp;

using ImageSplitter.Exceptions;

namespace ImageSplitter
{
    public class ImageGrid : IDisposable
    {
        /// <summary>
        /// Grid of <see cref="SKBitmap"/>s created by the constructor.
        /// </summary>
        public SKBitmap[,] Bitmaps { get; private set; }

        /// <summary>
        /// Disposed state.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Initializes a <see cref="ImageGrid"/> instance and its <see cref="Bitmaps"/> property.
        /// </summary>
        /// <param name="fileName">Used to create the <see cref="Bitmaps"/> 2d array.</param>
        /// <param name="rows">Number of rows to be divided into equally.</param>
        /// <param name="columns">Number of columns to be divided into equally.</param>
        /// <exception cref="ImageSubsetExtractionException"></exception>
        public ImageGrid(string fileName, int rows, int columns)
        {
            using (SKBitmap bmp = SKBitmap.Decode(fileName)) // Automatically cleanup
            {
                SKBitmap[,] bitmaps = new SKBitmap[rows, columns];
                // Dimension Data
                int width = bmp.Width / columns;
                int height = bmp.Height / rows;
                SKRectI dimensions;
                int left = 0;
                int top = 0;
                int right = 0;
                int bottom = 0;

                for (int row = 0; row < rows; row++) // Rows
                {
                    top = height * row;
                    bottom = top + height;
                    for (int col = 0; col < columns; col++) // Column
                    {
                        left = width * col;
                        right = left + width;
                        dimensions = new SKRectI(left, top, right, bottom);
                        SKBitmap map = new SKBitmap(width, height);
                        if (!bmp.ExtractSubset(map, dimensions))
                        {
                            DiposeBitmaps(bitmaps, row, col);
                            throw new ImageSubsetExtractionException(row, col, dimensions);
                        }

                        bitmaps[row, col] = map;
                    }
                }

                Bitmaps = bitmaps;
            }

            /* Disposes bitmaps until failure location */
            void DiposeBitmaps(SKBitmap[,] bmps, int failedRow, int failedColumn)
            {
                for (int row = 0; row <= failedRow; row++)  // Dispose through last row              
                    for (int col = 0; col < failedColumn; col++) // Stop before failed bitmap (failed bitmap is null)                       
                        bmps[row, col].Dispose();
            }
        }

        /// <summary>
        /// Disposes all unmanaged resources owned by this <see cref="ImageGrid"/>.
        /// </summary>
        public void Dispose()
        {
            if (isDisposed) return;
            OnDispose();
        }
      
        /// <summary>
        /// Calls <see cref="DisposeBitmaps(SKBitmap[,])"/> and sets <see cref="isDisposed"/> to true after completion.
        /// </summary>
        protected virtual void OnDispose()
        {            
            DisposeBitmaps(Bitmaps);
            isDisposed = true;
        }

        ~ImageGrid()
            => Dispose();

        /// <summary>
        /// Saves the <see cref="Bitmaps"/> to file.
        /// </summary>
        /// <param name="fileName">Where to be saved.</param>
        /// <param name="format">File extension to be used.</param>
        /// <param name="quality">Percentage of quality to be saved with.</param>
        public void SaveBitmaps(string fileName, SKEncodedImageFormat format, int quality)
            => SaveBitmaps(Bitmaps, fileName, format, quality);

        /// <summary>
        /// Saves the given bitmaps to file.
        /// </summary>
        /// <param name="bitmaps">To be saved.</param>
        /// <param name="fileName">Where to be saved.</param>
        /// <param name="format">File extension to be used.</param>
        /// <param name="quality">Percentage of quality to be saved with.</param>
        public static void SaveBitmaps(SKBitmap[,] bitmaps, string fileName, SKEncodedImageFormat format, int quality)
        {
            int rows = bitmaps.GetLength(0);
            int cols = bitmaps.GetLength(1);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                {
                    using (var image = SKImage.FromBitmap(bitmaps[row, col]))
                    using (var data = image.Encode(format, quality))
                    {
                        using (var stream = File.OpenWrite($"{fileName}-r{row}-c{col}.{format}"))
                        {
                            data.SaveTo(stream);
                        }
                    }
                }
        }

        /// <summary>
        /// Disposes all bitmaps given with null check. Always iterates over entire collection even when some bitmaps are null.
        /// </summary>
        /// <param name="bitmaps">To be disposed of.</param>
        public static void DisposeBitmaps(SKBitmap[,] bitmaps)
        {
            int rows = bitmaps.GetLength(0);
            int cols = bitmaps.GetLength(1);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    bitmaps[row, col]?.Dispose();
        }
    }
}
