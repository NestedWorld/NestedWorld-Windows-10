using NestedWorld.Classes.ElementsGame.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NestedWorld.View.AreaViews
{
    public sealed partial class AreaMonsterView : UserControl
    {
        private int _selectedMonster;
        private AreaSwapMonster swapUI;

        public int selectiedMonster
        {
            get { return _selectedMonster; }
            set
            {
                _selectedMonster = value % 4;

                Debug.WriteLine(_selectedMonster);
                switch (_selectedMonster)
                {
                    case (0):
                        SelectOne.Begin();
                        break;
                    case (1):
                        SelectTwo.Begin();
                        break;
                    case (2):
                        SelectTree.Begin();
                        break;
                    case (3):
                        SelectFour.Begin();
                        break;
                }
                ShowMonster();
            }
        }

        private void ShowMonster()
        {
            try
            {

                NameMonster.Text = area.MonsterList.monsterList[selectiedMonster].Name;
                SurnameMonster.Text = area.MonsterList.monsterList[selectiedMonster].Surname;
                LevelMonster.Text = area.MonsterList.monsterList[selectiedMonster].Level.ToString() + "  lvl";
                profilButton.IsEnabled = true;
            }
            catch
            {
                profilButton.IsEnabled = false;
            }
        }

        private Area area { get; set; }
        public AreaMonsterView()
        {
            this.InitializeComponent();
            this.DataContextChanged += AreaMonsterView_DataContextChanged;
            this.PopUpView.Closed += PopUpView_Closed;
        }

        private void PopUpView_Closed(object sender, object e)
        {
            area.MonsterList.monsterList[selectiedMonster] = swapUI.MonsterSelected;
            itemOne.DataContext = area.MonsterList.monsterList[0];
            itemTwo.DataContext = area.MonsterList.monsterList[1];
            itemTree.DataContext = area.MonsterList.monsterList[2];
            itemFour.DataContext = area.MonsterList.monsterList[3];
            ShowMonster();
        }

        private void AreaMonsterView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            area = DataContext as Area;
            swapUI = new AreaSwapMonster();
            itemOne.DataContext = area.MonsterList.monsterList[0];
            itemTwo.DataContext = area.MonsterList.monsterList[1];
            itemTree.DataContext = area.MonsterList.monsterList[2];
            itemFour.DataContext = area.MonsterList.monsterList[3];
            selectiedMonster = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "Left")
                selectiedMonster--;
            else
                selectiedMonster++;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;

            root.Navigate(typeof(Pages.MonsterPage), area.MonsterList.monsterList[selectiedMonster]);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            swapUI.MonsterSelected = area.MonsterList.monsterList[selectiedMonster];

            PopUpView.VerticalOffset =-50;
 
            PopUpView.Child = swapUI;
            PopUpView.IsOpen = true;
            swapUI.Show.Begin();
        }
    }
}
