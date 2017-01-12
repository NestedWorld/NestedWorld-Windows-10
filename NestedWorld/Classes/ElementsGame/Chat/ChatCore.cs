using MessagePack.Client.Chat;
using MessagePack.Serveur.Chat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using NestedWorldSocketIo;
using Newtonsoft.Json.Linq;

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
                _channelSelected = value;
                JoinChannel(value);
            }
        }

       
        public void JoinChannel(Channel channel)
        {
            socketIo.Send("subscribe", channel.Name);
        }

        public void SendMessage(string message)
        {
            JObject obj = new JObject();

            obj.Add("room", _channelSelected.Name);
            obj.Add("message", message);


            socketIo.Send("send message", obj.ToString());
        }

        //public popup

        public ChatCore()
        {

            //TODO callback answer
        }

        public void Init()
        {

            _list = new List<Channel>(App.core.userList.GetChannelAllies());

            socketIo = new SocketIo("http://nestedworld.com:4241");

            socketIo.On("conversation private post", (data) =>
            {
                Utils.Log.Info(data);
            });

            socketIo.Connect();

            
           

        }


    }
}
