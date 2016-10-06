using NestedWorld.Classes.ElementsGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.MonsterViews
{
    public sealed partial class MonsterHeaderView : UserControl
    {
        public static readonly DependencyProperty MonsterNameProperty = DependencyProperty.Register("MonsterName", typeof(string), typeof(MonsterHeaderView), null);
        public static readonly DependencyProperty MonsterImageProperty = DependencyProperty.Register("MonsterImage", typeof(string), typeof(MonsterHeaderView), null);
        public static readonly DependencyProperty MonsterTypeProperty = DependencyProperty.Register("MonsterType", typeof(Color), typeof(MonsterHeaderView), null);
        public static readonly DependencyProperty MonsterCombatProperty = DependencyProperty.Register("MonsterCombat", typeof(string), typeof(MonsterHeaderView), null);
        public static readonly DependencyProperty MonsterLevelProperty = DependencyProperty.Register("MonsterLevel", typeof(string), typeof(MonsterHeaderView), null);
        public static readonly DependencyProperty MonsterVictoryProperty = DependencyProperty.Register("MonsterVictory", typeof(string), typeof(MonsterHeaderView), null);

        public string MonsterName
        {
            get
            {
                return GetValue(MonsterNameProperty) as string;
            }
            set
            {
                SetValue(MonsterNameProperty, value);
            }
        }
        public string MonsterImage
        {
            get { return GetValue(MonsterImageProperty) as string; }
            set { SetValue(MonsterImageProperty, value); }
        }

        public TypeEnum MonsterType
        {
            get { return TypeEnum.FIRE; }
            set { SetValue(MonsterTypeProperty, Utils.ColorUtils.GetColorFromHex(Utils.ColorUtils.GetTypeColor(value))); }
        }

        public int MonsterCombat
        {
            get { return 0; }
            set { SetValue(MonsterCombatProperty, value.ToString()); }
        }

        public int MonsterLevel
        {
            get { return 0; }
            set { SetValue(MonsterLevelProperty, value.ToString() + " lvl"); }
        }

        public int MonsterVictory
        {
            get { return 0; }
            set { SetValue(MonsterVictoryProperty, value.ToString()); }
        }
        public MonsterHeaderView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}
