using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.RootView
{
    public sealed partial class InternetStatueView : UserControl
    {
        private CoreDispatcher dispatcher;
        public InternetStatueView()
        {
            this.InitializeComponent();
            dispatcher = Window.Current.Dispatcher;
        }

        public void Show()
        {
            Page current = Window.Current.Content as Page;


        }

        public async void Hide()
        {
            if (dispatcher == null)
                return;
            await dispatcher.RunAsync(CoreDispatcherPriority.Low, new DispatchedHandler(
              () =>
              {
                  this.Visibility = Visibility.Collapsed;
              }));
        }
    }
}
