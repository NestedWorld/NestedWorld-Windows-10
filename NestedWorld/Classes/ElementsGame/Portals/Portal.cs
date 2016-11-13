using NestedWorld.View.MapPoint;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
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

        public Task<double> distance
        {
            get
            {
                return Distance();
            }
            set { }
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


        public static Portal NewPortal(int Id, double longitude, double latitude, TypeEnum type)
        {
            return new Portal()
            {
                ID = Id,
                longitude = longitude,
                latitude = latitude,
                type = type,
                pmp = new PortalMapPoint()
            };
        }

        public static Portal LoadJson(JObject obj)
        {
            string url = obj["url"].ToObject<string>();
            string[] urls = url.Split('/');

            int id = Convert.ToInt32(urls[4]);
            double longitude = obj["position"].ToObject<int[]>()[0];
            double latitude = obj["position"].ToObject<int[]>()[1];
            TypeEnum type = TypeEnum.WATHER;

            return NewPortal(id, longitude, latitude, type);
        }
    }
}
