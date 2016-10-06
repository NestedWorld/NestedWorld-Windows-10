using NestedWorld.Classes.ElementsGame.Battle;
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

namespace NestedWorld.View.BattleViews
{
    public sealed partial class UserMonsterList : UserControl
    {
        private BattleController _controller;

        public BattleController controller { get { return _controller; } set { _controller = value;  SetBinding(); } }

        private void SetBinding()
        {
            if (controller.UserMonsters.monsterList.Count <= 4)
                monsterFour.DataContext = controller.UserMonsters.monsterList[3];
            if (controller.UserMonsters.monsterList.Count <= 3)
                monsterThree.DataContext = controller.UserMonsters.monsterList[2];
            if (controller.UserMonsters.monsterList.Count <= 2)
                monsterTwo.DataContext = controller.UserMonsters.monsterList[1];
            if (controller.UserMonsters.monsterList.Count <= 1)
                monsterOne.DataContext = controller.UserMonsters.monsterList[0];
        }

        public UserMonsterList()
        {
            this.InitializeComponent();
            
        }

        private void monsterOne_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserMonsterView umv = sender as UserMonsterView;

            Log.Info("monsterOne_Tapped", umv.Name);

            switch (umv.Name)
            {
                case "monsterOne":
                    break;
                case "monsterTwo":
                    break;
                case "monsterThree":
                    break;
                case "monsterFour":
                    break;
            }
        }
    }
}
