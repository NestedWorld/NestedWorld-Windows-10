using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace NestedWorld.Utils
{
    class IO
    {
        private const string TMPFILE = "tmpfile";

        public static async Task<StorageFile> GetFile(string folder, string file)
        {
            StorageFolder storageFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(folder);
            StorageFile storageFile = await storageFolder.GetFileAsync(file);
            return storageFile;
        }

        public static async Task<StorageFile> GetFile(string folder, string subfolder, string file)
        {
            StorageFolder storageFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(folder);
            StorageFolder storageSubFolder = await storageFolder.GetFolderAsync(subfolder);
            StorageFile storageFile = await storageSubFolder.GetFileAsync(file);
            return storageFile;
        }

        public static BitmapImage GetBitmapImage(string url)
        {
            try
            {
                return new BitmapImage(new Uri(url));
            }
            catch (Exception ex)
            {
                Log.Error("Execption", ex);
            }
            return null;
        }

        public static async Task<Windows.Data.Xml.Dom.XmlDocument> LoadXmlFile(String folder, String file)
        {
            Windows.Storage.StorageFolder storageFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(folder);
            Windows.Storage.StorageFile storageFile = await storageFolder.GetFileAsync(file);
            Windows.Data.Xml.Dom.XmlLoadSettings loadSettings = new Windows.Data.Xml.Dom.XmlLoadSettings();
            loadSettings.ProhibitDtd = false;
            loadSettings.ResolveExternals = false;
            return await Windows.Data.Xml.Dom.XmlDocument.LoadFromFileAsync(storageFile, loadSettings);
        }

        public static async Task<string> GetDataAsString(String folder, String file)
        {
            Windows.Storage.StorageFolder storageFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(folder);
            Windows.Storage.StorageFile storageFile = await storageFolder.GetFileAsync(file);
            string contents = "";
            try
            {
                using (IRandomAccessStream textStream = await storageFile.OpenReadAsync())
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);
                        contents = textReader.ReadString(textLength);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetDataAsString : " + ex);
            }
            return contents;
        }

        public static async Task<IRandomAccessStream> GetDataAsStream(String folder, String file)
        {
            Windows.Storage.StorageFolder storageFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(folder);
            Windows.Storage.StorageFile storageFile = await storageFolder.GetFileAsync(file);
            IRandomAccessStream contents = null;
            try
            {
                contents = await storageFile.OpenReadAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetDataAsString : " + ex);
            }
            return contents;
        }

        public static async Task<Byte[]> ReadFile(StorageFile file)
        {
            Byte[] fileBytes = null;

            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            return fileBytes;
        }

        public static async Task<StorageFile> SaveFile(StorageFile file, string filename, CreationCollisionOption option)
        {
            try
            {
                StorageFile save = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename + file.FileType, option);

                using (Stream writeStream = await save.OpenStreamForWriteAsync())
                {
                    Byte[] fileByte = await ReadFile(file);
                    DataContractSerializer saveSerialiser = new DataContractSerializer(typeof(Byte[]));

                    saveSerialiser.WriteObject(writeStream, fileByte);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                    return save;
                }
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error(ex);
                return null;
            }
        }



    }
}
