using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MonsterPage : Page
    {
        public MonsterPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetSize(e.Size.Width);
        }

        private void SetSize(double width)
        {
            if (width >= 1080)
            {
                InfoBlock.Width = (width / 3) - 10;
                LocationBlock.Width = (width / 3) - 10;
                UserMonsterInfoBlock.Width = (width / 3) - 10;
                UserMonsterLoactionBlock.Width = (width / 3) - 10;
                AttacksBlock.Width = (width / 3) - 10;
                StatsBlock.Width = (width / 3) - 10;
                return;
            }

            if (width >= 720)
            {
                InfoBlock.Width = (width / 2) - 10;
                LocationBlock.Width = (width / 2) - 10;
                UserMonsterInfoBlock.Width = (width / 2) - 10;
                UserMonsterLoactionBlock.Width = (width / 2) - 10;
                AttacksBlock.Width = (width / 2) - 10;
                StatsBlock.Width = (width / 2) - 10;
                return;
            }

            InfoBlock.Width = (width) - 10;
            LocationBlock.Width = (width) - 10;
            UserMonsterInfoBlock.Width = (width) - 10;
            UserMonsterLoactionBlock.Width = (width) - 10;
            AttacksBlock.Width = (width) - 10;
            StatsBlock.Width = width - 10;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= MonsterPage_BackRequested;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MonsterPage_BackRequested;
            base.OnNavigatedFrom(e);
            try
            {
                Monster monster = e.Parameter as Monster;
                if (!monster.PlayerMonster)
                {
                    UserMonsterInfoBlock.Visibility = Visibility.Collapsed;
                    UserMonsterLoactionBlock.Visibility = Visibility.Collapsed;
                    AttacksBlock.Visibility = Visibility.Collapsed;
                    StatsBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    StatsView.Life = monster.Life;
                    StatsView.Exp = monster.Exp;
                    BattleStats.DataContext = Stats.NewStat("Battle Stats", Utils.RandomValue.GetRandValue(0, 30), Utils.RandomValue.GetRandValue(0, 30), Utils.RandomValue.GetRandValue(0, 30));
                    userLocationMonster.point = new Windows.Devices.Geolocation.Geopoint(Utils.RandomValue.GetRandomPosition());
                }
                this.DataContext = e.Parameter;
                hearderView.DataContext = e.Parameter;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            SetSize(Window.Current.Bounds.Width);
        }

        private void MonsterPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                e.Handled = true;
            }
        }
    }
}
