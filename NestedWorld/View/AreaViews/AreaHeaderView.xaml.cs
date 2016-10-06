using NestedWorld.Classes.ElementsGame.Maps;
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
using NestedWorld.Utils;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.AreaViews
{
    public sealed partial class AreaHeaderView : UserControl
    {
        public AreaHeaderView()
        {
            this.InitializeComponent();
            this.DataContextChanged += AreaHeaderView_DataContextChanged;
        }

        private async void AreaHeaderView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Area area = DataContext as Area;

            var shape = area.getPolygon();
            shape.AddData(area);
            MapControl.MapElements.Add(shape);
            await MapControl.TrySetViewAsync(area.Center, 10, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);

        }
    }
}
