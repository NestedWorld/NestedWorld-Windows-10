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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NestedWorld.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingPage : Page
    {
        List<string> states = new List<string>
        {
            "Loading souls data",
            "Setup the map portal",
            "Connect to Nested Quantum Computer",
            "Welcome on Nested World"
        };

        private int _index;

        private int index
        {
            get
            {
                return _index;
            }
            set
            {
                if (value >= states.Count)
                    _index = 0;
                else
                    _index = value;
                stateTextBox.Text = states[index];
            }
        }

        private DispatcherTimer timer = new DispatcherTimer();

        public LoadingPage()
        {
            this.InitializeComponent();
             this.timer.Interval = TimeSpan.FromSeconds(1.5);
            this.timer.Tick += Timer_Tick;
            this.HideState.Completed += ShowState_Completed;
            LoadingAnnim.Completed += LoadingAnnim_Completed;
        }

        private void LoadingAnnim_Completed(object sender, object e)
        {
            LoadingAnnim.Begin();
        }

        private void ShowState_Completed(object sender, object e)
        {
            index++;
            ShowState.Begin();
        }

        private void Timer_Tick(object sender, object e)
        {
            HideState.Begin();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            index = 0;
            timer.Start();
            LoadingAnnim.Begin();
            var ret = await App.UserSession.Connect();
            if (ret.IsError)
                Frame.Navigate(typeof(MainPage));
           
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

            base.OnNavigatedFrom(e);
            LoadingAnnim.Stop();
            timer.Stop();
        }
    }
}
