using NestedWorld.Classes.DesignUtilities;
using NestedWorld.Classes.ElementsGame.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.MapViews
{
    public sealed partial class AreaInfoView : UserControl
    {

        private CircularPresentorItem presentorItem;
        public AreaInfoView(Area area)
        {
            this.InitializeComponent();
            this.DataContext = area;
            this.presentorItem = new CircularPresentorItem(area.Name, area.Image, area.ItemList, 400,
                100, 5);
            this.presentorItem.contenor = contenorGarden;
            this.presentorItem.Init();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Popup daddy = this.Parent as Popup;

            daddy.IsOpen = false;
        }
    }
}
