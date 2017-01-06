using NestedWorld.Classes.ElementsGame.Portals;
using NestedWorld.View.MapViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View
{
    public sealed partial class MapView : UserControl
    {
        public MapView()
        {
            this.InitializeComponent();
            App.core.MapController.MapControl = mapControlView.mapControl;
            App.core.MapController.GeolocatoreActivated = true;
            App.core.MapController.OnUserPositionChanged += MapController_OnUserPositionChanged;

        }

        private void MapController_OnUserPositionChanged(Windows.Devices.Geolocation.Geoposition position)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            DisplayPortal(position.Coordinate.Latitude, position.Coordinate.Longitude);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private async void DisplayPortal(double latitude, double longitude)
        {
            App.core.PortalList?.HideOnMap(App.core.MapController, mapControlView.PortalMapPoint_OnPortalSelected);
            App.core.PortalList = new Classes.ElementsGame.Portals.PortalList();
            var ret = await App.network.GetPortals(latitude, longitude);
            ret.ShowErrorOnApp();
            App.core.PortalList = ret.Content as PortalList;
            if (App.core.PortalList != null)
            {
                this.PortalListView.DataContext = App.core.PortalList;
                this.PortalListView.OnPortalSelected += mapControlView.focusOn;
                App.core.PortalList.DisplayOnMap(App.core.MapController, mapControlView.PortalMapPoint_OnPortalSelected);
            }
        }

        public async void Init()
        {
            try
            {
                var locate = await App.core.MapController.GetUserPosition();
                mapControlView.CenterUser(locate);
#pragma warning disable CS0618 // Type or member is obsolete
                DisplayPortal(locate.Coordinate.Latitude, locate.Coordinate.Longitude);
#pragma warning restore CS0618 // Type or member is obsolete

            }
            catch (Exception ex)
            {
                Utils.Log.Error("MapView::Init", ex);
                await new MessageDialog("Please go to Settings > Privacy > location, and allow Nested World to access to your location",
                    "You're not enable Nested World to access to your localisation").ShowAsync();
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitViewOption.IsPaneOpen = !splitViewOption.IsPaneOpen;
        }
    }
}
