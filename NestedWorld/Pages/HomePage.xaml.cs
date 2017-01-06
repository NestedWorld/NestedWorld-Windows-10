using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Style;
using NestedWorld.ViewModel;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using NestedWorld.Utils;
using MessagePack.Serveur.Combat;
using NestedWorldNotificationLib.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NestedWorld.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private const string taskName = "NotificationBackground";
        private const string taskEntryPoint = "NotificationBackground.BackgroundCore";

        private HomePageController controller;

        public HomePage()
        {
            this.InitializeComponent();
            App.RootContent = this.content;
        }

        private async void RegisterBackgroundTask()
        {
            Log.Info("HomePage", "RegisterBackgroundTask");
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = taskName;
                taskBuilder.TaskEntryPoint = taskEntryPoint;
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                var registration = taskBuilder.Register();
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //  if (e.SourcePageType == typeof(MainPage))
            //    this.RegisterBackgroundTask();

           


            init();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            App.network.serveurMessageList["combat:available"].OnCompled -= CombatAvalaible_OnCompled;
        }

        public void init()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            // App.core.SetValue(this);
            controller = new HomePageController(this);
            App.core.notificationInternal.Conteneur = popupView;


            App.network.serveurMessageList["combat:available"].OnCompled += CombatAvalaible_OnCompled;

            //App.RootContent.Children.Add(new Pages.SettingsPage());
        }

        private void CombatAvalaible_OnCompled(object value)
        {
            //  Available av = value as Available;

            // Log.Info("Combat oppoment", App.core.monsterList[av.monster_id].Name);

            //popupView.Child = new PopUp.NewBattlePopUp(App.core.monsterList[av.monster_id]);
            //popupView.IsOpen = true;

            BattleButton.Label = App.core.battleRouter.Sum == 0 ? "" : App.core.battleRouter.Sum.ToString();

        }

        private void ChatClick(object sender, RoutedEventArgs e)
        {
            chatview.Visibility = Visibility.Visible;
            battleview.Visibility = Visibility.Collapsed;

            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
            chatview.Init();
        }

        private void BattleClick(object sender, RoutedEventArgs e)
        {
            chatview.Visibility = Visibility.Collapsed;
            battleview.Visibility = Visibility.Visible;

            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
            battleview.Init();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.SettingsPage));
        }

        private void LogtoutClick(object sender, RoutedEventArgs e)
        {
            App.UserSession.Disconnect();
        }

        private void homeView_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.ProfilePage));
        }
    }
}
