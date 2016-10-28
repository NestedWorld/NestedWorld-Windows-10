using NestedWorld.Classes.ElementsGame.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.ProfileViews
{
    public sealed partial class EditProfileView : UserControl
    {
        public delegate void OnCompledCallBack();
        public delegate void OnCancelCallBack();

        public event OnCompledCallBack OnCompled;
        public event OnCancelCallBack OnCancel;


        private string UserImage;
        private string UserBackground;
        private string UserName;

        public EditProfileView()
        {
            this.InitializeComponent();
            this.UserImage = App.core.user.Image;
            this.UserBackground = App.core.user.Background;
            this.UserName = App.core.user.Name;
            this.DataContext = App.core.user;
            this.Visibility = Visibility.Collapsed;

        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        private async void UpdateInfo()
        {
            var ret = await App.network.UpdateInfo(UserNameTextBox.Text == App.core.user.Name ? null : UserNameTextBox.Text, this.UserImage, this.UserBackground);
        }

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFile file = await Utils.ImageUtils.ImageOpenPicker();
            UserImage = await new Classes.Network.AmazonPostingImage().PostImage(file, "UserImage");
            UserImageEllipse.Fill = Utils.ImageUtils.UrlToFillSource(UserImage);
        }

        private async void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            StorageFile file = await Utils.ImageUtils.ImageOpenPicker();
            UserBackground = await new Classes.Network.AmazonPostingImage().PostImage(file, "UserBackground");
            UserbackgroundRectangle.Fill = Utils.ImageUtils.UrlToFillSource(UserBackground);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
            this.Visibility = Visibility.Collapsed;
            OnCompled?.Invoke();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            OnCancel?.Invoke();
        }
    }
}
