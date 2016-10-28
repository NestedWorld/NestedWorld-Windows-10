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
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.PrepareBattleView
{
    public sealed partial class UserMonsterList : UserControl
    {
        public Classes.ElementsGame.Monsters.Monster selectedMonster
        {
            get
            {
                var tmp = (UserMonsterGridView.SelectedItem as Classes.ElementsGame.Monsters.Monster);
                UserMonsterGridView.SelectedIndex = (UserMonsterGridView.SelectedIndex + 1) >= App.core.monsterUserList.monsterList.Count ? 0 : UserMonsterGridView.SelectedIndex + 1;
                return tmp;
            }
            set
            {

            }
        }

        public UserMonsterList()
        {
            this.InitializeComponent();
            this.UserMonsterGridView.DataContext = App.core.monsterUserList.content;
        }


        private void UserMonsterGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserMonsterGridView.SelectedIndex == -1)
                return;

            StackIndicator.Children.Clear();

            for (int i = 0; i < App.core.monsterUserList.monsterList.Count; i++)
            {
                if (i == UserMonsterGridView.SelectedIndex)
                {
                    StackIndicator.Children.Add(new Ellipse()
                    {
                        Height = 10,
                        Width = 10,
                        Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Stroke = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Margin = new Thickness(2.5)
                    });
                }
                else
                {
                    StackIndicator.Children.Add(new Ellipse()
                    {
                        Height = 10,
                        Width = 10,
                        Fill = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FFFFFFFF")),
                        Stroke = new SolidColorBrush(Utils.ColorUtils.GetColorFromHex("#FF2196F3")),
                        Margin = new Thickness(2.5)
                    });
                }
            }
        }
    }
}
