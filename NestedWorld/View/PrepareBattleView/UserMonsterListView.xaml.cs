using NestedWorld.Classes.ElementsGame.Monsters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class UserMonsterListView : UserControl
    {
        private MonsterList _monsterList;

        public MonsterList monsterList
        {
            get { return _monsterList; }
            set
            {
                _monsterList = value;
                userMonsterGridView.DataContext = new ObservableCollection<Monster>(value.monsterList);
            }
        }

        public MonsterList SelectedMonsterList { get; private set; }

        public UserMonsterListView()
        {
            this.InitializeComponent();
            SelectedMonsterList = new MonsterList();

        }

        private void GridView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = new StringBuilder();
          
            Monster m = e.Items[0] as Monster;
            items.Append(m.ID.ToString() + "\n");

            e.Data.SetText(items.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Copy;

        }

        private void TargetListView_DragOver(object sender, DragEventArgs e)
        {
           e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.Text)) ? DataPackageOperation.Copy : DataPackageOperation.Move;
        }

        private async void TargetListView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var def = e.GetDeferral();
                var s = await e.DataView.GetTextAsync();
                string[] items = s.Split('\n');
                View.MonsterSoloView msv = new MonsterSoloView();
                msv.DataContext = monsterList.GetMonsterByID(Convert.ToInt32(items[0]));
                SelectedMonsterList.Add(monsterList.GetMonsterByID(Convert.ToInt32(items[0])));

                msv.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                msv.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Center);
                Grid grid = sender as Grid;
                foreach (var element in grid.Children)
                {
                    View.MonsterSoloView msvelement = element as View.MonsterSoloView;
                    SelectedMonsterList.monsterList.Remove(msvelement.DataContext as Monster);
                    grid.Children.Remove(element);
                    
                }
                grid.Children.Add(msv);

           
                e.AcceptedOperation = DataPackageOperation.Copy;
                def.Complete();
                selectedMonsterGridView.DataContext = new ObservableCollection<Monster>(SelectedMonsterList.monsterList);
            }
        }
    }
}
