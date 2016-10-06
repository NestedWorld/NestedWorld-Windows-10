using MessagePack.Serveur.Combat;
using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.Utils;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NestedWorld.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrepareBattlePage : Page
    {

        public Battle battle { get; set; }
        public User ennemie { get; set; }
        public PrepareBattlePage()
        {
            this.InitializeComponent();
            UserMonsterGridView.DataContext = App.core.monsterUserList.monsterList;

            App.network.serveurMessageList["combat:start"].OnCompled += CombatStart;
        }

        private void CombatStart(object value)
        {
            try
            {
                Start start = value as Start;

                BattleController battleController = new BattleController();

                battleController.UserMonsters = prepareSelectedMonsted.MonsterListSelected;
                battleController.EnnemieMonster = App.core.monsterList[start.OppomentMonster.Monster_Id].FromStruct(start.OppomentMonster);
                battleController.combatID = start.combat_id;
                battleController.start = start;

                Frame.Navigate(typeof(Pages.BattlePage), battleController);
            }
            catch (System.NullReferenceException nullex)
            {
                Log.Warning("CombatStart", nullex);
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.battle = e.Parameter as Battle;

            switch (battle.ContextBattle)
            {
                case (Context.WILD):
                    break;

                case (Context.PVP):
                    break;

                case (Context.TOWER):
                    break;
            }

            try
            {
                ennemie = e.Parameter as User;
                Header.DataContext = App.core.user;
                EnnemieHeader.DataContext = ennemie;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void OK_click(object sender, RoutedEventArgs e)
        {
            App.network.SendRequest(
                  MessagePackNestedWorld.MessagePack.Client.Result.Combat.ResultAskSuccess.Awnser(
                      this.battle.BattleID,
                  true, prepareSelectedMonsted.idarray));
            prepareSelectedMonsted.MonsterListSelected.loadAttack();

            /*

            #region debug
            BattleController battleController = new BattleController();

            battleController.UserMonsters = prepareSelectedMonsted.MonsterListSelected;
            battleController.EnnemieMonsters = prepareSelectedMonsted.MonsterListSelected;
            battleController.combatID = 0;

            Frame.Navigate(typeof(Pages.BattlePage), battleController);

            #endregion*/

        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            prepareSelectedMonsted.Add(UserMonsterGridView.SelectedItem as Monster);
        }

        private void UserMonsterGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (UserMonsterGridView.SelectedIndex == -1)
                return;

            StackIndicator.Children.Clear();

            for (int i = 0; i < App.core.monsterUserList.monsterList.Count; i++)
            {
                if (i == UserMonsterGridView.SelectedIndex)
                {
                    StackIndicator.Children.Add(new Ellipse()
                    {
                        Height = 10,
                        Width = 10,
                        Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Stroke = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Margin = new Thickness(2.5)
                    });
                }
                else
                {
                    StackIndicator.Children.Add(new Ellipse()
                    {
                        Height = 10,
                        Width = 10,
                        Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FFFFFFFF")),
                        Stroke = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Margin = new Thickness(2.5)
                    });
                }
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            prepareSelectedMonsted.Init();
        }
    }
}
