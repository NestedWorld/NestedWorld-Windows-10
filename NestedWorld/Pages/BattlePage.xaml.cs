using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.View.BattleViews;
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
    public sealed partial class BattlePage : Page
    {
        private BattleController battleController;

        public BattlePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.Navigate(typeof(Pages.HomePage));
                    a.Handled = true;
                }
            };
            try
            {
                battleController = e.Parameter as BattleController;
                battleController.UIUserMonster = UIUserMonster;
                battleController.UIEnnemiMonster = UIEnnemieMonster;

                battleController.EnnemieMonster = App.core.monsterList[battleController.start.OppomentMonster.Monster_Id].FromStruct(battleController.start.OppomentMonster);

                userMonsterList.controller = battleController;

                battleController.annimationCanvas = this.AnimationCanvas;
                battleController.Init(battleCanvas);

                Monster monster = battleController.UserMonsters.monsterList[0];
                battleController.UserMonster = monster.FromStruct(battleController.start.UserMonster);
              
            }
            catch (Exception ex)
            {
                Utils.Log.Error("BattlePage::OnNavigated", ex);
            }
        }
    }
}
