using NestedWorld.Classes.ElementsGame.Monsters;
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

namespace NestedWorld.View.MonsterViews.MonsterTab
{
    public sealed partial class Content : UserControl
    {
        public Content()
        {
            this.InitializeComponent();
            this.DataContextChanged += Content_DataContextChanged;
        }

        private void Content_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.DataContext == null)
                return;
            try
            {
                listItemView.DataContext = (this.DataContext as MonsterList).content;
            }
            catch(Exception ex)
            {
                Utils.Log.Error("Content::Content_DataContextChanged", ex);
            }
        }
    }
}
