using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Notification
{
    public class MessageReceiveNotification : NotificationBase
    {
        public string Message;
        public string Channel;
        public string UserName;
        public string UserImage;

        public MessageReceiveNotification() : base(NotificationType.MESSAGE)
        {

        }

        public override void OnAccept()
        {
            //show message;
        }

        public override void OnDecline()
        {
            root.IsOpen = false;
        }
    }
}
