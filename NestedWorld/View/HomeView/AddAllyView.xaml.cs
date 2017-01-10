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

namespace NestedWorld.View.HomeView
{
    public sealed partial class AddAllyView : UserControl
    {
        public delegate void EndCallback(bool success);


        public event EndCallback Done;
        public AddAllyView()
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
                var ret = await App.network.PostAllies(Entry.Text);
                ret.ShowError();

                App.core.userList = ret.Content as UserList;

                Done?.Invoke(true);
            }
            catch (Exception ex)
            {
                Utils.Log.Error("AddAllyView", ex);
                Done?.Invoke(false);
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Done?.Invoke(false);
        }
    }
}
