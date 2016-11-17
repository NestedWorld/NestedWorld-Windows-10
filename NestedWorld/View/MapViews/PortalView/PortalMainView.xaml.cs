using NestedWorld.Classes.ElementsGame.Portals;
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

namespace NestedWorld.View.MapViews.PortalView
{
    public sealed partial class PortalMainView : UserControl
    {
        public PortalMainView()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
            this.ShowAnnimation.Begin();
        }

        public void Hide()
        {
            this.HideAnnimation.Begin();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MonsterCircleView_OnMonsterSelected(Classes.ElementsGame.Monsters.Monster monster)
        {
            Utils.Log.Info(monster.Name);
            try
            {
                var tmp = MessagePackNestedWorld.MessagePack.Client.Portal.Capture.TryCapture((this.DataContext as Portal).ID, monster.UserID);
                tmp.OnSuccess += Tmp_OnSuccess;
                tmp.OnError += Tmp_OnError;
                App.network.SendRequest(tmp);
            }
            catch(Exception ex)
            {
                Utils.Log.Error("OnMonsterSelected", ex);
            }

        }

        private void Tmp_OnError(MessagePack.Serveur.ResultRequest result)
        {
            
        }

        private void Tmp_OnSuccess(MessagePack.Serveur.ResultRequest result)
        {
            
        }
    }
}
