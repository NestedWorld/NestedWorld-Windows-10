using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.Stats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NestedWorld.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MonsterPage : Page
    {
        public MonsterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= MonsterPage_BackRequested;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MonsterPage_BackRequested;
            base.OnNavigatedFrom(e);
            try
            {
                Monster monster = e.Parameter as Monster;
                monster.LoadAttack();
                this.DataContext = monster;

                if (!monster.PlayerMonster)
                {

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void MonsterPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                e.Handled = true;
            }
        }
    }
}
