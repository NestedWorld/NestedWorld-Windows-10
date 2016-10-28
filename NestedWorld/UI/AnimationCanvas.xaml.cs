using NestedWorld.Classes.DesignUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.UI
{
    public sealed partial class AnimationCanvas : UserControl
    {
        private const int ROUNDMAX = 10;

        private DispatcherTimer timer;
        private Sprite _sprite;

        private int _index;
        private int _round;

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

        private int round
        {
            get { return _round; }
            set
            {
                _round = value;
                if (_round > ROUNDMAX)
                {
                    ImageEllipse.Visibility = Visibility.Collapsed;
                    timer.Stop();
                }
            }
        }

        public Sprite Sprite
        {
            get
            { return _sprite; }
            set
            {
                _sprite = value;
                Utils.Log.Info(value);
                if (!_sprite.isLoad)
                    _sprite.LoadData();
                _index = 0;
                _round = 0;
                ImageEllipse.Visibility = Visibility.Visible;
                timer.Start();
            }
        }

        public AnimationCanvas()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.075);
            timer.Tick += Timer_Tick;
            ImageEllipse.Visibility = Visibility.Collapsed;
        }

        private void Timer_Tick(object sender, object e)
        {
            if (!Sprite.isLoad)
                return;
            try
            {
                this.ImageEllipse.Fill = this.Sprite[index];
                index++;

                round++;
            }
            catch (Exception ex)
            {
                Utils.Log.Warning("Sprite display :", ex);
            }

           
        }
        
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
