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

            App.network.serveurMessageList["combat:start"].OnCompled += CombatStart;
        }

        private void CombatStart(object value)
        {
            try
            {
                Start start = value as Start;

                BattleController battleController = new BattleController();

                battleController.UserMonsters = body.selectedMonster;
                battleController.combatID = start.combat_id;
                battleController.start = start;
                loadingView.Stop();
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

            header.Battle = this.battle;

        }

        private void OK_click(object sender, RoutedEventArgs e)
        {
            loadingView.Start();
            App.network.SendRequest(
                  MessagePackNestedWorld.MessagePack.Client.Result.Combat.ResultAskSuccess.Awnser(
                      this.battle.BattleID,
                  true, this.body.selectedMonster.idarray));
        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

            //  prepareSelectedMonsted.Init();
        }
    }
}
