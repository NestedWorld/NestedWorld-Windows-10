using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace NestedWorld.Classes.ElementsGame.Maps
{
    public class Place
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public BasicGeoposition Location
        {
            get
            {
                return new BasicGeoposition()
                {
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                };
            }
            set { }
        }


    }
}
