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

namespace NestedWorld.View.BattleViews
{
    public sealed partial class EnemieMonster : UserControl
    {
        private const int LIFEBARMAX = 300;

        private Monster _monster;

        public double Life
        {
            get { return 0; }
            set
            {
                double lifeRend = value * LIFEBARMAX / Monster.LifeMax;
                
                LifeBar.Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3"));
                if (LifeBar.Width < 20)
                    LifeBar.Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FFF44336"));
            }
        }

        public Monster Monster
        {
            get { return _monster; }
            set
            {
                _monster = value;
                DataContext = value;
                Life = _monster.Life;
            }
        }

        public EnemieMonster()
        {
            this.InitializeComponent();
        }
    }
}
