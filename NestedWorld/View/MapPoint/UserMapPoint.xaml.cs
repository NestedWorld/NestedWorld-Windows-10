﻿using System;
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

namespace NestedWorld.View.MapPoint
{
    public sealed partial class UserMapPoint : UserControl
    {
        public UserMapPoint()
        {
            this.InitializeComponent();
            isShow = false;
        }

        bool isShow;

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (isShow)
            {
                ShowAnnimation.Begin();
            }
            else
                ColapseAnnimation.Begin();
            isShow = !isShow;

        }
    }
}
