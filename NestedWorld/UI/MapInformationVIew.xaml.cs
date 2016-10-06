using NestedWorld.View.MapPoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236


#pragma warning disable CS4014

namespace NestedWorld.UI
{
    public sealed partial class MapInformationVIew : UserControl
    {
        private bool _mapShow;
        private Geopoint _point;
        private MapInformationPoint _pip;

        public MapInformationPoint pip { get { return _pip; } set { mapControl.Children.Add(value); _pip = value; } }
        public bool mapShow
        {
            get { return _mapShow; }
            set
            {
                _mapShow = value;
                if (value)
                {
                    mapControl.Visibility = Visibility.Visible;
                    InformationBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    mapControl.Visibility = Visibility.Collapsed;
                    InformationBlock.Visibility = Visibility.Visible;
                }
            }
        }

        public Geopoint point
        {
            get { return _point; }
            set
            {
                _point = value;
                MapControl.SetLocation(_pip, value);
                UpdateAddress();
            }
        }

        private async void UpdateAddress()
        {
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(_point);

            string res = "";
            try
            {
                res = result.Locations[0].Address.StreetNumber + " " + result.Locations[0].Address.Street + ", " + result.Locations[0].Address.Town;
                Debug.WriteLine(res);
                PlaceName.Text = res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            res = _point.Position.Latitude + ", " + _point.Position.Longitude;
            await mapControl.TrySetViewAsync(_point, 15, 0, 0, MapAnimationKind.Linear);

        }

        public MapInformationVIew()
        {
            this.InitializeComponent();
            mapShow = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mapShow = !mapShow;
        }


    }
}
