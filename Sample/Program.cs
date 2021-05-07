using System;

using ImageSplitter;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the ImageSplitter Sample!");
            Console.WriteLine("The image that is being split is called nature.bmp and it resides in a <resource> folder in this repository's root directory.");

            using var splitter = new ImageGrid("../../../../resources/nature.bmp", 4, 4);            
            splitter.SaveBitmaps("example", SkiaSharp.SKEncodedImageFormat.Png, 55);

            Console.WriteLine("Check your bin/** folder for the output.");
            Console.ReadLine();
        }
    }
}
