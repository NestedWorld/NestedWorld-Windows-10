using NestedWorld.Classes.ElementsGame.Users;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace NestedWorld.Classes.ElementsGame.Maps
{
    public class Area
    {
        public List<BasicGeoposition> PositionList { get; set; }

        public Geopoint Center { get { return new Geopoint(PositionList[0]); } set { } }

        public string Name { get; set; }

        public string Image { get; set; }



        public List<Item.Item> ItemList { get; set; }
        public Monsters.MonsterList MonsterList { get; set; }

        public UserEnum UserEnum { get; set; }


        public Area(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public Area(string name, string image, List<BasicGeoposition> positionList)
            : this(name, image)
        {
            PositionList = new List<BasicGeoposition>(positionList);
        }

        public Area(string name, string image, List<BasicGeoposition> positionList, List<Item.Item> itemList)
            : this(name, image, positionList)
        {
            ItemList = new List<Item.Item>(itemList);
        }

        public Area(string name, string image, List<BasicGeoposition> positionList, List<Item.Item> itemList, Monsters.MonsterList monsterList)
            : this(name, image, positionList, itemList)
        {
            MonsterList = monsterList;
        }

        public MapPolygon getPolygon()
        {
            MapPolygon ret = new MapPolygon
            {
                StrokeThickness = 3,
                StrokeDashed = true,
                ZIndex = 1,
                FillColor = Utils.ColorUtils.GetColorFromHex("#99424242"),
                StrokeColor = Utils.ColorUtils.GetColorFromHex("#FF424242"),
                Path = new Geopath(PositionList)
            };
            return ret;
        }

        public bool PointOnIt(BasicGeoposition pos)
        {
            return true;
        }
    }
}
