using MessagePack.Serveur.Combat.Struct;
using NestedWorld.Classes.ElementsGame.Users;
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

namespace NestedWorld.PopUp
{
    public sealed partial class NewBattlePopUp : UserControl
    {


        public NewBattlePopUp(Classes.ElementsGame.Users.User user)
        {
            this.InitializeComponent();
            this.DataContext = user;

            Title.Text = "New match avabaible";

            ShowAnnimation.Begin();
            RemoveAnnimation.Completed += RemoveAnnimation_Completed;
        }

        public NewBattlePopUp(Classes.ElementsGame.Monsters.Monster monster)
        {
            this.InitializeComponent();
            this.DataContext = monster;

            Title.Text = "A monster appears";

            ShowAnnimation.Begin();
            RemoveAnnimation.Completed += RemoveAnnimation_Completed;
        }

        private void RemoveAnnimation_Completed(object sender, object e)
        {
            Popup p = this.Parent as Popup;

            p.IsOpen = false;

            /*App.network.SendRequest(
                MessagePackNestedWorld.MessagePack.Client.Result.Combat.ResultAskSuccess.Awnser(
                    App.network.serveurMessageList.GetLastId("combat:available"), false, null));*/
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            Popup p = this.Parent as Popup;

            p.IsOpen = false;
            root.Navigate(typeof(Pages.PrepareBattlePage));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            RemoveAnnimation.Begin();
        }
    }
}
