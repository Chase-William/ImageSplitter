# ImageSplitter

A simple cross-platform image splitting library. Currently you can only split images into a number of equal sized rows and columns.

## Example Usage

```cs
// Create disposible imagegrid
using var splitter = new ImageGrid("../../../../resources/nature.bmp", rows: 4, columns: 4);
// Save all split parts to file with the name <"myImage"-r<row>-c<column>>
splitter.SaveBitmaps("myImage", SkiaSharp.SKEncodedImageFormat.Png, quality: 55);
```

The `ImageGrid` implements the IDisposible because it contains a 2d array of all the smaller bmp's which are extracted from the original.
