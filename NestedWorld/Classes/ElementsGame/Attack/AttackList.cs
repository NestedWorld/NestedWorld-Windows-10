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
        public Dictionary<int, Attack> list { get; set; }

        public ObservableCollection<Attack> observable { get { return new ObservableCollection<Attack>(list.Values); } set { } }

        public AttackList()
        {
            list = new Dictionary<int, Attack>();
        }

        public void Add(Attack newattack)
        {
            list[newattack.Id] = newattack;
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
            try
            {
                JArray array = Jobject["attacks"].ToObject<JArray>();
                foreach (JObject obj in array)
                {
                    int index = obj["id"].ToObject<int>();
                    Utils.Log.Info("id", index);
                    Attack att = list[index];
                    ret.Add(att);
                }
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("AttackList::NewAttackListFromJson", ex);
            }
            return ret;
        }


        public Attack this[AttackType key]
        {
            get { return Get(key); }
            set { }
        }

        private Attack Get(AttackType key)
        {
            var q = from attack in list.Values where attack.Type == key select attack;

            return q.ToList()[0];
        }


        public override string ToString()
        {
            var ret = "";

            foreach (Attack a in list.Values)
            {
                ret += a.Id + " " + a.Name + " " + a.typeString;
            }

            return ret;
        }
    }
}
