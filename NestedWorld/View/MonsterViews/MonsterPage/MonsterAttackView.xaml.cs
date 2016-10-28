using NestedWorld.Classes.ElementsGame.Attack;
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
    public sealed partial class MonsterAttackView : UserControl
    {
        public MonsterAttackView()
        {
            this.InitializeComponent();
            this.DataContextChanged += MonsterAttackView_DataContextChanged;
        }

        private void MonsterAttackView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Attack attack = (DataContext as Attack);

            spriteAnnimation.DataContext = App.core.Resources.AttackSprite[attack.AttackRessourcesName];
        }
    }
}
