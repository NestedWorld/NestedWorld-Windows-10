using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.Utils;
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
    public sealed partial class AddAllyPopUp : UserControl
    {
        public AddAllyPopUp()
        {
            this.InitializeComponent();
            this.Loaded += RegisterPopUp_Loaded;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetSize(e.Size.Height, e.Size.Width);
        }

        private void SetSize(double height, double width)
        {
            try
            {
                Popup p = this.Parent as Popup;
                p.HorizontalOffset = (width / 2) - (this.Width / 2);
                p.VerticalOffset = (height / 2) - (this.Height / 2);
            }
            catch (Exception ex)
            {
                Log.Error("AddAllyPopUp", ex);
            }
        }

        private void RegisterPopUp_Loaded(object sender, RoutedEventArgs e)
        {
           // ShowAnnim.Begin();
           // SetSize(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Popup p = this.Parent as Popup;
           // HideAnnim.Begin();
            p.IsOpen = false;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var ret = await App.network.PostAllies(entry.Text);
            ret.ShowError();
            if (ret.ErrorCode != 0)
            {
                ErrorTextBlock.Text = ret.Message;
                ErrorAnnimation.Begin();
            }
            else
            {
                Info.Text = string.Format("{0} is now you ally", entry.Text);

                ret = await App.network.GetAllies();
                App.core.userList = ret.Content as UserList;
                ret.ShowError();

            }
        }
    }
}
