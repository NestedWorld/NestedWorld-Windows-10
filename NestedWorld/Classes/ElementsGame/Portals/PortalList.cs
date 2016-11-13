using NestedWorld.Classes.ElementsGame.Maps;
using NestedWorld.View.MapPoint;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace NestedWorld.Classes.ElementsGame.Portals
{
    public class PortalList
    {
        private Dictionary<int, Portal> _portalMap;

        public ObservableCollection<Portal> content { get { return new ObservableCollection<Portal>(_portalMap.Values); } set { } }

        public PortalList()
        {
            this._portalMap = new Dictionary<int, Portal>();
        }

        public void Add(Portal newPortal)
        {
            _portalMap[newPortal.ID] = newPortal;
        }

        public Portal Get(int ID)
        {
            Portal value = null;

            if (_portalMap.TryGetValue(ID, out value))
                return value;
            return null;
        }

        public void DisplayOnMap(MapController mapController, PortalMapPoint.PortalSelected portalMapPoint_OnPortalSelected)
        {
            foreach (Portal p in _portalMap.Values)
            {
                p.pmp.OnPortalSelected += portalMapPoint_OnPortalSelected;
                mapController.MapControl.Children.Add(p.pmp);
            }
        }

        public void HideOnMap(MapController mapController, PortalMapPoint.PortalSelected portalMapPoint_OnPortalSelected)
        {
            foreach (Portal p in _portalMap.Values)
            {
                p.pmp.OnPortalSelected -= portalMapPoint_OnPortalSelected;
                mapController.MapControl.Children.Remove(p.pmp);
            }
        }

        public Portal this[int id]
        {
            get
            {
                return Get(id);
            }
            set
            {
                Add(value);
            }
        }


        public static PortalList LoadJson(JObject obj)
        {
            PortalList ret = new PortalList();

            JArray array = obj["portals"].ToObject<JArray>();


            int i = 0;
            foreach (JObject JPortal in array)
            {
                ret.Add(Portal.LoadJson(JPortal));
                if (i > 20)
                    break;
                i++;
            }
            return ret;
        }

      
    }
}
