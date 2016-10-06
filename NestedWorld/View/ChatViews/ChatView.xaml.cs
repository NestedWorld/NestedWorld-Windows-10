using NestedWorld.Classes.ElementGame.Chat;
using NestedWorld.Classes.ElementsGame.Chat;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.ChatViews
{
    public sealed partial class ChatView : UserControl
    {

        private Channel channel { get { return DataContext as Channel; } set { channel = value; } }

        public ChatView()
        {
            this.InitializeComponent();
            this.DataContextChanged += ChatView_DataContextChanged;
        }

        private void ChatView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (channel != null)
                chatList.ItemsSource = channel.content;
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && (sender as TextBox).Text != "")
            {
                Debug.WriteLine((sender as TextBox).Text);
                channel?.Add(new Message("You", (sender as TextBox).Text, DateTime.Now, eWho.ME));
                (sender as TextBox).Text = string.Empty;
                chatList.ItemsSource = channel.content;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text != "")
            {
                channel?.Add(new Message("You", (sender as TextBox).Text, DateTime.Now, eWho.ME));
                (sender as TextBox).Text = string.Empty;
                chatList.ItemsSource = channel.content;
            }
        }
    }
}
