# ImageSplitter

A simple cross-platform image splitting library. Currently you can only split images into a number of equal sized rows and columns.

## Dependencies:

- <a href="https://github.com/mono/SkiaSharp/releases/tag/v2.80.2">SkiaSharp v2.80.2</a>

## Example Usage

```cs
// Create disposible imagegrid
using var splitter = new ImageGrid("yourInputFileNameHere", rows: 4, columns: 4);
// Save all split parts to file with the name <"saveName"-r<row>-c<column>>
splitter.SaveBitmaps("saveName", SkiaSharp.SKEncodedImageFormat.Png, quality: 55);
```

The `ImageGrid` class implements the `IDisposible` interface because it contains a 2d array of all the smaller bmp's which are extracted from the original. These bitmaps themselves implement the `IDisposible` interface and must be clean-up.
