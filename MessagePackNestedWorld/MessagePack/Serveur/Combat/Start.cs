using Network.MessagePack;
using System;

namespace MessagePack.Serveur.Combat
{
    public class Start : ResultBase
    {
        public int combat_id { get; set; }
        private const string WILD = "wild";
        private const string DUEL = "duel";

        public Struct.Monster UserMonster { get; set; }
        #region Oppement

        public Struct.User OppomentUser { get; set; }
        public Struct.Monster OppomentMonster { get; set; }
        public int OppomentMonstersCount { get; set; }

        #endregion
        public string combat_type { get; set; }
        public string combat_env { get; set; }
        public bool first { get; set; }

        public Start()
            : base("combat:start")
        {

        }

        public override void SetValue(ReceiveMessage receiveMessage)
        {

            try
            {
                combat_id = Convert.ToInt32(receiveMessage.GetByte("combat_id"));
                combat_type = receiveMessage.GetString("type");
                combat_env = receiveMessage.GetString("env");
                first = receiveMessage.GetBoolean("first");

                var tmpUserMonster = receiveMessage.GetStruct("user").GetStruct("monster");

                UserMonster = new Struct.Monster()
                {
                    Id = Convert.ToInt32(tmpUserMonster.GetByte("id")),
                    Name = tmpUserMonster.GetString("name"),
                    Monster_Id = Convert.ToInt32(tmpUserMonster.GetByte("monster_id")),
                    user_monster_id = Convert.ToInt32(tmpUserMonster.GetByte("user_monster_id")),
                    Hp = Convert.ToInt32(tmpUserMonster.GetByte("hp")),
                    Level = Convert.ToInt32(tmpUserMonster.GetByte("level")),
                };

                if (combat_type == DUEL)
                {
                    var tmpOppUser = receiveMessage.GetStruct("opponent").GetStruct("user");
                    OppomentUser = new Struct.User()
                    {
                        Id = Convert.ToInt32(tmpOppUser.GetByte("monster_id")),
                        Name = tmpOppUser.GetString("name"),
                    };
                    OppomentMonstersCount = Convert.ToInt32(receiveMessage.GetStruct("oppoment").GetByte("monsters_count"));
                }

                var tmpOppMonster = receiveMessage.GetStruct("opponent").GetStruct("monster");

                OppomentMonster = new Struct.Monster()
                {
                    Id = Convert.ToInt32(tmpOppMonster.GetByte("id")),
                    Name = tmpOppMonster.GetString("name"),
                    Monster_Id = Convert.ToInt32(tmpOppMonster.GetByte("monster_id")),
                    user_monster_id = -1,
                    Hp = Convert.ToInt32(tmpOppMonster.GetByte("hp")),
                    Level = Convert.ToInt32(tmpOppMonster.GetByte("level")),
                };


                onCompled();
                return;
            }
            catch (Exception.NoAttributeFoundException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeBadTypeException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeEmptyException ex)
            {
                OnError(ex);
            }
        }
    }
}
