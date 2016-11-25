using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.View.MapPoint;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace NestedWorld.Classes.ElementsGame.Maps
{
    public class MapController
    {
        public delegate void UserPositionChanged(Geoposition position);

        public event UserPositionChanged OnUserPositionChanged;

        private Geolocator geolocator;
        private bool _geoActiv;

        public bool GeolocatoreActivated
        {
            get { return _geoActiv; }
            set
            {
                _geoActiv = value;
                if (value && canUse)
                {
                    //active;
                    geolocator.PositionChanged += Geolocator_PositionChanged;
                }
                else
                {
                    //disable;
                    geolocator.PositionChanged -= Geolocator_PositionChanged;
                }
            }
        }

        public MapControl MapControl
        {
            get { return _mapControl; }
            set
            {
                if (_mapControl != null)
                    _mapControl.Children.Clear();
                _mapControl = value;
                if (value != null)
                    _mapControl.MapElementClick += MapControl_MapElementClick;
            }
        }

        private void MapControl_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var area = App.core.areaList.GetAreaTaped(args.Location.Position);

            if (area != null)
            {
                Debug.WriteLine(area.Name);
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(Pages.AreaPage), area);
            }
            else
                Debug.WriteLine("NONE");
        }

        private bool canUse;
        private MapControl _mapControl;
        private CoreDispatcher dispatcher;
        public PlayerMapPoint pmp;

        public MapController(bool canUse = true)
        {
            this.canUse = canUse;

            geolocator = new Geolocator();
            geolocator.MovementThreshold = 10;
            geolocator.DesiredAccuracy = PositionAccuracy.Default;
            _mapControl = null;
            pmp = new PlayerMapPoint();
            pmp.DataContext = App.core.user;

            GeolocatoreActivated = false;
            dispatcher = Window.Current.Dispatcher;
        }

        private void Init()
        {

        }

        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (!GeolocatoreActivated)
                return;
            try
            {
                if (dispatcher == null)
                    return;
                await dispatcher.RunAsync(CoreDispatcherPriority.Low, new DispatchedHandler(
                  () =>
                  {
                      UpdateUserPosition(args.Position);
                      this.OnUserPositionChanged?.Invoke(args.Position);
                  }));
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("Geolocator_PositionChanged :" + ex.ToString());
            }
        }

        private async void UpdateUserPosition(Geoposition position, bool setView = false)
        {
            if (!MapControl.Children.Contains(pmp))
                MapControl.Children.Add(pmp);
            MapControl.SetLocation(pmp, position.Coordinate.Point);
            if (setView)
                await MapControl.TrySetViewAsync(position.Coordinate.Point, MapControl.ZoomLevel, 0, 0, MapAnimationKind.Linear);
        }

        public async Task<Geoposition> GetUserPosition()
        {
            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            return geoposition;
        }

        public async void Locate()
        {
            try
            {
                UpdateUserPosition(await GetUserPosition(), true);
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("MapView::Init", ex);
                await new MessageDialog("Please go to Settings > Privacy > location, and allow Nested World to access to your location",
                    "You're not enable Nested World to access to your localisation").ShowAsync();
            }
        }

        #region allieslocation
        public void ShowAllyLocation(MapControl mapControl)
        {
            Random rand = new Random();
            foreach (User user in App.core.userList.userList)
            {

                MapControl.SetLocation(user.ump,
                    new Windows.Devices.Geolocation.Geopoint(
                        new Windows.Devices.Geolocation.BasicGeoposition
                        {
                            Latitude = rand.Next(-89, 89),
                            Longitude = rand.Next(-89, 89)
                        }));
                mapControl.Children.Add(user.ump);
            }
        }
        public void ShowAllyLocation()
        {
            ShowAllyLocation(_mapControl);
        }

        public void ColapseAllyLocation(MapControl mapControl)
        {
            foreach (User user in App.core.userList.userList)
            {
                mapControl.Children.Remove(user.ump);
            }
        }

        public void ColapseAllyLocation()
        {
            ColapseAllyLocation(_mapControl);
        }
        #endregion

        #region MonsterLocation
        public void ShowMonsterLocation(MapControl mapControl)
        {

        }

        public void ShowMonsterLocation()
        {
            ShowMonsterLocation(_mapControl);
        }

        public void ColapseMonsterLocation()
        {
            ColapseMonsterLocation(_mapControl);
        }

        private void ColapseMonsterLocation(MapControl _mapControl)
        {

        }
        #endregion

        #region AreaLocation
        public void ShowAreaLocation(MapControl mapControl)
        {
            App.core.areaList.Show(mapControl);
        }
        public void ShowAreaLocation()
        {
            ShowAreaLocation(_mapControl);
        }

        public void ColapseAreaLocation()
        {
            ColapseAreaLocation(_mapControl);
        }

        private void ColapseAreaLocation(MapControl _mapControl)
        {
            App.core.areaList.Hide(_mapControl);
        }
        #endregion

        internal static async Task<MapController> GetNewMapController()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    return new MapController();
                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("Please go to Settings > Privacy > location, and allow Nested World to access to your location",
                   "You're not enable Nested World to access to your localisation").ShowAsync();
                    break;
                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Some probleme to Nested World to access to your localisation").ShowAsync();
                    break;
            }
            var mapcontrollertmp = new MapController(false);
            mapcontrollertmp.Init();
            return mapcontrollertmp;
        }


    }
}
