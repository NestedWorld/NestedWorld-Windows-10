using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class BlurBackground : UserControl
    {
        private CanvasRadialGradientBrush radialBrush;
        private CanvasBitmap image;
        private float _blurAmount;
        private float _blurRadius;
        private SolidColorBrush _background;
        private string _imageSource;

        public SolidColorBrush BackgroundColorBlur
        {
            get { return _background; }
            set { _background = value; setBackgroundToImage(); }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
            }
        }

        public float BlurAmount
        {
            get { return _blurAmount; }
            set { _blurAmount = value; update(); }
        }

        public float BlurRadius
        {
            get { return _blurRadius; }
            set { _blurRadius = value; update(); }
        }

        private void update()
        {

        }

        private void setBackgroundToImage()
        {
            image.SetPixelColors(_background.ToArrayColor((int)canvasControl.Height, (int)canvasControl.Width));
        }

        private async void loadImage()
        {
            image = await CanvasBitmap.LoadAsync(canvasControl, new Uri(ImageSource));
        }

        public BlurBackground()
        {
            this.InitializeComponent();
            _blurRadius = 0;
            _blurAmount = 40;

            this.DataContext = this;
        }

        private void canvasControl_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            radialBrush = new CanvasRadialGradientBrush(sender, Colors.Transparent, BackgroundColorBlur.Color);
            args.TrackAsyncAction(Task.Run(async () =>
            {
                if (ImageSource != null)
                    image = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/NestedWorldLogo.png"));
            }).AsAsyncAction());
        }

        private void canvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            var session = args.DrawingSession;
            args.DrawingSession.Clear(Colors.White);
            radialBrush.Center = new System.Numerics.Vector2((float)(image.Size.Width / 2.0f), (float)(image.Size.Height / 2.0f));
            radialBrush.RadiusX = radialBrush.RadiusY = BlurRadius;
            session.DrawImage(image, image.Bounds);
            using (session.CreateLayer(radialBrush))
            {
                using (var blurEffect = new GaussianBlurEffect())
                {
                    blurEffect.Source = image;
                    blurEffect.BlurAmount = BlurAmount;
                    blurEffect.Optimization = EffectOptimization.Quality;
                    blurEffect.BorderMode = EffectBorderMode.Hard;
                    session.DrawImage(blurEffect, 0, 0);
                }
            }
        }
    }
}
