using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.PopUp
{
    public sealed partial class AddAllyPopUp : UserControl
    {
        public AddAllyPopUp()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }



        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var ret = await App.network.PostAllies(entry.Text);
            ret.ShowError();
            NestedWorldHttp.HttpResult result = (ret.Content as NestedWorldHttp.HttpResult);
            if (result.code != System.Net.HttpStatusCode.Accepted)
            {
                Info.Text = "no user found";
            }
            else
            {
                Info.Text = string.Format("{0} is now you ally", entry.Text);
                ret = await App.network.GetAllies();
                App.core.userList = ret.Content as UserList;
                ret.ShowError();
            }
        }
    }
}
