using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Attack
{
    public class AttackList
    {
        public List<Attack> list { get; set; }

        public ObservableCollection<Attack> observable { get { return new ObservableCollection<Attack>(list); } set { } }

        public AttackList()
        {
            list = new List<Attack>();
        }

        public void Add(Attack newattack)
        {
            list.Add(newattack);
        }

        public void InitDebug()
        {
            Add(new Attack("Attack", "attack", TypeEnum.FIRE));
            Add(new Attack("Sheild", "def", TypeEnum.WATHER));
            Add(new Attack("electAttack", "electAttack", TypeEnum.ELEC));
            Add(new Attack("heal", "heal", TypeEnum.ELEC));
        }

        public static AttackList NewAttackList()
        {
            var ret = new AttackList();
            ret.InitDebug();
            return ret;
        }

        public static AttackList LoadFromJson(JObject Jobject)
        {
            AttackList ret = new AttackList();

            JArray array = Jobject["attacks"].ToObject<JArray>();

            foreach (JObject obj in array)
            {
                ret.Add(Attack.LoadFromJson(obj));
            }

            return ret;
        }

        public AttackList NewAttackListFromJson(JObject Jobject)
        {
            AttackList ret = new AttackList();

            JArray array = Jobject["attacks"].ToObject<JArray>();

            int i = 0;

            foreach (JObject obj in array)
            {
                Attack att = list[obj["id"].ToObject<int>()];
                ret.Add(att);
                i++;
            }
            return ret;
        }


        public Attack this[AttackType key]
        {
            get { return Get(key); }
            set { }
        }

        public Attack this[int key]
        {
            get { return Get(key); }
            set { }
        }

        private Attack Get(AttackType key)
        {
            var q = from attack in list where attack.Type == key select attack;

            return q.ToList()[0];
        }

        private Attack Get(int key)
        {
            var q = from attack in list where attack.Id == key select attack;

            return q.ToList()[0];
        }
    }
}
