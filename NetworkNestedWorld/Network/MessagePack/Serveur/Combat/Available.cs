﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.MessagePack.Serveur.Combat
{
    public class Available : ResultBase
    {
        private const string WILD = "wild-monster";
        private const string DUEL = "duel";
        public string origin { get; set; }
        public int monster_id { get; set; }
        public Struct.User user { get; set; }

        public Available() : base("combat:available") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {
            try
            {
                origin = receiveMessage.GetString("origin");
                if (origin == WILD)
                    monster_id = Convert.ToInt32(receiveMessage.GetByte("monster_id"));
                else if (origin == DUEL)
                    {
                    var tmp = receiveMessage.GetStruct("user");
                    user = new Struct.User()
                    {
                        Name = tmp.GetString("name"),
                        Id = tmp.GetInt("id")
                    };
                }
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
