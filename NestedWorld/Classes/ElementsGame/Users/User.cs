using NestedWorld.Classes.Chat;
using NestedWorld.View.MapPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Newtonsoft.Json.Linq;

namespace NestedWorld.Classes.ElementsGame.Users
{
    public class User
    {
      
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Background { get; private set; }
        public int Level { get; private set; }
        public bool IsOnline { get; private set; }
       // public Channel discution { get; set; }

        public UserMapPoint ump { get; set; }
        public Color color
        {
            get
            {
                if (IsOnline)
                    return Utils.ColorUtils.GetColorFromHex("#FFB3E5FC");
                return Utils.ColorUtils.GetColorFromHex("#FFFFFFFF");
            }
            set { int i = 0; i++; }
        }

        public User(User user)
        {
            this.Name = user.Name;
            this.Image = user.Image;
            this.IsOnline = user.IsOnline;
            this.Level = user.Level;
     //       discution = new Channel(user.Name, user.Image, false);
        }

        public User(string name, string image, bool isOnline, int level)
        {
            this.Name = name;
            this.Image = image;
            this.IsOnline = isOnline;
            this.Level = level;
         //   discution = new Channel(name, image, false);
            ump = new UserMapPoint();
            ump.DataContext = this;
        }

        public User(string name, string image, string background, bool isOnline, int level) : this(name, image, isOnline, level)
        {
            Background = background;
        }

        internal static User GetFronJson(JObject obj)
        {


            JObject item = obj["user"].ToObject<JObject>();
            string avatar = item["avatar"].ToObject<string>() == null ? "ms-appx:///Assets/profilDefault.png" : item["avatar"].ToObject<string>();
            string name = item["pseudo"].ToObject<string>();
            string background = item["background"].ToObject<string>() == null ? "ms-appx:///Assets/NestedWorldLogo.png" : item["background"].ToObject<string>();
            int level = item["level"].ToObject<int>();
            bool online = item["is_connected"].ToObject<bool>();
            return new User(name, avatar, background, online, level);
        }
    }
}
