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

namespace NestedWorld.View
{
    public sealed partial class MonsterTapItem : UserControl
    {
        private MonsterList _monsterList;
        public MonsterList monsterList
        {
            get { return _monsterList; }
            set
            {
                _monsterList = value;
                main.monsterListView.DataContext = monsterList.monsterList;
            }
        }

        public MonsterTapItem()
        {
            this.InitializeComponent();
            menu.monsterList = monsterList;
        }

        public void Init()
        {
            LoadingView.Start();

            //     var ret = await App.network.GetUseMonster();

            monsterList = App.core.monsterList;
            LoadingView.Stop();
            // ret.ShowError();*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            monsterList = new MonsterList(monsterList.SearchMonster((sender as TextBox).Text));
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            splitViewOption.IsPaneOpen = !splitViewOption.IsPaneOpen;
        }

    }
}
