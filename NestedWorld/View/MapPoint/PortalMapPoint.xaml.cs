using NestedWorld.Classes.ElementsGame.Portals;
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

namespace NestedWorld.View.MapPoint
{
    public sealed partial class PortalMapPoint : UserControl
    {
        public delegate void PortalSelected(Portal portal);

        public event PortalSelected OnPortalSelected;

        public PortalMapPoint()
        {
            this.InitializeComponent();
            
        }

        public PortalMapPoint(Portal data)
        {
            this.InitializeComponent();
            this.DataContext = data;
        }

        private void PortalMapPoint_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext == null)
                return;

            Portal portal = (DataContext as Portal);
            
             
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                OnPortalSelected?.Invoke((DataContext as Portal));
            }
            catch(Exception ex)
            {
                Utils.Log.Error("PortalMapPoint::Grid_tapped", ex);
            }
        }
    }
}
