using NestedWorld.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NestedWorld.ViewModel
{
    public class HomePageController
    {
        public HomePage homePage { get; set; }

        public HomePageController(HomePage page)
        {
            homePage = page;

            homePage.RootPivot.SelectionChanged += RootPivot_SelectionChanged;
        }

        private void RootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;

            foreach (PivotItem th in pivot.Items)
            {
                (th.Header as NestedWorld.Style.TabHeader).isSelect = false;
            }

            PivotItem header = pivot.SelectedItem as PivotItem;
            (header.Header as NestedWorld.Style.TabHeader).isSelect = true;

            switch (pivot.SelectedIndex)
            {
                case (0):
                    homePage.mainView.Init();
                    break;
                case (1):
                    homePage.MonsterView.Init();
                    break;
                case (2):
                    homePage.MapView.Init();
                    break;
                case (3):
                    homePage.EventView.Init();
                    break;
                case (4):
                    homePage.ShopView.Init();
                    break;
                default:
                    break;
            }
        }
    }
}
