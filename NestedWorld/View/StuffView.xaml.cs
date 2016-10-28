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

namespace NestedWorld.View
{
    public sealed partial class StuffView : UserControl
    {
        private ItemList _itemList;

        public ItemList itemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                _itemList = value;
                this.DataContext = value;
            }
        }

        public StuffView()
        {
            this.InitializeComponent();
        }

        public void Init()
        {
            this.itemList = App.core.itemsList;
        }

        private void ItemsListView_OnItemSelected(Item item)
        {
            Utils.Log.Info(item.Name);
            this.Use.DataContext = item;
            this.Use.Show();
        }
    }
}
