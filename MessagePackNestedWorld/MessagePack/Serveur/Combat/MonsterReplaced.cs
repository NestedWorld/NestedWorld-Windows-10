using MessagePack.Serveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.MessagePack;
using MessagePack.Exception;
using MessagePack.Serveur.Combat.Struct;

namespace MessagePackNestedWorld.MessagePack.Serveur.Combat
{
    public class MonsterReplaced : ResultBase
    {
        public Monster monster { get; set; }

        public MonsterReplaced() : base("combat:monster-replaced") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                var tmpUserMonster = receiveMessage.GetStruct("monster");

                monster = new Monster()
                {
                    Id = tmpUserMonster.GetInt("id"),
                    Name = tmpUserMonster.GetString("name"),
                    Monster_Id = tmpUserMonster.GetInt("monster_id"),
                    user_monster_id = tmpUserMonster.GetInt("user_monster_id"),
                    Hp = tmpUserMonster.GetInt("hp"),
                    Level = tmpUserMonster.GetInt("level"),
                };
                onCompled();
                return;
            }
            catch (NoAttributeFoundException ex)
            {
                OnError(ex);
            }
            catch (AttributeBadTypeException ex)
            {
                OnError(ex);
            }
            catch (AttributeEmptyException ex)
            {
                OnError(ex);
            }
        }
    }
}
