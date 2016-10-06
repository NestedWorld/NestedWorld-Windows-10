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

namespace NestedWorld.View.MonsterViews
{
    public sealed partial class MonsterInformationView : UserControl
    {
        public MonsterInformationView()
        {
            this.InitializeComponent();
            this.DataContextChanged += MonsterInformationView_DataContextChanged;
            //Window.Current.SizeChanged += Current_SizeChanged;
            //SetSize(Window.Current.Bounds.Height, Window.Current.Bounds.Height);
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetSize(e.Size.Height, e.Size.Width);
        }

        private void SetSize(double height, double width)
        {
            this.Width = width;
        }

        private void MonsterInformationView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext == null)
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Visible;
                hearderView.DataContext = this.DataContext;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }
    }
}
