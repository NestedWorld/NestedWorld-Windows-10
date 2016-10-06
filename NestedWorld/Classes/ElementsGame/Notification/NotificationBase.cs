using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace NestedWorld.Classes.ElementsGame.Notification
{
    public enum NotificationType
    {
        MESSAGE,
        MONSTER,
        ZONE,
        ALLY,
        BATTLE
    }
    public abstract class NotificationBase
    {
        public NotificationType type;
        public Popup root;


        public NotificationBase(NotificationType type)
        {
            this.type = type;
        }

        public abstract void OnAccept();
        public abstract void OnDecline();
    }
}
