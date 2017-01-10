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

namespace NestedWorld.View.MonsterViews.MonsterPage
{
    public sealed partial class UpdateSurnameView : UserControl
    {
        public delegate void EndCallback(Monster monster);

        public event EndCallback Done;


        public UpdateSurnameView()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Monster m = this.DataContext as Monster;

                var ret = await App.network.UpdateMonsterInfo(m.UserID, this.Entry.Text);

                ret.ShowError();

                App.core.monsterUserList[m.UserID] = ret.Content as Monster;

                this.Done?.Invoke(ret.Content as Monster);
            }
            catch (Exception ex)
            {
                Utils.Log.Error("UpdateSurname::Button_click", ex);
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Done?.Invoke(null);
        }
    }
}
