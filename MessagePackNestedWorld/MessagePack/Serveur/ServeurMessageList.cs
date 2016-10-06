using MessagePackNestedWorld.Utils;
using Network.MessagePack;
using System.Collections.Generic;
using System.IO;

namespace MessagePack.Serveur
{
    public class ServeurMessageList
    {
        private Dictionary<string, ResultBase> map;

        public Dictionary<string, string> idstack;

        public ServeurMessageList()
        {
            map = new Dictionary<string, ResultBase>();
            idstack = new Dictionary<string, string>();
        }

        public void Init()
        {
            Add(new ResultRequest());
            Add(new Chat.MessageReceived());
            Add(new Chat.UserJoin());
            Add(new Chat.UserPart());
            Add(new Combat.Available());
            Add(new Combat.Start());
            Add(new Combat.AttackReceived());
            Add(new Combat.MonsterKo());
            Add(new Combat.End());
        }

        public void Add(ResultBase resutl)
        {
            map[resutl.type] = resutl;
        }

        public void Add(string key, ResultBase resutl)
        {
            map[key] = resutl;
        }

        public ResultBase Get(string type)
        {
            ResultBase value;
            if (map.TryGetValue(type, out value))
                return value;
            throw new Exception.NoTypeFoundException(type);
        }

        public string GetLastId(string type)
        {
            string value;
            if (idstack.TryGetValue(type, out value))
                return value;
            throw new Exception.NoTypeFoundException(type);
        }

        public void SelectMessage(byte[] obj)
        {
            if (obj.Length == 0)
                return;

            using (MemoryStream ms = new MemoryStream(obj))
            {
                try
                {
                    ReceiveMessage rm = new ReceiveMessage(ms);
                    string type = rm.GetMessageType();
                    idstack[type] = rm.GetString("id");
                    Log.Info("SelectMessage, type receive", type);
                    var ret = Get(type);

                    ret.SetValue(rm);
                }
                catch (Exception.NoTypeFoundException ex)
                {
                    Log.Warning("ServeurMessageList.SelectMessage", ex);
                }
                catch (Exception.NoDictionaryFoundException ex)
                {
                    Log.Warning("ServeurMessageList.SelectMessage", ex);
                }
                catch (Exception.NoAttributeFoundException ex)
                {
                    Log.Warning("ServeurMessageList.SelectMessage", ex);
                }
            }
        }

        public ResultBase this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }
        }

    }
}