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

namespace NestedWorld.UI
{
    public sealed partial class SearchBox : UserControl
    {
        public string Text { get { return textBoxSearch.Text; } set { } }
        public TextChangedEventHandler Event { get { return null; } set { textBoxSearch.TextChanged += value; } }


        public SearchBox()
        {
            this.InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ShowAnnimation.Begin();
        }

        private void Button_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            HideAnimation.Begin();
        }
    }
}
