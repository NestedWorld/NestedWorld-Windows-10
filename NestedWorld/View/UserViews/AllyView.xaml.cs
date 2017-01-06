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

namespace NestedWorld.View.UserViews
{
    public sealed partial class AllyView : UserControl
    {
        public AllyView()
        {
            this.InitializeComponent();
            this.DataContextChanged += AllyView_DataContextChanged;
        }

        private void AllyView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext == null)
                return;

            try
            {
                this.OnlineStatus.Visibility = (DataContext as User).IsOnline ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception ex)
            {
                Utils.Log.Error("AllyView::DataContextChanged", ex);
            }
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;

            root.Navigate(typeof(Pages.ProfilePage), DataContext);
        }
    }
}
