using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldNotificationLib.Notifications
{
    public class NotificationAction
    {
        public delegate void OnActionCallback(object actionOne);
        public string conversationId { get; set; }
        public ToastButton button
        {
            get
            {
                return new ToastButton(this.Text, new QueryString()
                    {
                        { "action", this.Name },
                        { "conversationId", conversationId.ToString()}
                    }.ToString())
                {
                    ActivationType = this.type,
                    ImageUri = this.Image,
                    TextBoxId = this.TextId
                };
            }
            set
            {

            }
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public string TextId { get; set; }
        public ToastActivationType type { get; set; }
        public string Image { get; set; }

    }
}
