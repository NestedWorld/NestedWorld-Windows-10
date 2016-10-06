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
    public sealed partial class MonsterStatsView : UserControl
    {

        public int Life
        {
            get { return 0; }
            set
            {
                LifeBar.Width = value;
                if (value < 25)
                    LifeBar.Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FFFF0000"));
            }
        }

        public int Exp { get { return 0; } set { ExpBar.Width = value; } }

        public MonsterStatsView()
        {
            this.InitializeComponent();
        }
    }
}
