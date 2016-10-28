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
                this.DataContext = value;
            }
        }

        public MonsterTapItem()
        {
            this.InitializeComponent();            
        }

        public void Init()
        {
            monsterList = App.core.monsterList;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            monsterList = new MonsterList(monsterList.SearchMonster((sender as TextBox).Text));
        }

        private void monsterListView_OnMonsterSelected(Monster value)
        {

        }

        private void Banner_OnPannelOpenClose()
        {
            //this.splitViewOption.IsPaneOpen = !splitViewOption.IsPaneOpen;
        }

        private void Banner_OnSearch(string term)
        {
             monsterList = new MonsterList(App.core.monsterList.SearchMonster(term));
        }
    }
}
