using Xunit;

using SkiaSharp;

using ImageSplitter;
using System;

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

        [Theory(DisplayName = "ImageGrid Initialization Failure")]
        [InlineData(TEST_FILE_LOC, -1, 4)]
        [InlineData(TEST_FILE_LOC, 4, -1)]
        [InlineData(TEST_FILE_LOC, -1, -1)]
        public void TestImageGridInitizationFailure(string fileName, int rows, int columns)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
                using var imgGrid = new ImageGrid(fileName, rows, columns);
            });            
        }

        [Theory(DisplayName = "ImageGrid Saves Bitmaps Correctly")]
        [InlineData(TEST_FILE_LOC, 1, 1)]
        [InlineData(TEST_FILE_LOC, 4, 4)]
        [InlineData(TEST_FILE_LOC, 12, 8)]
        [InlineData(TEST_FILE_LOC, 8, 10)]
        public void TestImageGridSaving(string fileName, int rows, int columns)
        {
            using ImageGrid imgGrid = new ImageGrid(fileName, rows, columns);
            imgGrid.SaveBitmaps("saving-example", SKEncodedImageFormat.Png, 80);
        }        
    }
}
