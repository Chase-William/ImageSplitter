using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SkiaSharp;

namespace ImageDivider
{
    public static class ImgDivider
    {        
        //public DividedImage(Stream bitmapStream)
        //{
        //    try
        //    {
        //        SKBitmap bmp = SKBitmap.Decode(bitmapStream); // Throws Error
        //        Console.WriteLine();
        //    }                        
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine();
        //    }
        //}

        public static SKBitmap[,] GetDividedEqually(string fileName, int rows, int columns)
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
                            return null; // Failure
                        }

                        bitmaps[row, col] = map;
                    }
                }

                return bitmaps;
            }

            /* Disposes bitmaps until the failed bitmap is reached */
            void DiposeBitmaps(SKBitmap[,] bmps, int failedRow, int failedColumn)
            {
                for (int row = 0; row <= failedRow; row++)  // Dispose through last row              
                    for (int col = 0; col < failedColumn; col++) // Stop before failed bitmap (failed bitmap is null)                       
                        bmps[row, col].Dispose();                                
            }
        }

        public static void DiposeBitmaps(SKBitmap[,] bitmaps)
        {
            int rows = bitmaps.GetLength(0);
            int cols = bitmaps.GetLength(1);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    bitmaps[row, col]?.Dispose();           
        }

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
                        using (var stream = File.OpenWrite($"{fileName}-row{row}-column{col}.png"))
                        {
                            data.SaveTo(stream);
                        }
                    }
                }
        }
    }
}
