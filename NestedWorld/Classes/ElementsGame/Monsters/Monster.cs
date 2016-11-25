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
        public string Image { get { if (PlayerMonster) return UserImage; return WildImage; } set { } }
        public string ImageBackground { get; private set; }
        public string ImageType { get { return Utils.ImageUtils.GetImageType(Type); } set { } }
        public string Info { get; private set; }

        public bool PlayerMonster { get; set; }
        public int Level { get; private set; }
        public int LifeMax { get; set; }
       
        public string UserImage { get; set; }
        public string WildImage { get; set; }

        public int Life { get; set; }
        public int Exp { get; private set; }
        public int Attackeffect { get; private set; }
        public int Defence { get; private set; }
        public string Surname { get; private set; }

        public int ID { get; private set; }
        public int UserID { get; private set; }
        public Attack.AttackList attackList { get; set; }


        public bool PlayerAs { get; set; }

        public Monster(Monster source)
        {
            this.Name = source.Name;
            this.Type = source.Type;
            this.Image = source.Image;
            this.ID = source.ID;
            mmp = new MonsterMapPoint();
            mmp.DataContext = this;
            Info = source.Info;
            PlayerMonster = source.PlayerMonster;
            if (source.attackList == null)
                Utils.Log.Info("null");
            this.attackList = source.attackList;
            this.ImageBackground = "http://www.intrawallpaper.com/static/images/abstract-mosaic-background.png";

            if (PlayerMonster)
            {
                this.UserID = source.UserID;
                this.Level = source.Level;
                this.Life = source.Life;
                this.Exp = source.Exp;
                this.Attackeffect = source.Attackeffect;
                this.Defence = source.Defence;
                this.Surname = source.Surname;
            }

        }

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
            PlayerAs = false;
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
            this.LifeMax = live;
        }

        internal static Monster GetMonster(JObject jObject)
        {
            try
            {
                TypeEnum type = TypeEnum.FIRE;

                switch (jObject["type"].ToObject<string>())
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

                string name = jObject["name"].ToObject<string>();
                //string enraged_sprite = "https://s3-eu-west-1.amazonaws.com/nestedworld/Monsters/bad.png";
                string enraged_sprite = jObject["enraged_sprite"].ToObject<string>();
                string base_sprite = jObject["base_sprite"].ToObject<string>();
                int id = jObject["id"].ToObject<int>();
                Monster ret = new Monster()
                {
                    Name = name,
                    ID = id,
                    Type = type,
                    UserImage = base_sprite,
                    WildImage = enraged_sprite
                };

                ret.LoadAttack();
                return ret;
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

                int hp = Infos["hp"].ToObject<int>();
                int attack = Infos["attack"].ToObject<int>();
                int user_id = jObject["id"].ToObject<int>();
                int id = Infos["id"].ToObject<int>();
                int exp = jObject["experience"].ToObject<int>();
                int level = jObject["level"].ToObject<int>();
                string surname = jObject["surname"].ToObject<string>();

                Monster monster = App.core.monsterList[id];
                App.core.monsterList[id].PlayerMonster = true;
                if (monster.attackList == null)
                    Utils.Log.Info("null");

                return new Monster()
                {
                    ID = id,
                    UserID = user_id,
                    Name = monster.Name,
                    UserImage = monster.UserImage,
                    WildImage = monster.WildImage,
                    Type = monster.Type,
                    Level = level,
                    Life = hp,
                    Exp = exp,
                    Attackeffect = attack,
                    Defence = defense,
                    Surname = surname,
                    PlayerMonster = true,
                    LifeMax = hp,
                    attackList = monster.attackList
                };
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
            Monster monsterRet = new Monster(this);
            monsterRet.Life = oppomentMonster.Hp;
            monsterRet.Level = oppomentMonster.Level;
            monsterRet.LifeMax = oppomentMonster.Hp;
            return monsterRet;
        }

        public Monster FromStruct(MessagePack.Serveur.Combat.Struct.AttackMonster attackMonster)
        {
            Monster monsterRet = new Monster(this);

            monsterRet.LifeMax = this.LifeMax;
            monsterRet.Life = attackMonster.Hp;

            return monsterRet;
        }

        public async void LoadAttack()
        {
            var ret = await App.network.GetMonsterAttack(this.ID);

            if (ret.ErrorCode == 0)
                this.attackList = App.core.attackList.NewAttackListFromJson(ret.Content as JObject);
        }

    }
}
