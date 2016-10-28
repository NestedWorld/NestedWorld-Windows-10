using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class MonsterListView : UserControl
    {
        public delegate void MonsterSelected(Monster value);

        public event MonsterSelected OnMonsterSelected;



        public MonsterListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += MonsterListView_DataContextChanged;
        }


        private void MonsterListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            try
            {
                if (this.DataContext != null)
                    MonsterGridView.DataContext = new ObservableCollection<Monster>(this.DataContext as List<Monster>);
            }
            catch (Exception ex)
            {
                Log.Error("MonsterListView_DataContextChanged", ex);
            }
        }

        private void MonsterGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnMonsterSelected?.Invoke((MonsterGridView.SelectedItem as Monster));
            }
            catch (Exception ex)
            {
                Log.Warning("MonsterGridView_SelectionChanged, exception", ex);
            }
        }
    }
}
