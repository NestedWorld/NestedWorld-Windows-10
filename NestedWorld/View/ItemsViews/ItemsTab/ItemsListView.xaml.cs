using NestedWorld.Classes.ElementsGame.Item;
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

namespace NestedWorld.View.ItemsViews.ItemsTab
{
    public sealed partial class ItemsListView : UserControl
    {
        public delegate void ItemSelected(Item item);

        public event ItemSelected OnItemSelected;

        public ItemsListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += ItemsListView_DataContextChanged;
        }

        private void ItemsListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.DataContext == null)
                return;

            try
            {
                listview.DataContext = (this.DataContext as Inventory).content;
            }
            catch (Exception ex)
            {
                Utils.Log.Error("ItemsListView::ItemsListView_DataContextChanged", ex);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.OnItemSelected?.Invoke(listview.SelectedItem as Item);
            }
            catch (Exception ex)
            {
                Utils.Log.Error("ItemsListView::ListView_SelectionChanged", ex);
            }
        }
    }
}
