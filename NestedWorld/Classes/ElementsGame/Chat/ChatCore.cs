using MessagePack.Client.Chat;
using MessagePack.Serveur.Chat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using NestedWorldSocketIo;
namespace NestedWorld.Classes.ElementsGame.Chat
{
    public class ChatCore
    {

        private SocketIo socketIo;
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

        private void LeftChannel(Channel channelSelected)
        {

        }

        public void JoinChannel(Channel channel)
        {

        }

        public void SendMessage(string message)
        {

        }

        //public popup

        public ChatCore()
        {

            //TODO callback answer
        }

        public void Init()
        {

            _list = new List<Channel>(App.core.userList.GetChannelAllies());

            socketIo = new SocketIo("localhost:4241");

           

            socketIo.Connect();

            socketIo.Send("subscribe", "test");
            socketIo.Send("send message", "hello");

        }


    }
}
