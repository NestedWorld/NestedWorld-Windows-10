using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace NestedWorld.Classes.ElementsGame.Maps
{
    public class AreaList
    {
        private List<Area> _list;

        public List<Area> list { get { return _list; } set { _list = value; } }

        public ObservableCollection<Area> content { get { return new ObservableCollection<Area>(list); } set { int i = 0; i++; } }


        public AreaList()
        {
            list = new List<Area>();
        }

        public void Add(Area newArea)
        {
            list.Add(newArea);
        }

        public void Clean()
        {
            list.Clear();
        }

        public Area GetAreaTaped(BasicGeoposition tappedGeoPosition)
        {

            foreach (Area area in list)
            {
                if (area.PointOnIt(tappedGeoPosition))
                    return area;
            }
            return null;
        }

        public void Init()
        {


            Add(new Area("Guadalajara", "http://allaboutguadalajara.com/wp-content/uploads/Guad_Main.jpg",
                GenerateGeoPoint(
                   new BasicGeoposition
                   {
                       Latitude = 20.733433,
                       Longitude = -103.4582296
                   },
                    new BasicGeoposition
                    {
                        Latitude = 20.681559,
                        Longitude = -103.3247552,
                    },
                      new BasicGeoposition
                      {
                          Latitude = 20.6748909,
                          Longitude = -103.3574124,
                      }
                    ),
                GenerateItemList(new Item.Item("flower", 0, "desc", "ms-appx:///Assets/Flower/flowerFire.png", 0, 0, 0, 0), new Item.Item("flower", 0, "desc", "ms-appx:///Assets/Flower/flowerFire.png", 0, 0, 0, 0), new Item.Item("flower", 0, "desc", "ms-appx:///Assets/Flower/flowerFire.png", 0, 0, 0, 0)),
               MonsterList.GetUserMonsterListFromJson(null, 4)));
        }

        public void Hide(MapControl map)
        {
            try
            {
                foreach (Area a in list)
                {
                    var shape = a.getPolygon();
                    shape.AddData(a);
                    map.MapElements.Remove(shape);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Show(MapControl map)
        {
            try
            {
                foreach (Area a in list)
                {
                    var shape = a.getPolygon();
                    shape.AddData(a);
                    map.MapElements.Add(shape);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        internal static List<BasicGeoposition> GenerateGeoPoint(BasicGeoposition point1, BasicGeoposition point2, BasicGeoposition point3)
        {
            List<BasicGeoposition> ret = new List<BasicGeoposition>();
            ret.Add(point1);
            ret.Add(point2);
            ret.Add(point3);
            return ret;
        }



        internal static List<Item.Item> GenerateItemList(Item.Item item1, Item.Item item2, Item.Item item3)
        {
            List<Item.Item> ret = new List<Item.Item>();
            ret.Add(item1);
            ret.Add(item2);
            ret.Add(item3);

            return ret;
        }

    }
}
