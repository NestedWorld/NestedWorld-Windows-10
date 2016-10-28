using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NestedWorld.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NestedWorld.Classes.ElementsGame.Session
{
    public class UserSession
    {
        private Utils.AppData.Composite composite;

        public string Token
        {
            get { return this.composite["token"] as string; }
            set { this.composite["token"] = value; }
        }

        public DateTime LastSession
        {
            get { return new DateTime().FromTimestamp(Convert.ToDouble(this.composite["LastSession"])); }
            set { this.composite["lastSession"] = value.ToTimestamp(); }
        }

        public string Mail
        {
            get { return this.composite["mail"] as string; }
            set { this.composite["mail"] = value; }
        }

        public string Password
        {
            get { return this.composite["password"] as string; }
            set { this.composite["password"] = value; }
        }

        public bool ValideToken
        {
            get { return this.Token != null; }
            set { }
        }

        public UserSession()
        {
            this.composite = Utils.AppData.Composite.Instance("session");
        }

        public async void Delete()
        {
            this.composite.Save();
            await App.network.Logout();
        }

        public void SaveToken()
        {
            this.composite.Save();
        }

        public async Task<NestedWorldHttp.ReturnObject> Connect(bool redirect = true)
        {
            Frame root = Window.Current.Content as Frame;

            var ret = await App.network.Connect(this.Mail, this.Password);
            if (ret.IsError)
            {
                ret.ShowError();
                return ret;
            }
            await App.core.Init();

            UI.TitleBarCustom.ApplyToContainerHomePage();
            if (redirect)
                root.Navigate(typeof(Pages.HomePage));
            return ret;

        }

        public async void Disconnect()
        {
            Frame root = Window.Current.Content as Frame;
            var ret = await App.network.Logout();
            ret.ShowError();
            this.Token = null;
            App.network.streamConnection.Disconnect();
            App.network = new Network.Network();

            root.Navigate(typeof(MainPage));
        }
    }
}
