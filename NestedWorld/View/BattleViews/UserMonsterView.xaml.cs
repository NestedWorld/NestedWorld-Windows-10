using NestedWorld.Classes.ElementsGame.Monsters;
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
    public sealed partial class UserMonsterView : UserControl
    {
        public UserMonsterView()
        {
            this.InitializeComponent();

            this.DataContextChanged += UserMonsterView_DataContextChanged;
        }

        private void UserMonsterView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Monster tmp = this.DataContext as Monster;

            if (tmp != null)
                Log.Info("monster : ", tmp.Name);
        }
    }
}
