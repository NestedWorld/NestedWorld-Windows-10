using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.View.MapPoint;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace NestedWorld.Classes.ElementsGame.Portals
{
    public enum PortalState
    {
        available,
        otherMonster,
        userMonster
    }

    public class Portal
    {
        public static DateTime DEFFAULTDATETIME = DateTime.ParseExact("2000-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        public Monster monster { get; set; }

        public DateTime catchEnd { get; set;}

        public int ID { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public PortalState state { get; set; }
        public BasicGeoposition Location
        {
            get
            {
                return new BasicGeoposition()
                {
                    Latitude = this.latitude,
                    Longitude = this.longitude
                };
            }
            set { }
        }
        public TypeEnum type;
        private PortalMapPoint _pmp;
        public Color color { get { return (Utils.ColorUtils.GetColorFromHex(Utils.ColorUtils.GetTypeColor(type))); } set { } }
        public Color stateColor
        {
            get
            {
                switch (state)
                {
                    case (PortalState.available):
                        return Utils.ColorUtils.GetColorFromHex("#FFFFFFFF");
                    case (PortalState.otherMonster):
                        return Utils.ColorUtils.GetColorFromHex("#FF616668");
                    case (PortalState.userMonster):
                        return Utils.ColorUtils.GetColorFromHex("#FF1C1C25");
                }
                return Utils.ColorUtils.GetColorFromHex("#FFFFFFFF");
            }
            set { }
        }
        public string Name { get; set; }

        public PortalMapPoint pmp
        {
            get
            {
                MapControl.SetLocation(_pmp, new Geopoint(Location));
                _pmp.DataContext = this;
                return _pmp;
            }
            set
            {
                _pmp = value;
            }
        }

        public string distance
        {
            get;

            set;
        }

        private async Task<double> Distance()
        {
            var coor = await App.core.MapController.GetUserPosition();

            return 0;
        }

        public string Image
        {
            get
            {
                switch (type)
                {
                    case (TypeEnum.DIRT):
                        return "https://s3-eu-west-1.amazonaws.com/nestedworld/Portal/brown.png";
                    case (TypeEnum.FIRE):
                        return "https://s3-eu-west-1.amazonaws.com/nestedworld/Portal/red.png";
                    case (TypeEnum.ELEC):
                        return "https://s3-eu-west-1.amazonaws.com/nestedworld/Portal/yellow.png";
                    case (TypeEnum.GRASS):
                        return "https://s3-eu-west-1.amazonaws.com/nestedworld/Portal/green.png";
                    case (TypeEnum.WATHER):
                        return "https://s3-eu-west-1.amazonaws.com/nestedworld/Portal/blue.png";
                }
                return "";
            }
            set { }
        }

        public static Portal NewPortal(int Id, double longitude, double latitude, TypeEnum type, string name, string distance, DateTime end)
        {
            return new Portal()
            {
                ID = Id,
                longitude = longitude,
                latitude = latitude,
                type = type,
                pmp = new PortalMapPoint(),
                Name = name,
                distance = distance,
                catchEnd = end
            };
        }

        public static Portal LoadJson(JObject obj)
        {
            try
            {
                int id = obj["id"].ToObject<int>();
                double longitude = obj["position"].ToObject<double[]>()[0];
                double latitude = obj["position"].ToObject<double[]>()[1];
                TypeEnum type = TypeEnum.WATHER;
                switch (obj["type"].ToObject<string>())
                {
                    case ("fire"):
                        type = TypeEnum.FIRE;
                        break;
                    case ("plant"):
                        type = TypeEnum.GRASS;
                        break;
                    case ("electric"):
                        type = TypeEnum.ELEC;
                        break;
                    case ("water"):
                        type = TypeEnum.WATHER;
                        break;
                    case ("earth"):
                        type = TypeEnum.DIRT;
                        break;
                }
                string name = obj["name"].ToObject<string>();
                string distance = (obj["distance"].ToObject<double>() < 1.0 ? "less than 1 m" : obj["distance"].ToObject<double>().ToString() + " m");
                string catching_endTmp = obj["catching_end"].ToObject<string>();

                DateTime catching_end = DEFFAULTDATETIME;
                if (catching_endTmp != null)
                {
                    catching_end = DateTime.ParseExact(catching_endTmp, "", System.Globalization.CultureInfo.InvariantCulture);
                }
           //     obj["catching_end"].ToObject<string>() == null ? null : Convert.ToDateTime(obj["catching_end"].ToObject<string>())) ;
                Utils.Log.Info(catching_endTmp);
                return NewPortal(id, longitude, latitude, type, name, distance, catching_end);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                Utils.Log.Error("Portal::LoadJson", ex);
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("Portal::LoadJson", ex);
            }
            return null;
        }
    }
}
