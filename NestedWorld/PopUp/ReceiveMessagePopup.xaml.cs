using NestedWorld.Classes.ElementsGame.Notification;
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
    public sealed partial class ReceiveMessagePopup : UserControl
    {
        public ReceiveMessagePopup()
        {
            this.InitializeComponent();
            this.DataContextChanged += ReceiveMessagePopup_DataContextChanged;
        }

        private void ReceiveMessagePopup_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            ShowAnnimation.Begin();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MessageReceiveNotification).OnAccept();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MessageReceiveNotification).OnDecline();
        }
    }
}
