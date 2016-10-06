using NestedWorld.View.MapViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NestedWorld.View
{
    public sealed partial class MapView : UserControl
    {
        public MapView()
        {
            this.InitializeComponent();
            App.core.MapController.MapControl = mapControlView.mapControl;
            App.core.MapController.GeolocatoreActivated = true;
        }

        public async void Init()
        {
            await App.network.GetPlaces();

            //await App.network.
        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitViewOption.IsPaneOpen = !splitViewOption.IsPaneOpen;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;

            switch (ts.Name)
            {
                case ("monsterShowSwitch"):
                    if (ts.IsOn)
                        App.core.MapController.ShowMonsterLocation();
                    else
                        App.core.MapController.ColapseMonsterLocation();
                    break;
                case ("areasShowSwitch"):
                    if (ts.IsOn)
                        App.core.MapController.ShowAreaLocation();
                    else
                        App.core.MapController.ColapseAreaLocation();
                    break;
                case ("alliesShowSwitch"):
                    if (ts.IsOn)
                        App.core.MapController.ShowAllyLocation();
                    else
                        App.core.MapController.ColapseAllyLocation();
                    break;
            }
        }
    }
}
