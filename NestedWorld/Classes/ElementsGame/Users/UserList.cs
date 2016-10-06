using NestedWorld.Classes.ElementGame.Chat;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Users
{
    public class UserList
    {
        public List<User> userList { get; set; }

        public int UserOnLineNumber
        {
            get
            {
                var query = from user in userList where user.IsOnline == true select user;
                return query.Count();
            }
            set { }
        }

        public UserList()
        {
            userList = new List<User>();
        }

        public void Init()
        {
            Add(new User("Florian", "https://scontent-lax3-1.xx.fbcdn.net/hphotos-xft1/v/t1.0-9/12020049_669922673138720_4791492090206059614_n.jpg?_nc_eui=ARhHo7VpJA7k5l2Ie_qZC1bamE9BgUr4Rn4q0yJTVjrrHl2fyFJYuw&oh=5d4915b6380fd34cc28b88be21a7702e&oe=574F668D", true, 6));
            Add(new User("Aude", "https://scontent-mia1-1.xx.fbcdn.net/hprofile-xfa1/v/t1.0-1/p160x160/10517566_821896734496741_7072430878313375486_n.jpg?oh=6630f7e9e4fe1cf0167f86fff6b7d9d6&oe=5698DA2F",
                "https://scontent-lax3-1.xx.fbcdn.net/hphotos-xpa1/t31.0-8/1415404_789010594452022_1972957823478929161_o.jpg", false, 6));
            Add(new User("Guillaume", "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/v/t1.0-1/c13.13.166.166/s160x160/536108_3081594322004_602048383_n.jpg?oh=a4222cf7e9007ede5613934e3aeb6287&oe=56C9FD6C&__gda__=1452507807_11b5b9f27eddb23295b98c65d5125368",
                "", true, 2));
            Add(new User("Quentin", "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-xap1/v/t1.0-1/c0.0.160.160/p160x160/10440903_10202384683714824_5624139557794751954_n.jpg?oh=6ca57f63f26bed81a24355af7808aca6&oe=56C983AE&__gda__=1453060368_8207568bb626c39422c6acdea66a8ad1",
                "https://scontent-lax3-1.xx.fbcdn.net/hphotos-xfa1/t31.0-8/10960084_10203871227837498_249910409561681_o.jpg", true, 7));
            Add(new User("Jerome", "https://scontent-mia1-1.xx.fbcdn.net/hprofile-xfp1/v/t1.0-1/c19.0.111.111/1653706_10152231062102863_115429900_n.jpg?oh=cabd07d20d1f2cc455dcc77080acde1f&oe=569A946E",
                "", false, 6));
        }

        public void Init(JObject jObject)
        {
            JArray Array = jObject["friends"].ToObject<JArray>();

            foreach (JObject item in Array)
            {
                Add(User.GetFronJson(item));
            }

        }

        public List<Channel> GetChannelAllies()
        {
            List<Channel> ret = new List<Channel>();
            foreach (var item in userList)
                ret.Add(new Channel(item));
            return ret;
        }

        public void Add(User newUser)
        {
            userList.Add(newUser);
        }

        public static UserList NewUserListOffLine()
        {
            UserList ret = new UserList();

            ret.Init();
            return ret;
        }

        public static UserList NewUserListFromJson(JObject obj)
        {
            UserList ret = new UserList();
            ret.Init(obj);
            return ret;
        }
    }
}
