using NestedWorld.Classes.ElementsGame.Portals;
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
    public sealed partial class PortalListView : UserControl
    {
        public delegate void PortalListSelected(Portal portal);

        public event PortalListSelected OnPortalSelected;

        public PortalListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += PortalListView_DataContextChanged;
        }

        private void PortalListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.DataContext == null)
                return;
            this.list.DataContext = (this.DataContext as PortalList).content;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.OnPortalSelected?.Invoke((list.SelectedItem as Portal));
            }
            catch(Exception ex)
            {
                Utils.Log.Error(ex);
            }
        }
    }
}
