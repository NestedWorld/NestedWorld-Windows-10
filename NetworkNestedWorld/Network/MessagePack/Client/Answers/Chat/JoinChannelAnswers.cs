﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NestedWorld.Classes.Network.MessagePack.Serveur;

namespace NestedWorld.Classes.Network.MessagePack.Client.Answers.Chat
{
    public class JoinChannelAnswers : AnswerBase
    {
        public Dictionary<int, string> UserMap { get; set; }
        public override void SetValue(ResultRequest result)
        {
            try
            {
                UserMap = new Dictionary<int, string>();
                var userArray = result.Content.GetStruct("users");
                foreach (var item in userArray.Map)
                    UserMap[Convert.ToInt32(item.Key)] = (string)item.Value;
                //OnCompled?.Invoke(this);
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
