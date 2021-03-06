﻿using NestedWorld.Classes.ElementsGame.Monsters;
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

namespace NestedWorld.View.AreaViews
{
    public sealed partial class AreaSwapMonster : UserControl
    {
        private Monster _monsterSelected;

        public Monster MonsterSelected
        {
            get { return _monsterSelected; }
            set
            {
                _monsterSelected = value;
                SelectedMonsterView.DataContext = _monsterSelected;
            }
        }

        public AreaSwapMonster()
        {
            this.InitializeComponent();
            
            UserMonsterGridView.DataContext = App.core.monsterUserList.monsterList;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //cancel
            Hide.Begin();
            (this.Parent as Popup).IsOpen = false;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //ok
            Hide.Begin();
            (this.Parent as Popup).IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MonsterSelected = UserMonsterGridView.SelectedItem as Monster;
            }
            catch
            {

            }
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
