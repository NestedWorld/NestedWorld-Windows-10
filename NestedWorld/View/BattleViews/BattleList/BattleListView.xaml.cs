using NestedWorld.UI;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace NestedWorld.View.BattleViews.BattleList
{
    public sealed partial class BattleListView : UserControl
    {
        public BattleListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += BattleListView_DataContextChanged;
        }

        private void BattleListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Classes.ElementsGame.Battle.BattleList bl = DataContext as Classes.ElementsGame.Battle.BattleList;
            try
            {
                this.list.DataContext = bl.content;
                this.titleList.Text = bl.header;
                Utils.Log.Info("DataContextChanged", bl);
            }
#pragma warning disable CS0168
            catch (Exception ex)
            {

            }
#pragma warning restore CS0168
        }
    }
}
