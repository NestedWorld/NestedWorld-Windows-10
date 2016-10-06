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

namespace NestedWorld.PopUp
{
    public sealed partial class ForgotPassPopUp : UserControl
    {
        public ForgotPassPopUp()
        {
            this.InitializeComponent();
            this.Loaded += ForgotPassPopUp_Loaded;
        }

        private void ForgotPassPopUp_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAnnim.Begin();
            SetSize(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

            Popup p = this.Parent as Popup;
            HideAnnim.Begin();
            p.IsOpen = false;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (mailEntry.Text == string.Empty || !mailEntry.Text.Contains("@"))
            {
                setError("Mail");
                return;
            }

            Classes.Request.Auth.ResetPassword resetPassword = new Classes.Request.Auth.ResetPassword();

            resetPassword.SetParam(mailEntry.Text);
            try
            {
                var result = await resetPassword.GetJsonAsync();
                Popup p = this.Parent as Popup;
                HideAnnim.Begin();

                p.IsOpen = false;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                setError("Error Network");
                Debug.WriteLine(ex);
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                setError("Error Json");

                Debug.WriteLine(jEx);
            }


        }

        private void setError(string errorMessage)
        {
            ErrorTextBlock.Text = errorMessage;
            ErrorAnnimation.Begin();
        }

        private void SetSize(double height, double width)
        {
            this.Height = height - 20;
            this.Width = width - 20;
            try
            {
                Popup p = this.Parent as Popup;
                p.HorizontalOffset = (width / 2) - (this.Width / 2);
                p.VerticalOffset = (height / 2) - (this.Height / 2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
