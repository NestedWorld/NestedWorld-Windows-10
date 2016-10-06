using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NestedWorld.Classes.ElementsGame.Monsters
{

    public class MonsterList
    {
        public List<Monster> monsterList { get; set; }

        public List<Monster> monsterListByType
        {
            get { return monsterList.OrderBy(item => item.Type).ToList(); }
            set { int i = 0; i++; }
        }

        public List<Monster> monsterListByName
        {
            get { return monsterList.OrderBy(item => item.Name).ToList(); }
            set { int i = 0; i++; }
        }
        public List<Monster> monsterListByLevel
        {
            get { return monsterList.OrderBy(item => item.Level).ToList(); }
            set { int i = 0; i++; }
        }
        public List<Monster> monsterListByID
        {
            get { return monsterList.OrderBy(item => item.ID).ToList(); }
            set { int i = 0; i++; }
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

        public void init()
        {

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

        public Monster GetMonsterByID(int ID)
        {
            var querry = from item in monsterList where item.ID == ID select item;

            foreach (Monster monster in querry)
            {
                Debug.WriteLine(monster.Name);
            }

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


        #region Json

        internal static MonsterList GetMonsterListFromJson(JObject jObject, int number = -1)
        {
            if (jObject == null)
                return NewList(number);

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

        public async void loadAttack()
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
        }

        internal static MonsterList GetUserMonsterListFromJson(JObject jObject, int number = -1)
        {
            if (jObject == null)
                return NewListUser(number);

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

        private static MonsterList NewList(int number)
        {
            MonsterList list = new MonsterList();
            for (int i = 0; i < number; i++)
            {
                list.Add(Monster.GetMonster(i));
            }
            return list;
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

            }
        }
    }
}
