using NestedWorld.Classes.ElementsGame.Monsters;
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

namespace NestedWorld.View.ExchangeViews
{
    public sealed partial class CreateExchangeView : UserControl
    {
        public CreateExchangeView()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.HideAnnimation.Begin();
            this.Visibility = Visibility.Collapsed;
        }

        public void Show()
        {
            userMonsertList.DataContext = App.core.monsterUserList.content;
            monsertList.DataContext = App.core.monsterList.content;
            this.Visibility = Visibility.Visible;
            ShowAnnimation.Begin();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Monster monsterSend = userMonsertList.SelectedMonster;
            Monster monsterAsk = monsertList.SelectedMonster;

            if (monsterSend == null || monsterAsk == null)
                return;

            var ret = await App.network.PostEchange(new Classes.ElementsGame.Exchanges.Exchange()
            {
                MonsterIdSend = monsterSend.ID,
                UserMonsterSend = monsterSend,
                MonsterIdAsk = monsterAsk.ID,
                Id = 0
            }
            );

            ret.ShowError();

            this.Visibility = Visibility.Collapsed;
            HideAnnimation.Begin();
        }
    }
}
