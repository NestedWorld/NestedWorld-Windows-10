using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.Classes.Stats;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NestedWorld.Pages
{
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (e.Parameter == null)
                {
                    mainView.DataContext = App.core.user;
                    this.DataContext = App.core.user;
                    PlayerCommandBar.Visibility = Visibility.Visible;
                    UserCommandBar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    User user = e.Parameter as User;
                    this.DataContext = user;
                    mainView.DataContext = user;
                    PlayerCommandBar.Visibility = Visibility.Collapsed;
                    UserCommandBar.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.Navigate(typeof(Pages.HomePage));
                    a.Handled = true;
                }
            };
            battleStats.DataContext = Stats.NewStat("Battle Statstics", 20, 1, 15);
            catchStats.DataContext = Stats.NewStat("Catch Statistics", 40, 2, 15);
            areaStats.DataContext = Stats.NewStat("Areas Statistics", 10, 10, 10);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void StartAttack(object sender, RoutedEventArgs e)
        {
            User user = this.DataContext as User;
            var tmp = MessagePackNestedWorld.MessagePack.Client.Combat.Ask.StartFightWhit((this.DataContext as User).Name);
            tmp.OnSuccess += Tmp_OnCompled;
            tmp.OnError += Tmp_OnError;
            App.network.SendRequest(tmp);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            App.network.serveurMessageList["combat:available"].OnCompled -= ProfilePage_OnCompled;

        }

        private async void Tmp_OnError(MessagePack.Serveur.ResultRequest result)
        {
            await new MessageDialog("User not availble").ShowAsync();
        }

        private void Tmp_OnCompled(MessagePack.Serveur.ResultRequest sender)
        {
            App.network.serveurMessageList["combat:available"].OnCompled += ProfilePage_OnCompled;
        }

        private void ProfilePage_OnCompled(object value)
        {
            try
            {
                var battle = App.core.battleRouter.OppBattle[(value as MessagePack.Serveur.Combat.Available).id];
                Frame.Navigate(typeof(Pages.PrepareBattlePage), battle);

            }
            catch (NestedWorld.Classes.Exception.InvalideMapKeyException invalidMapKeyException)
            {
                Utils.Log.Error(invalidMapKeyException);
            }
        }
    }
}
