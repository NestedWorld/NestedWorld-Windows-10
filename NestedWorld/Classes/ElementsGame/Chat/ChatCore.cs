using MessagePack.Client.Chat;
using MessagePack.Serveur.Chat;
using NestedWorld.Classes.ElementGame.Chat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NestedWorld.Classes.ElementsGame.Chat
{
    public class ChatCore
    {
        private List<Channel> _list;

        public ObservableCollection<Channel> List
        {
            get { return new ObservableCollection<Channel>(_list); }
            set { }
        }

        private Channel _channelSelected;
        public Channel ChannelSelect
        {
            get { return _channelSelected; }
            set
            {
                LeftChannel(_channelSelected);
                _channelSelected = value;
                JoinChannel(value);
            }
        }

        //public popup

        public ChatCore()
        {

            //TODO callback answer
        }

        public void Init()
        {
            _list = new List<Channel>(App.core.userList.GetChannelAllies());

            if (App.core.Offline)
            {
                for (int i = 1; i < 11; i++)
                    _list.Add(new Channel("Test" + i.ToString()));
            }
            App.network.serveurMessageList["chat:user-joined"].OnCompled += User_Joinded;
            App.network.serveurMessageList["chat:user-parted"].OnCompled += User_Parteded;
            App.network.serveurMessageList["chat:message-received"].OnCompled += Receive_message;
        }

        public void JoinChannel(Channel channel)
        {
            var tmp = MessagePack.Client.Chat.JoinChannel.Join(channel.Name);
            App.network.SendRequest(tmp);
        }

        public void LeftChannel(Channel channel)
        {
            if (channel != null)
                App.network.SendRequest(PartChannel.Part(channel.Name));
        }

        public void SendMessage(string message)
        {
            App.network.SendRequest(MessagePack.Client.Chat.SendMessage.Send(ChannelSelect.Name, message));
        }


        private void User_Joinded(object value)
        {
            UserJoin Uj = value as UserJoin;
        }

        private void User_Parteded(object value)
        {
            UserPart Up = value as UserPart;
        }

        private void Receive_message(object value)
        {
            MessageReceived mr = value as MessageReceived;

            App.core.notificationInternal.SendNotification(Notification.NotificationType.MESSAGE,
                new Notification.MessageReceiveNotification()
                {
                    Message = mr.message,
                    Channel = mr.channel,
                    UserName = App.core.userList.userList[0].Name,
                    UserImage = App.core.userList.userList[0].Image
                });
        }
    }
}
