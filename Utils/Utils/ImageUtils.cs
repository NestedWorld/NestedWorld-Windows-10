using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace NestedWorld.Utils
{
    public static class ImageUtils
    {

        public static async Task<WriteableBitmap> CropImage(this StorageFile source, int xOffSet, int yOffSet, int height, int width)
        {
            Point start = new Point(xOffSet, yOffSet);
            Size size = new Size(height, width);

            WriteableBitmap wrb = await CropBitmap.GetCroppedBitmapAsync(source, start, size, 1);
           

            return wrb;
        }

        public static async Task<byte[]> getBytes(this WriteableBitmap wb)
        {
            byte[] pixels;
            using (Stream stream = wb.PixelBuffer.AsStream())
            {
                pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);
            }
            return pixels;
        }


        public static async Task<BitmapImage> LoadFromBytes(this BitmapImage image, Byte[] data)
        {
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(data.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            return image;
        }


        internal static async Task<StorageFile> ImageOpenPicker()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            return await openPicker.PickSingleFileAsync();
        }

      
    }
}