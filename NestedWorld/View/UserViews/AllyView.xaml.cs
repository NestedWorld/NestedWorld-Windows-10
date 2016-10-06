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

namespace NestedWorld.View.UserViews
{
    public sealed partial class AllyView : UserControl
    {
        public AllyView()
        {
            this.InitializeComponent();
            this.Loaded += AllyView_Loaded;
        }

        private void AllyView_Loaded(object sender, RoutedEventArgs e)
        {
            this.LevelTextBlock.Text += " lvl";
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;

            root.Navigate(typeof(Pages.ProfilePage), DataContext);
        }
    }
}
