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

namespace NestedWorld.View.ProfileViews
{
    public sealed partial class MonsterStat : UserControl
    {
        public static readonly DependencyProperty WaterProperty = DependencyProperty.Register("waterValue", typeof(int), typeof(MonsterStat), null);
        public static readonly DependencyProperty WaterPorcentProperty = DependencyProperty.Register("waterPorcent", typeof(int), typeof(MonsterStat), null);

        public static readonly DependencyProperty FireProperty = DependencyProperty.Register("fireValue", typeof(int), typeof(MonsterStat), null);
        public static readonly DependencyProperty FirePorcentProperty = DependencyProperty.Register("firePorcent", typeof(int), typeof(MonsterStat), null);

        public static readonly DependencyProperty DirtProperty = DependencyProperty.Register("dirtValue", typeof(int), typeof(MonsterStat), null);
        public static readonly DependencyProperty DirtPorcentProperty = DependencyProperty.Register("dirtPorcent", typeof(int), typeof(MonsterStat), null);

        public static readonly DependencyProperty ElecProperty = DependencyProperty.Register("elecValue", typeof(int), typeof(MonsterStat), null);
        public static readonly DependencyProperty ElecPorcentProperty = DependencyProperty.Register("elecPorcent", typeof(int), typeof(MonsterStat), null);

        public static readonly DependencyProperty GrassProperty = DependencyProperty.Register("grassValue", typeof(int), typeof(MonsterStat), null);
        public static readonly DependencyProperty GrassPorcentProperty = DependencyProperty.Register("grassPorcent", typeof(int), typeof(MonsterStat), null);

        public Classes.ElementsGame.Users.StatsMonster Stats
        {
            get
            {
                return null;
            }
            set
            {
                SetValue(WaterProperty, value.water);
                SetValue(WaterPorcentProperty, (value.water * 100) / value.total);

                SetValue(FireProperty, value.fire);
                SetValue(FirePorcentProperty, (value.fire * 100) / value.total);

                SetValue(DirtProperty, value.earth);
                SetValue(DirtPorcentProperty, (value.earth * 100) / value.total);

                SetValue(ElecProperty, value.electric);
                SetValue(ElecPorcentProperty, (value.electric * 100) / value.total);

                SetValue(GrassProperty, value.plant);
                SetValue(GrassPorcentProperty, (value.plant * 100) / value.total);

            }
        }


        public MonsterStat()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}
