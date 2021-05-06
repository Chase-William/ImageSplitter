using System;
using Xunit;

using SkiaSharp;

using ImageSplitter;

namespace ImageSplitterTest
{
    public class ImageGridTest
    {
        /// <summary>
        /// BMP file to be used for testing.
        /// </summary>
        const string TEST_FILE_LOC = "../../../../resources/nature.bmp";

        [Theory(DisplayName = "ImageGrid Initializes Correctly")]
        [InlineData(TEST_FILE_LOC, 4, 4)]
        public void TestImageGridInitialization(string fileName, int rows, int columns)
        {
            using ImageGrid imgGrid = new ImageGrid(fileName, rows, columns);           
        }    

        [Theory(DisplayName = "ImageGrid Saves Bitmaps Correctly")]
        [InlineData(TEST_FILE_LOC, 4, 4)]
        public void TestImageGridSaving(string fileName, int rows, int columns)
        {
            using ImageGrid imgGrid = new ImageGrid(fileName, rows, columns);
            imgGrid.SaveBitmaps("saving-example", SKEncodedImageFormat.Png, 80);
        }
    }
}
