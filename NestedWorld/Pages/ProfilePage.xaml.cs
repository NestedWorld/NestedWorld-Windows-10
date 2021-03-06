﻿using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Classes.ElementsGame.Player;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Classes.ElementsGame.Users.Stats stats = null;

            try
            {
                if (e.Parameter == null)
                {
                    mainView.DataContext = App.core.user;
                    this.DataContext = App.core.user;
                    PlayerCommandBar.Visibility = Visibility.Visible;
                    UserCommandBar.Visibility = Visibility.Collapsed;

                    var ret = await App.network.GetUserStat();
                    ret.ShowErrorOnApp();

                    stats = ret.Content as Classes.ElementsGame.Users.Stats;
                }
                else
                {
                    User user = e.Parameter as User;
                    this.DataContext = user;
                    mainView.DataContext = user;
                    PlayerCommandBar.Visibility = Visibility.Collapsed;
                    UserCommandBar.Visibility = Visibility.Visible;
                    var ret = await App.network.GetUserStat(user.id);
                    ret.ShowErrorOnApp();
                    stats = ret.Content as Classes.ElementsGame.Users.Stats;
                }
                statsTotal.SetValue(stats.Defeats.total, stats.Victories.total);
                statsPVE.SetValue(stats.Defeats.pve, stats.Victories.pve);
                statsPVP.SetValue(stats.Defeats.pvp, stats.Victories.pvp);
                statsPortals.SetValue(stats.Defeats.portals, stats.Victories.portals);
                monsterStats.Stats = stats.Monsters;
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
           /* battleStats.DataContext = Stats.NewStat("Battle Statstics", 20, 1, 15);
            catchStats.DataContext = Stats.NewStat("Catch Statistics", 40, 2, 15);
            areaStats.DataContext = Stats.NewStat("Areas Statistics", 10, 10, 10);*/
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.EditProfileView.Show();
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
            User user = this.DataContext as User;

            App.core.battleRouter.OppBattle[sender.id] = new Classes.ElementsGame.Battle.Battle()
            {
                OpponentImage = user.Image,
                OpponentName = user.Name,
                BattleID = sender.id,
                ContextBattle = Context.PVP,
                StateBattle = State.AVALAIBLE
            };
            var battle = App.core.battleRouter.OppBattle[sender.id];
            Frame.Navigate(typeof(Pages.PrepareBattlePage), battle);

            /*            }
                        App.network.serveurMessageList["combat:available"].OnCompled += ProfilePage_OnCompled;*/
        }

        private void ProfilePage_OnCompled(object value)
        {
            try
            {
                MessagePack.Serveur.Combat.Available av = value as MessagePack.Serveur.Combat.Available;
                if (av.origin == "duel")
                {
                    var battle = App.core.battleRouter.OppBattle[av.id];
                    Frame.Navigate(typeof(Pages.PrepareBattlePage), battle);
                }
            }
            catch (NestedWorld.Classes.Exception.InvalideMapKeyException invalidMapKeyException)
            {
                Utils.Log.Error(invalidMapKeyException);
            }
        }

        private async void EditProfileView_OnCompled()
        {
            var ret = await App.network.GetUserInfo();

            App.core.user = ret.Content as UserInfo;
        }
    }
}
