using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.ElementsGame.Stats;
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
    public sealed partial class MonsterSpecView : UserControl
    {
        public MonsterSpecView()
        {
            this.InitializeComponent();
            this.DataContextChanged += MonsterSpecView_DataContextChanged;
        }

        private void MonsterSpecView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Monster monster = (this.DataContext as Monster);

            lifeSpec.DataContext = new Charac() { Name = "Life", Value = monster.Life, ValueMax = monster.LifeMax };
            AttackSpec.DataContext = new Charac() { Name = "Attack", Value = monster.Attackeffect, ValueMax = 100 };
            AttackSpeSpec.DataContext = new Charac() { Name = "Attack Special", Value = monster.Attackeffect, ValueMax = 100 };
            DefSpec.DataContext = new Charac() { Name = "Defense", Value = monster.Defence, ValueMax = 100 };
            DefSpeSpec.DataContext = new Charac() { Name = "Defense Special", Value = monster.Defence, ValueMax = 100 };
            SpeedSpec.DataContext = new Charac() { Name = "Speed", Value = monster.Speed, ValueMax = 100 };
        }
    }
}
