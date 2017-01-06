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

namespace NestedWorld.View.PrepareBattleView
{
    public sealed partial class SelectedMonsterView : UserControl
    {
        private Classes.ElementsGame.Monsters.MonsterList selectedMonster;

        public Classes.ElementsGame.Monsters.MonsterList SelectedMonster
        {
            get
            {
                return selectedMonster;
            }
            set
            {

            }
        }

        public SelectedMonsterView()
        {
            this.InitializeComponent();
            (root.Children[0] as SelectedMonsterItemView).isSelected = true;
            selectedMonster = new Classes.ElementsGame.Monsters.MonsterList();
        }

        public void Select(Classes.ElementsGame.Monsters.Monster monster)
        {

            for (int i = 0; i < root.Children.Count; i++)
            {
                if ((root.Children[i] as SelectedMonsterItemView).isSelected)
                {
                    selectedMonster.Add(monster, i);

                    (root.Children[i] as SelectedMonsterItemView).DataContext = monster;
                    this.SelectedMonsterItemView_OnSelectedEvent((root.Children[((i + 1) == root.Children.Count) ? 0 : i + 1] as SelectedMonsterItemView));
                    break;
                }
            }
        }

        private void SelectedMonsterItemView_OnSelectedEvent(SelectedMonsterItemView sender)
        {
            foreach (SelectedMonsterItemView item in root.Children)
            {
                item.isSelected = false;
            }
            sender.isSelected = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedMonster = new Classes.ElementsGame.Monsters.MonsterList();
            for (int i = 0; i < root.Children.Count; i++)
            {
                (root.Children[i] as SelectedMonsterItemView).DataContext = null;
                (root.Children[i] as SelectedMonsterItemView).isSelected = false;
            }
            (root.Children[0] as SelectedMonsterItemView).isSelected = true;

        }
    }
}
