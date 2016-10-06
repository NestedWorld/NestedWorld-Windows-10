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
    public sealed partial class MonsterNearPopUp : UserControl
    {
        public MonsterNearPopUp()
        {
            this.InitializeComponent();
            ShowAnnimation.Begin();
            RemoveAnnimation.Completed += RemoveAnnimation_Completed;
        }

        private void RemoveAnnimation_Completed(object sender, object e)
        {
            Popup p = this.Parent as Popup;

            p.IsOpen = false;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;

            root.Navigate(typeof(Pages.PrepareBattlePage));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            RemoveAnnimation.Begin();
        }
    }
}
