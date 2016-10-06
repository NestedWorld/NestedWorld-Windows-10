using NestedWorld.Classes.ElementsGame.Maps;
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

namespace NestedWorld.View.AreaViews
{
    public sealed partial class AreaItemView : UserControl
    {
        public Area area { get; set; }

        public AreaItemView()
        {
            this.InitializeComponent();
            this.DataContextChanged += AreaItemView_DataContextChanged;
        }

        private void AreaItemView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void profilButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
