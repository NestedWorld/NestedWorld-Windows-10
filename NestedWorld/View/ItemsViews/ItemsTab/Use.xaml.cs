using NestedWorld.Classes.ElementsGame.Item;
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

namespace NestedWorld.View.ItemsViews.ItemsTab
{
    public sealed partial class Use : UserControl
    {
        public Use()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }


        public void Show()
        {
            this.Visibility = Visibility.Visible;
            this.ShowAnnimation.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.HideAnnimation.Begin();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Item item = this.DataContext as Item;
            Utils.Log.Info("Item", item.Name, "use on", userMonsterList.selectedMonster.Name);
        }
    }
}
