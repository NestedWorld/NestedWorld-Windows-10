using NestedWorld.Classes.ElementsGame.Shop;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View
{

    public sealed partial class ShopView : UserControl
    {
        public ShopView()
        {
            InitializeComponent();
        }

        public void Init()
        {
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.exchangeView.Show();
        }

        private void Grid_Tapped_1(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.createExchangeView.Show();
        }
    }
}
