using NestedWorld.Classes.ElementsGame.Exchanges;
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

namespace NestedWorld.View.ExchangeViews
{
    public sealed partial class ExchangeView : UserControl
    {
        public ExchangeView()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public async void Show()
        {
            var ret = await App.network.GetExchange();

            ret.ShowError();

            ExchangeList exchangeList = ret.Content as ExchangeList;
            this.DataContext = exchangeList;
            this.Visibility = Visibility.Visible;
            ShowAnnimation.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.HideAnnimation.Begin();
            this.Visibility = Visibility.Collapsed;

        }
    }
}
