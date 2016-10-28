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
    public sealed partial class SelectedMonsterItemView : UserControl
    {

        private const double select = 60;

        private bool _isSelected;

        public delegate void OnSelected(SelectedMonsterItemView item);

        public event OnSelected OnSelectedEvent;

        public bool isSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if (value)
                {
                    this.Width = select * 1.5;
                    this.Height = select * 1.5;
                }
                else
                {
                    this.Width = select;
                    this.Height = select;
                }
            }
        }

        public SelectedMonsterItemView()
        {
            this.InitializeComponent();
            this.isSelected = false;
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OnSelectedEvent?.Invoke(this);
        }
    }
}
