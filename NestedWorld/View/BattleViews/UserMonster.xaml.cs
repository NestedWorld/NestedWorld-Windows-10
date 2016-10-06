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
    public sealed partial class UserMonster : UserControl
    {

        public double Life
        {
            get { return 0; }
            set
            {
                LifeBar.Width = value * 315 / 100;

                LifeBar.Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3"));
                if (value < 20)
                    LifeBar.Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FFF44336"));
            }
        }
        public UserMonster()
        {
            this.InitializeComponent();
            this.DataContextChanged += EnemieMonster_DataContextChanged;
        }

        private void EnemieMonster_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Monster monster = DataContext as Monster;

            Life = (double)monster.Life;
        }
    }
}
