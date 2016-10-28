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

namespace NestedWorld.View.MonsterViews.MonsterPage
{
    public sealed partial class MontserAttackListView : UserControl
    {
        public MontserAttackListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += MontserAttackListView_DataContextChanged;
        }

        private void MontserAttackListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Monster monster = (DataContext as Monster);
            attackList.DataContext = monster.attackList.list;
        }
    }
}
