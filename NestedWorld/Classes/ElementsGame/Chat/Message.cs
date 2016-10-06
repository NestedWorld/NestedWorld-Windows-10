using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NestedWorld.Classes.ElementsGame.Chat
{

    public enum eWho
    {
        ME,
        YOU,
    }
    public class Message
    {
        public string content { get; private set; }
        public DateTime time { get; private set; }
        public eWho who { get; private set; }
        private string _name;
        public string Name { get { return _name; } set { } }

        public Message(string name, string c, DateTime t, eWho w)
        {
            content = c;
            time = t;
            who = w;
            _name = name;
        }

        public override string ToString()
        {
            return Name + " : " + content;
        }
    }
}
