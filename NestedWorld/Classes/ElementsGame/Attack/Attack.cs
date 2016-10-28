using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NestedWorld.Classes.ElementsGame.Attack
{
    public enum AttackType
    {
        ATTACK,
        DEF,
        ATTACKSPE,
        DEFSPE,
    }

    public class Attack
    {
        #region Prop
        public string Name { get; set; }
        public string AttackRessourcesName
        {
            get
            {
                switch (Type)
                {
                    case (AttackType.ATTACK):
                        return "attack";
                    case (AttackType.ATTACKSPE):
                        return "attack";
                    case (AttackType.DEF):
                        return "def";
                    case (AttackType.DEFSPE):
                        return "def";
                }
                return "";
            }
            set { }
        }

        public int Id { get; set; }

        public AttackType Type { get; set; }

        public string typeString
        {
            get
            {
                switch (Type)
                {
                    case (AttackType.ATTACK):
                        return "attack";
                    case (AttackType.ATTACKSPE):
                        return "attack special";
                    case (AttackType.DEF):
                        return "defense";
                    case (AttackType.DEFSPE):
                        return "defense special";
                }
                return "";
            }
            set { }
        }

        #endregion

        public Attack(string name, string resource, TypeEnum type)
        {
            this.Name = name;
            this.AttackRessourcesName = resource;
            //this.Type = type;
        }

        public Attack() { }

        internal static Attack LoadFromJson(JObject obj)
        {
            string typeS = obj["type"].ToObject<string>();
            string name = obj["name"].ToObject<string>();
            AttackType type = AttackType.ATTACK;

            #region switch type
            switch (typeS)
            {
                case ("attacksp"):
                    type = AttackType.ATTACKSPE;
                    break;
                case ("attack"):
                    type = AttackType.ATTACK;
                    break;
                case ("defensesp"):
                    type = AttackType.DEFSPE;
                    break;
                case ("defense"):
                    type = AttackType.DEF;
                    break;
            }
            #endregion

            int id = obj["id"].ToObject<int>();

            return new Attack()
            {
                Name = name,
                Type = type,
                Id = id
            };
        }
    }

}
