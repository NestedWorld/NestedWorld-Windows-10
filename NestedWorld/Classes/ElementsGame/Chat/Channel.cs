using NestedWorld.Classes.ElementsGame.Chat;
using NestedWorld.Classes.ElementsGame.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NestedWorld.Classes.ElementGame.Chat
{
    public class Channel
    {
        private List<Message> _messageList;
        private string _chatName;



        private List<User> _userList;

        public ObservableCollection<User> UserList { get { return new ObservableCollection<User>(_userList); } set { } }

        public bool canEdit { get; private set; }

        public ObservableCollection<Message> content { get { return new ObservableCollection<Message>(messageList); } set { } }
        public string image { get; set; }
        public string Name
        {
            get { return _chatName; }
            set
            {
                if (canEdit)
                    _chatName = value;
            }
        }

        public List<Message> messageList
        {
            get
            {
                return _messageList;
            }
            set
            {
                _messageList = value;
            }
        }

        public string discution { get { return this.ToString(); } set { } }

        public Channel(string chatName, string image = "ms-appx:///Assets/NestedWorldLogo.png")
        {
            messageList = new List<Message>();
            _userList = new List<User>();
            _chatName = chatName;
            this.image = image;
            this.canEdit = canEdit;
        }

        public Channel(User user)
        {
            messageList = new List<Message>();
            _userList = new List<User>();

            _userList.Add(user);

            _chatName = user.Name;
            image = user.Image;
            canEdit = false;
        }

        public void Add(Message message)
        {
            if (message.who == eWho.ME)
                App.core.Chat.SendMessage(message.content);
            messageList.Add(message);
        }

        public override string ToString()
        {
            if (messageList.Count != 0)
                return messageList.Last().ToString();
            return "Start Convesation";
        }
    }
}
