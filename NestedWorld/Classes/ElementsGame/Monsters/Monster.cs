using NestedWorld.Utils;
using NestedWorld.View.MapPoint;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Media;
using MessagePack.Serveur.Combat.Struct;

namespace NestedWorld.Classes.ElementsGame.Monsters
{
    public class Monster
    {
        public MonsterMapPoint mmp;

        public string Name { get; private set; }
        public TypeEnum Type { get; private set; }
        public Color color { get { return (Utils.ColorUtils.GetColorFromHex(Utils.ColorUtils.GetTypeColor(Type))); } set { } }
        public SolidColorBrush Brush { get { return new SolidColorBrush(color); } set { } }
        public string Image { get; private set; }
        public string ImageBackground { get; private set; }
        public string ImageType { get { return Utils.ImageUtils.GetImageType(Type); } set { } }
        public string Info { get; private set; }

        public bool PlayerMonster { get; set; }
        public int Level { get; private set; }
        public int LifeMax { get; set; }
        public double LifePercent { get { return (Life * 100) / LifeMax; } }

        public int Life { get; set; }
        public int Exp { get; private set; }
        public int Attackeffect { get; private set; }
        public int Defence { get; private set; }
        public string Surname { get; private set; }

        public int ID { get; private set; }
        public int UserID { get; private set; }
        public Attack.AttackList attackList { get; set; }

        public Monster(string name, TypeEnum type, string image, int id, string info)
        {
            this.Name = name;
            this.Type = type;
            this.Image = image;
            this.ID = id;
            mmp = new MonsterMapPoint();
            mmp.DataContext = this;
            Info = info;
            PlayerMonster = false;
            this.attackList = Attack.AttackList.NewAttackList();
            this.ImageBackground = "http://www.intrawallpaper.com/static/images/abstract-mosaic-background.png";
        }

       

        public Monster(int ID, string name, TypeEnum type, string image, string info,
           int level = 0, int live = 0, int exp = 0, int attack = 0, int def = 0, string surname = null, int iduser = 0)
            : this(name, type, image, ID, info)
        {
            if (level != 0)
                this.PlayerMonster = true;
            this.UserID = iduser;
            this.Level = level;
            this.Life = live;
            this.Exp = exp;
            this.Attackeffect = attack;
            this.Defence = def;
            this.Surname = surname;

        }


        internal static Monster GetMonster(JObject jObject)
        {
            try
            {
                return new Monster(jObject["id"].ToObject<int>(),
                    jObject["name"].ToObject<string>(),
                    Utils.RandomValue.GetRandValueType(),
                    "ms-appx:///Assets/default_monster.png",
                    "Monster Description");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("GetMonster : " + ex.ToString());
            }
            return null;

        }

        internal static Monster GetUserMonster(JObject jObject)
        {
            try
            {
                JObject Infos = jObject["infos"].ToObject<JObject>();

                int defense = Infos["defense"].ToObject<int>();
                string name = Infos["name"].ToObject<string>();
                int iduser = Infos["id"].ToObject<int>();
                int hp = Infos["hp"].ToObject<int>();
                int attack = Infos["attack"].ToObject<int>();
                int id = jObject["id"].ToObject<int>();

                int exp = jObject["experience"].ToObject<int>();
                int level = jObject["level"].ToObject<int>();
                string surname = jObject["surname"].ToObject<string>();


                return new Monster(id, name, Utils.RandomValue.GetRandValueType(),
                   "ms-appx:///Assets/default_monster.png",
                    "Monster Description", level, hp, exp, attack, 0, surname);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                Log.Error("GetUserMonster", ex.Message);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("GetMonster : " + ex.ToString());
            }
            return null;
        }

        internal static Monster GetMonster(int ID)
        {
            return new Monster(ID, "Monster " + ID.ToString(), Utils.RandomValue.GetRandValueType(), "ms-appx:///Assets/default_monster.png", "Monster's Info");
        }


        internal static Monster GetUserMonster(int ID)
        {
            return new Monster(ID, "Monster " + ID.ToString(), Utils.RandomValue.GetRandValueType(), "ms-appx:///Assets/default_monster.png", "Monster's Info",
                Utils.RandomValue.GetRandValue(0, 50),
                Utils.RandomValue.GetRandValue(0, 50),
                Utils.RandomValue.GetRandValue(0, 50),
                Utils.RandomValue.GetRandValue(0, 50),
                Utils.RandomValue.GetRandValue(0, 50),
                "Monster " + ID.ToString() + "'s surname");
        }
        public Monster()
        { }

        public Monster FromStruct(MessagePack.Serveur.Combat.Struct.Monster oppomentMonster)
        {
//            this.Surname = oppomentMonster.Name;
            this.Life = oppomentMonster.Hp;
            this.Level = oppomentMonster.Level;

            return this;
        }

    }
}
