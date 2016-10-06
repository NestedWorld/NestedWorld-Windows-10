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
            worldexchange.DataContext = new ShopMenu() { Name = "World Exchange", image = "ms-appx:///Assets/Shop/worldechange.jpg" };
            item2.DataContext = new ShopMenu() { Name = "Allies Exchange", image = "ms-appx:///Assets/Shop/alliesexchange.png" };
            item3.DataContext = new ShopMenu() { Name = "Nested Shop", image = "ms-appx:///Assets/NestedWorldLogo.png" };

        }
    }
}
