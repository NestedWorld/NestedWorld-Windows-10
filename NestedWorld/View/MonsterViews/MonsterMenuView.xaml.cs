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

namespace NestedWorld.View.MonsterViews
{
    public sealed partial class MonsterMenuView : UserControl
    {

        public MonsterList monsterList;

        public MonsterMenuView()
        {
            this.InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*  switch (((sender as ComboBox).SelectedItem as TextBlock).Name)
              {
                  case ("ByName"):
                      monsterList = new MonsterList(monsterList.monsterListByName);
                      break;
                  case ("ByLevel"):
                      monsterList = new MonsterList(monsterList.monsterListByLevel);
                      break;
                  case ("ByType"):
                      monsterList = new MonsterList(monsterList.monsterListByType);
                      break;
                  case ("ByID"):
                      monsterList = new MonsterList(monsterList.monsterListByID);
                      break;
              }*/
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            /*      if ((sender as ToggleSwitch).IsOn)
                  {
                      monsterList = new MonsterList(App.core.monsterList.monsterList);
                  }
                  else
                  {
                      monsterList = new MonsterList(App.core.monsterUserList.monsterList);
                  }*/
        }
    }
}
