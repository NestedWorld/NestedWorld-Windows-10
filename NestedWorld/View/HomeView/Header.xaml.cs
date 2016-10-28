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
    public sealed partial class Header : UserControl
    {
        public delegate void CreditTaped();
        public delegate void MonsterTaped();
        public delegate void AreaTaped();
        public delegate void AllyTapped();

        public event CreditTaped OnCreditTaped;

        public event MonsterTaped OnMonsterTaped;

        public event AreaTaped OnAreaTaped;

        public event AllyTapped OnAllyTapped;

        public Header()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Utils.Log.Info((sender as Grid).Name);
            switch ((sender as Grid).Name)
            {
                case "credit":
                    this.OnCreditTaped?.Invoke();
                    break;
                case "monster":
                    this.OnMonsterTaped?.Invoke();
                    break;
                case "area":
                    this.OnAreaTaped?.Invoke();
                    break;
                case "ally":
                    this.OnAllyTapped?.Invoke();
                    break;
            }
        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(Pages.ProfilePage));
        }
    }
}
