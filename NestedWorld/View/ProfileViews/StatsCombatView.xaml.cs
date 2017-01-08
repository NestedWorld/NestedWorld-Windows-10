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
using NestedWorld.Classes.ElementsGame.Users;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.ProfileViews
{
    public sealed partial class StatsCombatView : UserControl
    {

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("titlestring", typeof(string), typeof(StatsCombatView), null);
        public static readonly DependencyProperty DefeateProperty = DependencyProperty.Register("defporcent", typeof(int), typeof(StatsCombatView), null);
        public static readonly DependencyProperty DefeateRotateProperty = DependencyProperty.Register("defrotate", typeof(int), typeof(StatsCombatView), null);
        public static readonly DependencyProperty VictoryProperty = DependencyProperty.Register("vicporcent", typeof(int), typeof(StatsCombatView), null);

        public string title
        {
            get
            {
                return "";
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        private int total;

        private int defRot
        {
            get
            {
                return 0;
            }
            set
            {
                SetValue(DefeateRotateProperty, value);
            }
        }

        private int def
        {
            get { return 0; }
            set
            {
                SetValue(DefeateProperty, (value * 100) / total);
                defRot = 360 - ((((value * 100) / total) * 360) / 100);
            }
        }
        private int vic
        {
            get { return 0; }
            set
            {
                SetValue(VictoryProperty, (value * 100) / total);
            }
        }
        public StatsCombatView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public void SetValue(int defeats, int victories)
        {
            this.total = defeats + victories;
            this.def = defeats;
            this.vic = victories;
        }

    }
}
