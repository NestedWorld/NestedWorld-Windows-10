using NestedWorld.Classes.ElementsGame.Portals;
using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.View.MapPoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.MapViews
{
    public sealed partial class MapControlView : UserControl
    {
        public MapControl mapControl { get { return mC; } set { mC = value; } }
        public MapControlView()
        {
            this.InitializeComponent();
        }

        private void Locate(object sender, RoutedEventArgs e)
        {
            App.core.MapController.Locate();
        }

        public void PortalMapPoint_OnPortalSelected(Portal portal)
        {
            this.PortalView.DataContext = portal;
            this.PortalView.Show();
        }

        public async void focusOn(Portal portal)
        {
            try
            {
                await mapControl.TrySetViewAsync(new Windows.Devices.Geolocation.Geopoint(portal.Location), 10, 0, 0, MapAnimationKind.Bow);
            }
            catch(Exception ex)
            {
                Utils.Log.Error("focusOn", ex);
            }
        }
    }
}
