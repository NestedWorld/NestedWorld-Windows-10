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
    public sealed partial class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();

        }


        public async void Init()
        {
            var req = await App.network.GetUserMonster();
            req.ShowError();
            App.core.monsterUserList = req.Content as MonsterList;
            monsterView.monsterList = App.core.monsterUserList;
            userView.userList = App.core.userList;
            header.DataContext = App.core.user;
        }



        private void addAllyView_Done(bool success)
        {
            this.addAllyView.Hide();
            if (success)
                userView.userList = App.core.userList;
        }

        private void userView_Add()
        {
            this.addAllyView.Show();
        }
    }
}
