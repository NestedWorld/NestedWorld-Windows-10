using NestedWorld.Classes.ElementsGame.Chat;
using NestedWorld.Classes.ElementsGame.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NestedWorld.View.ChatViews
{
    public sealed partial class ChatMainView : UserControl
    {

        public ChatMainView()
        {
            this.InitializeComponent();
        }

        public void Init()
        {
            //chanelListView.listView.DataContext = App.core.userList.userList;
            chanelListView.listView.DataContext = App.core.Chat.List;

            chatView.GoBackButton.Click += GoBackButton_Click;
            chanelListView.listView.SelectionChanged += ListView_SelectionChanged;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ShowChannel.Begin();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.core.Chat.ChannelSelect = chanelListView.listView.SelectedItem as Channel;
            chatView.DataContext = App.core.Chat.ChannelSelect;
            ShowChat.Begin();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
