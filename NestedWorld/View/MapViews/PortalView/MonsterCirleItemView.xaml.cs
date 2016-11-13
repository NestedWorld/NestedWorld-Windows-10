﻿using NestedWorld.Classes.ElementsGame.Monsters;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.MapViews.PortalView
{
    public sealed partial class MonsterCirleItemView : UserControl
    {
        public delegate void ItemClick(Monster monster);

        public event ItemClick OnItemClick;

        public double left { get { return 0; } set { Canvas.SetLeft(this, value); } }
        public double top { get { return 0; } set { Canvas.SetTop(this, value); } }

        public MonsterCirleItemView()
        {
            this.InitializeComponent();
            this.CanDrag = true;
            this.DragStarting += MonsterCirleItemView_DragStarting;
        }

        private void MonsterCirleItemView_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            Utils.Log.Info("drag");
        }

        public MonsterCirleItemView(Monster data)
        {
            this.InitializeComponent();
         //   this.CanDrag = true;
       //     this.DragStarting += MonsterCirleItemView_DragStarting;
            this.DataContext = data;
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                this.OnItemClick?.Invoke(this.DataContext as Monster);
            }
            catch(Exception ex)
            {
                Utils.Log.Error("MonsterCirleItemView::Grid_Tapped", ex);
            }
        }

        private void Ellipse_Drop(object sender, DragEventArgs e)
        {
            Utils.Log.Info("drop");
        }

        private void Ellipse_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            Utils.Log.Info("dropCompleted");
        }
    }
}
