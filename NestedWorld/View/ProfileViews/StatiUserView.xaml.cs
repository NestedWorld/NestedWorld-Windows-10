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

namespace NestedWorld.View.ProfileViews
{
    public sealed partial class StatiUserView : UserControl
    {
        public StatiUserView()
        {
            this.InitializeComponent();
            this.DataContextChanged += StatiUserView_DataContextChanged;
        }

        private void StatiUserView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            ShowAnnim.Begin();
        }

        private void border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            WinEnter.Begin();
        }

        private void border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            WinExit.Begin();
        }

        private void Border_PointerEntered_1(object sender, PointerRoutedEventArgs e)
        {
            looseEnter.Begin();
        }

        private void Border_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            looseExit.Begin();
        }
    }
}
