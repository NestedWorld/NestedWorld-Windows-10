using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NestedWorld.Classes.ElementsGame.Monsters;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace NestedWorld.Classes.ElementsGame.Exchanges
{
    public class Exchange
    {
        private Monster _userMonsterSend;

        public Monsters.Monster UserMonsterSend
        {
            get
            {
                return _userMonsterSend;
            }
            set
            {
                this.MonsterIdSend = value.ID;
                this._userMonsterSend = value;
            }
        }
        public int MonsterIdAsk { get;  set; }
        public int MonsterIdSend { get;  set; }

        public int Id { get;  set; }

        //monster ask by the user who make the exchange
        public Monsters.Monster MonsterAsk
        {
            get { return App.core.monsterList[MonsterIdAsk]; }
            set
            {
                this.MonsterIdAsk = value.ID;
            }
        }
        //monster send by the user who make the exchange
        public bool CanEchange
        {
            get { return App.core.monsterUserList.Get(this.MonsterIdAsk) != null; }
            set { }
        }

        public Visibility Visible
        {
            get
            {
                return this.CanEchange ? Visibility.Collapsed : Visibility.Visible;
            }
            set { }
        }

        public Monsters.Monster MonsterSend { get { return App.core.monsterList[MonsterIdSend]; } set { this.MonsterIdSend = value.ID; } }

        public static Exchange LoadJson(JObject obj)
        {
            int ask = obj["monster_asked"].ToObject<int>();
            int send = obj["monster_sended"].ToObject<int>();
            int id = obj["id"].ToObject<int>();

            return new Exchange()
            {
                Id = id,
                MonsterIdAsk = ask,
                MonsterIdSend = send,
            };
        }
    }
}
