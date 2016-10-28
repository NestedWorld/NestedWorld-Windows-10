using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace NestedWorld.Utils
{
    public class Image
    {
        public StorageFile item { get; set; }
        public BitmapImage bitmap { get; set; }

      
    }

    public class ImageStorage
    {
        private const int RANGE = 50;
        private int index;
        private List<StorageFile> images;

        public ObservableCollection<StorageFile> Content { get { return new ObservableCollection<StorageFile>(images); } set { } }

        public ImageStorage()
        {
            images = new List<StorageFile>();
        }

        private async Task AddFiles(StorageFolder parentFolder)
        {
            var files = await parentFolder.GetFilesAsync().AsTask();

            files = files.OrderBy(e => e.DateCreated).ToList();
            foreach (StorageFile file in files)
            {
                string fileType = file.FileType.ToLowerInvariant();
                switch (fileType)
                {
                    case ".jpg":
                    case ".png":

                        lock (images)
                            images.Add(file);
                        break;
                }
            }


            var childFolders = await parentFolder.GetFoldersAsync();
            foreach (var folder in childFolders)
            {
                await AddFiles(folder);
            }

        }

        public async Task ListImage()
        {
            index = 0;
            await AddFiles(KnownFolders.PicturesLibrary);

           
            Utils.Log.Info("ListImage", "Done");
            //            await AddFiles(KnownFolders.CameraRoll);
        }

        public static async Task<BitmapImage> Open(StorageFile item)
        {
            var stream = await item.OpenReadAsync();
            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(stream);
            return bitmap;
        }
    }
}
