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

namespace NestedWorld.View.MapViews
{
    public sealed partial class MonsterMapList : UserControl
    {
        public MonsterMapList()
        {
            this.InitializeComponent();
            RemoveAnnimation.Completed += RemoveAnnimation_Completed;
            this.DataContextChanged += MonsterMapList_DataContextChanged;
        }

        private void MonsterMapList_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            listView.DataContext = this.DataContext;
        }

        private void RemoveAnnimation_Completed(object sender, object e)
        {
            root.Children.Remove(this);
        }

        public StackPanel root { get; internal set; }

        public void Show()
        {
            root.Children.Add(this);
            //ShowAnnimation.Begin();
        }

        public void Remove()
        {
            //  RemoveAnnimation.Begin();
            root.Children.Remove(this);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Remove();
        }
    }
}
