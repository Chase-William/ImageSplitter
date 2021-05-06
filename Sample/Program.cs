using System;
using System.Drawing;
using System.IO;

using ImageDivider;
using SkiaSharp;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string TEST_FILE = "../../../../resources/nature.bmp";

            Console.WriteLine($"Loading test file from resources...");

            //Image img;
            //try
            //{
            //    img = Image.FromFile(TEST_FILE);
            //}
            //catch (Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex);
            //    Console.ReadLine();
            //    return;
            //}

           
            //using MemoryStream memStream = new MemoryStream();
            //img.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
            //DividedImage dImg = new DividedImage(memStream);

            try
            {
                SKBitmap[,] maps = ImgDivider.GetDividedEqually(TEST_FILE, 4, 4);

                Console.WriteLine(maps.Length);

                ImgDivider.SaveBitmaps(maps, "example", SKEncodedImageFormat.Png, 80);

                ImgDivider.DiposeBitmaps(maps);
            }            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            Console.WriteLine("Loaded Image.");
        }
    }
}
