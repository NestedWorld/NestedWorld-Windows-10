﻿using System;
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
    public sealed partial class BattleListMainView : UserControl
    {
        public BattleListMainView()
        {
            this.InitializeComponent();
            App.core.battleRouter.view = this;
        }

        public void Init()
        {
//            this.DataContext = App.core.battleRouter;
            this.wildBattle.DataContext = App.core.battleRouter.WildBattle;
            this.pvpBattle.DataContext = App.core.battleRouter.OppBattle;
            this.portalBattle.DataContext = App.core.battleRouter.TowerBattle;
        }
    }
}
