using MessagePackNestedWorld.MessagePack.Client.Result.Combat;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NestedWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            UI.TitleBarCustom.ApplyToContainerMainPage();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 500));
           // App.network.connectionView = this.internalConnection;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            popupView.Closed += PopupView_Closed;

        }

        private void PopupView_Closed(object sender, object e)
        {
            Debug.WriteLine("close");
            LoadingView.Stop();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Opacity = 0;
            if (UserNameText.Text == string.Empty)
            {
                ShowError("Please enter a Login");
                return;
            }
            if (PassWordText.Password == string.Empty)
            {
                ShowError("Please enter a PassWord");
                return;
            }

            UserNameText.IsTabStop = false;
            PassWordText.IsTabStop = false;
            LoadingView.Start();

            loginButton.Visibility = Visibility.Collapsed;
            var ret = await App.network.Connect(UserNameText.Text, PassWordText.Password);

            LoadingView.Stop();
            UserNameText.IsTabStop = true;
            PassWordText.IsTabStop = true;

            if (ret.IsError)
                ShowError(ret.ToString());
            else
            {
                await App.core.Init();
                loginButton.Visibility = Visibility.Visible;

                UI.TitleBarCustom.ApplyToContainerHomePage();

                Frame.Navigate(typeof(Pages.HomePage));
            }
            loginButton.Visibility = Visibility.Visible;

        }

        private void ShowError(string ErrorMessage)
        {
            ErrorTextBlock.Text = ErrorMessage;
            ErrorAnnimation.Begin();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Regiter_Click(object sender, RoutedEventArgs e)
        {
            LoadingView.Start();

            popupView.Child = new PopUp.RegisterPopUp();
            popupView.IsOpen = true;
        }

        private void Forgot_Click(object sender, RoutedEventArgs e)
        {

            LoadingView.Start();

            popupView.Child = new PopUp.ForgotPassPopUp();
            popupView.IsOpen = true;
        }



        private void Setting_Click(object sender, RoutedEventArgs e)
        {

            //popupView.Child = new PopUp.SettingsPopUp();
            //popupView.IsOpen = true;
        }
    }
}
