using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace NestedWorld.Style
{

    public sealed partial class TabHeader : UserControl
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("dataIcon", typeof(Geometry), typeof(TabHeader), null);
        public static readonly DependencyProperty backgroundProperty = DependencyProperty.Register("background", typeof(SolidColorBrush), typeof(TabHeader), null);
        private bool _isSelect;

        public Geometry Icon
        {
            get { return GetValue(IconProperty) as Geometry; }
            set { SetValue(IconProperty, value); }
        }

        public bool isSelect
        {
            get { return _isSelect; }
            set
            {
                _isSelect = value;
                SetValue(backgroundProperty, new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF616668")));
                if (value)
                    SetValue(backgroundProperty, new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")));
             }
        }

        public TabHeader()
        {
            this.InitializeComponent();
            this.DataContext = this;
            Window.Current.SizeChanged += Current_SizeChanged;
            SetSize(Window.Current.Bounds.Width);
        }

        private void SetSize(double width)
        {
            this.Width = width / 5.0;
            
              
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetSize(e.Size.Width);
        }
    }
}
