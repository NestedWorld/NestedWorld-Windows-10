using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public class DataContextNull
    {
        public Color color { get { return (Utils.ColorUtils.GetColorFromHex("#FF616668")); } set { } }
        public string Image { get { return "ms-appx:///Assets/none_monster.png"; } set { } }
    }

    public sealed partial class AreaMonsterItemView : UserControl
    {
        public bool show
        {
            get { return true; }
            set
            {
                if (!value)
                    DataContext = null;      
            }
        }
        public AreaMonsterItemView()
        {
            this.InitializeComponent();
            this.DataContextChanged += AreaMonsterItemView_DataContextChanged;
        }

        private void AreaMonsterItemView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext == null)
            {
                this.DataContext = new DataContextNull();
            }

        }
    }
}
