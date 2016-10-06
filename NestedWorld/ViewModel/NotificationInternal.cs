using NestedWorld.Classes.ElementsGame.Notification;
using NestedWorld.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace NestedWorld.ViewModel
{
    public class NotificationInternal
    {
        public Popup Conteneur { get; set; }

        public NotificationInternal()
        {
        }

        public void SendNotification(NotificationType type, object dataContext)
        {
            switch (type)
            {
                case (NotificationType.MESSAGE):
                    SendNotification(new ReceiveMessagePopup(), dataContext);
                    break;
                case (NotificationType.ALLY):
                    SendNotification(new ReceiveMessagePopup(), dataContext);
                    break;
                case (NotificationType.ZONE):
                    SendNotification(new ReceiveMessagePopup(), dataContext);
                    break;
                case (NotificationType.MONSTER):
                    SendNotification(new ReceiveMessagePopup(), dataContext);
                    break;
                case (NotificationType.BATTLE):
                    SendNotification(new ReceiveMessagePopup(), dataContext);
                    break;
            }
        }

        public void SendNotification(UserControl NotifUI, object dataContext)
        {
            NotifUI.DataContext = dataContext;
            Conteneur.Child = NotifUI;
            Conteneur.Closed += Conteneur_Closed;
            Conteneur.IsOpen = true;
        }

        private void Conteneur_Closed(object sender, object e)
        {
            Conteneur.Closed -= Conteneur_Closed;
        }
    }
}
