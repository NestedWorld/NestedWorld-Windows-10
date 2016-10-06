using NestedWorld.Classes.DesignUtilities;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.UI
{
    public sealed partial class SpriteAnimation : UserControl
    {
        private int _index;
        public int index
        {
            get { return _index; }
            set
            {
                _index = value;
                if (_index >= Sprite.imageList.Count)
                    _index = 0;
            }
        }

        private DispatcherTimer timer;
        public Sprite Sprite { get; private set; }

        public SpriteAnimation()
        {
            this.InitializeComponent();
            this.DataContextChanged += SpriteAnimation_DataContextChanged;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.075);
            timer.Tick += Timer_Tick;

        }

        private void init()
        {
            if (!Sprite.isLoad)
                Sprite.LoadData();
            _index = 0;
            timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (!Sprite.isLoad)
                return;
            try
            {
                RenduImage.Source = Sprite.imageList[index];
                index++;
            }
            catch (Exception ex)
            {
                Log.Warning("Sprite display :", ex);
            }
        }

        private void SpriteAnimation_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.DataContext == null)
                return;
            this.Sprite = this.DataContext as Sprite;
            init();
        }
    }
}
