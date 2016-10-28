using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using NestedWorld.Classes.ElementsGame.Battle;
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

namespace NestedWorld.View.PrepareBattleView
{
    public sealed partial class Header : UserControl
    {
        private Battle _battle;

        public Classes.ElementsGame.Battle.Battle Battle
        {
            get { return _battle; }
            set { _battle = value; this.OpponentHeader.DataContext = value.Opponent; this.PlayerHeader.DataContext = App.core.user;}
        }

        public Header()
        {
            this.InitializeComponent();
        }

    }
}
