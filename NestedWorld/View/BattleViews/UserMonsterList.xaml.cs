using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Utils;
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
    public sealed partial class UserMonsterList : UserControl
    {
        private BattleController _controller;

        public BattleController controller
        {
            get { return _controller; }
            set
            {
                _controller = value;

                this.UserMonserGridView.DataContext = _controller.UserMonsters.content;
                this.UserMonserGridView.SelectedIndex = 0;
            }
        }

        public UserMonsterList()
        {
            this.InitializeComponent();
        }

        private void UserMonserGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //TODO: Change Monster
            //this.controller.
        }
    }
}
