using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;

namespace NestedWorld.Classes.DesignUtilities
{
    public class Sprite
    {
        public bool isLoad { get; private set; }
        private StorageFile file;
        public string imageName { get; set; }
        public string imageFolder { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int imagenumber { get; set; }
        public List<WriteableBitmap> imageList { get; set; }

        public Sprite()
        {
            imageList = new List<WriteableBitmap>();
            isLoad = false;
        }


        public async void Add(int xOffset)
        {
            WriteableBitmap tmp = await file.CropImage(xOffset * height, width, height, width);
            imageList.Add(tmp);
        }

        public void LoadData()
        {
            if (isLoad)
                return;
            try
            {
                for (int index = 0; index < imagenumber; index++)
                {
                    Add(index);
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("LoadData", ex);
            }
            isLoad = true;
        }


        private void UpdateUserPosition(object position)
        {
            throw new NotImplementedException();
        }

        public async void Load(XElement element)
        {
            if (element == null)
                return;
            try
            {
                imagenumber = (int)element.Attribute("imagenumber");
                height = (int)element.Attribute("height");
                width = (int)element.Attribute("width");
                imageName = (string)element.Attribute("file");
                imageFolder = (string)element.Attribute("folder");
                file = await IO.GetFile("Assets", imageFolder, imageName);
            }
            catch (System.Exception ex)
            {
                Log.Warning("Sprite Load", ex);
            }
        }

        public Windows.UI.Xaml.Media.ImageBrush this[int key]
        {
            get
            {
                Windows.UI.Xaml.Media.ImageBrush ib = new Windows.UI.Xaml.Media.ImageBrush();
                ib.ImageSource = this.imageList[key];
                return ib;
            }
            set
            {

            }
        }

        internal static Sprite LoadSprite(XElement element)
        {
            Sprite ret = new Sprite();
            ret.Load(element);
            return ret;
        }
    }
}
