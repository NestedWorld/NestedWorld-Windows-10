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

namespace NestedWorld.View.BattleViews.BattleList
{
    public sealed partial class BattleElemtView : UserControl
    {
        public BattleElemtView()
        {
            this.InitializeComponent();
        }

        public void Accept(object sender, RoutedEventArgs e)
        {
            Classes.ElementsGame.Battle.Battle battle = this.DataContext as Classes.ElementsGame.Battle.Battle;

            battle.Accept();
        }

        public void Refuse()
        {

        }
    }
}
