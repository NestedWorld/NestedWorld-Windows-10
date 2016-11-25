using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace NestedWorld.Classes.ElementsGame.Monsters
{

    public class MonsterList
    {
        public List<Monster> monsterList { get; set; }

        public ObservableCollection<Monster> content
        {
            get
            {
                return new ObservableCollection<Monster>(monsterList);
            }
            set
            {

            }
        }


        public MonsterList()
        {
            monsterList = new List<Monster>();
        }

        public MonsterList(List<Monster> q)
        {
            monsterList = q;
        }

        public List<Monster> SearchMonster(string search)
        {
            var q = from item in monsterList where item.Name.ToUpper().Contains(search.ToUpper()) select item;
            return q.ToList();
        }


        public void Add(Monster newMonster)
        {
            if (newMonster == null)
            {
                Debug.WriteLine("null monster");
                return;
            }
            monsterList?.Add(newMonster);
        }

        public void Add(Monster newMonster, int index)
        {
            if (index >= monsterList.Count)
            {
                monsterList.Add(newMonster);
            }
            else
            {
                monsterList[index] = newMonster;
            }
        }

        public Monster GetMonsterByID(int ID)
        {
            var querry = from item in monsterList where item.ID == ID select item;

            if (querry.Count() != 0)
                return querry.ElementAt(0);
            return null;
        }

        private int GetStat(TypeEnum type)
        {
            var q = from item in monsterList where item.Type == type select item;
            return q.Count();
        }

        public MonsterList GetUserMonsters()
        {
            var q = from item in monsterList where item.PlayerMonster == true select item;
            return new MonsterList(q.ToList());
        }

        public async void loadAttack()
        {
            foreach (Monster m in monsterList)
            {
                if (m == null)
                    break;
                var ret = await App.network.GetMonsterAttack(m.ID);

                if (ret.ErrorCode == 0)
                {
                    m.attackList = App.core.attackList.NewAttackListFromJson(ret.Content as JObject);
                    if (m.attackList == null)
                        Utils.Log.Info("null");
                }
                ret.ShowError();
            }
        }

        public async Task<int> loadAttackAsync()
        {
            foreach (Monster m in monsterList)
            {
                if (m == null)
                    break;
                var ret = await App.network.GetMonsterAttack(m.ID);

                if (ret.ErrorCode == 0)
                    m.attackList = App.core.attackList.NewAttackListFromJson(ret.Content as JObject);
                ret.ShowError();
            }
            return 1;
        }


        #region Json

        internal static MonsterList GetMonsterListFromJson(JObject jObject, int number = -1)
        {
            MonsterList ret = new MonsterList();
            try
            {
                JArray Array = jObject["monsters"].ToObject<JArray>();

                int i = 0;
                foreach (JObject monster in Array)
                {
                    if (i == number)
                        break;
                    ret.Add(Monster.GetMonster(monster));
                    i++;
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("GetMonsterListFromJson :" + ex.ToString());
            }
            return ret;
        }



        internal static MonsterList GetUserMonsterListFromJson(JObject jObject, int number = -1)
        {
            MonsterList ret = new MonsterList();
            try
            {
                JArray Array = jObject["monsters"].ToObject<JArray>();

                int i = 0;
                foreach (JObject monster in Array)
                {
                    if (i == number)
                        break;
                    ret.Add(Monster.GetUserMonster(monster));
                    i++;
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("GetMonsterListFromJson :" + ex.ToString());
            }
            return ret;
        }

        private static MonsterList NewListUser(int number)
        {
            MonsterList list = new MonsterList();
            for (int i = 0; i < number; i++)
            {
                list.Add(Monster.GetUserMonster(i));
            }
            return list;
        }
        #endregion

        private Monster Get(int id)
        {
            var query = from monster in monsterList where monster.ID == id select monster;

            if (query.Count() != 1)
                return null;
            return query.ToList()[0];
        }


        public Monster this[int id]
        {
            get
            {
                return Get(id);
            }
            set
            {
                this.monsterList[id] = value;
            }
        }

        public int[] idarray
        {
            get
            {
                return getIds();
            }
            set { }
        }

        private int[] getIds()
        {
            int[] ids = new int[4] { 0, 0, 0, 0 };

            int i = 0;
            foreach (var m in this.monsterList)
            {
                if (m != null)
                    ids[i] = m.UserID;
                i++;
            }

            return ids;
        }
    }
}
