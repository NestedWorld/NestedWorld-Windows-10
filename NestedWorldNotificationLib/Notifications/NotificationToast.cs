using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace NestedWorldNotificationLib.Notifications
{
    public enum ToastInputType
    {
        none,
        input,
        button,
        inputAndButton,
    }

    public class NotificationToast
    {

        public ToastInputType Type { get; set; }

        private Dictionary<string, NotificationAction> notificationActions { get; set; }

        public string title { get; set; }
        public string content { get; set; }
        public string image { get; set; }
       
        public string Input { get; set; }

        private ToastVisual visual
        {
            get
            {
                return new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },
                            new AdaptiveText()
                            {
                                Text = content
                            }

                    },

                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = this.image == null ? "ms-appx:///Assets/NestedWorldLogo.png" : this.image,
                            HintCrop = ToastGenericAppLogoCrop.Default,

                        }
                    }
                };
            }
            set
            {
            }
        }
        private ToastActionsCustom action
        {
            get
            {
                ToastActionsCustom tmp = new ToastActionsCustom()
                {

                };
                if (this.Input != String.Empty)
                    tmp.Inputs.Add(
                        new ToastTextBox("tbRepply")
                        {
                            PlaceholderContent = this.Input
                        });

                foreach (KeyValuePair<string, NotificationAction> actions in this.notificationActions)
                    tmp.Buttons.Add(actions.Value.button);
                return tmp;
            }
            set
            {
            }
        }
        public string notificationId { get; set; }
        public ToastNotification toast
        {
            get
            {
                ToastContent toastContent = new ToastContent()
                {
                    Visual = visual,
                    Actions = action,
                    Launch = new QueryString()
                    {
                        { "action", "viewConversation" },
                        { "notificationId", notificationId.ToString() }

                    }.ToString()
                };
                return new ToastNotification(toastContent.GetXml());
            }
            set
            {

            }
        }

        public NotificationToast(ToastInputType type)
        {
            this.Type = type;
            this.Input = string.Empty;
            this.image = "ms-appx:///Assets/NestedWorldLogo.png";
            this.notificationActions = new Dictionary<string, NotificationAction>();

        }

        public void Add(NotificationAction NewAction)
        {
            NewAction.TextId = "tbRepply";
            NewAction.conversationId = this.notificationId;
            this.notificationActions[NewAction.Name] = NewAction;
        }

        public void Show()
        {
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }



    }

}

