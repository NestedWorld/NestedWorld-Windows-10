using NestedWorld.Classes.ElementsGame.Monsters;
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
    public sealed partial class MonsterCircleView : UserControl
    {
        private const int DEFAULTTOP = 150;

        public delegate void MonsterSelected(Monster monster);

        public event MonsterSelected OnMonsterSelected;

        private List<MonsterCirleItemView> itemList;

        public MonsterCircleView()
        {
            this.itemList = new List<MonsterCirleItemView>();

            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            this.DisplayItem();
            this.PortalRotate.Completed += PortalRotate_Completed;
            this.PortalRotate.Begin();
        }

        private void PortalRotate_Completed(object sender, object e)
        {
            this.PortalRotate.Begin();
        }

        private void DisplayItem()
        {
            this.itemList = new List<MonsterCirleItemView>();

            foreach (Monster m in App.core.monsterUserList.monsterList)
                this.itemList.Add(new MonsterCirleItemView(m));
            foreach (MonsterCirleItemView mv in this.itemList)
            {
                mv.OnItemClick += Mv_OnItemClick;
                this.CanvasDisplay.Children.Add(mv);
            }
            this.SetPlacement(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
        }

        private void Mv_OnItemClick(Monster monster)
        {
            this.OnMonsterSelected?.Invoke(monster);
        }

        private void SetPlacement(double height, double width)
        {
            if (itemList.Count == 0)
                return;

            double PidivTwo = (Math.PI / 2);
            double alpha = (2 * Math.PI) / itemList.Count;

            double defaultTop = ((height) / 2) - (itemList[0].Height / 2);
            double defaultLeft = ((width) / 2) - (itemList[0].Width / 2);
            int index = 0;

            foreach (MonsterCirleItemView item in itemList)
            {
                item.top = ((Math.Sin(PidivTwo + index * alpha)) * 150) + defaultTop - DEFAULTTOP;// - (CanvasDisplay.Height / 2);
                item.left = ((Math.Cos(PidivTwo + index * alpha)) * 150) + defaultLeft;// - (CanvasDisplay.Width / 2);
                index++;
            }
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.SetPlacement(e.Size.Height, e.Size.Width);
        }

        private void ellipse_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            Utils.Log.Info("ellipse_DropCompleted");
        }

        private void ellipse_Drop(object sender, DragEventArgs e)
        {
            Utils.Log.Info("ellipse_Drop");
        }

        private void Ellipse_DragEnter(object sender, DragEventArgs e)
        {
            Utils.Log.Info("Ellipse_DragEnter");
        }

        private void Ellipse_DragOver(object sender, DragEventArgs e)
        {
            Utils.Log.Info("Ellipse_DragOver");
        }
    }
}
