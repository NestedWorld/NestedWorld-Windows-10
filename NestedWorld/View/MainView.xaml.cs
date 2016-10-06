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

namespace NestedWorld.View
{
    public sealed partial class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            UpdateSize();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            if (Window.Current.Bounds.Width < 720)
            {
                monsterView.Width = (Window.Current.Bounds.Width - 10);
                userView.Width = (Window.Current.Bounds.Width - 10);
                return;
            }
            monsterView.Width = (Window.Current.Bounds.Width / 2) - 10;
            userView.Width = (Window.Current.Bounds.Width / 2) - 10;
        }

        public void Init()
        {
            monsterView.monsterList = App.core.monsterUserList;
            userView.userList = App.core.userList;
            homeView.DataContext = App.core.user;
        }


        private void homeView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(Pages.ProfilePage));
        }
    }
}
