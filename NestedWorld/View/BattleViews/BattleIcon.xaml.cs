using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.BattleViews
{
    public sealed partial class BattleIcon : UserControl
    {
        public static readonly DependencyProperty UserImageProperty = DependencyProperty.Register("ImageItem", typeof(string), typeof(BattleIcon), null);

        public string ImageItem
        {
            get { return GetValue(UserImageProperty) as string; }
            set { SetValue(UserImageProperty, value); }

        }
        public double left { get { return 0; } set { Canvas.SetLeft(this, value); } }
        public double top { get { return 0; } set { Canvas.SetTop(this, value); } }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value)
                    Active.Begin();
                else
                    UnActive.Begin();
                _isActive = value;
            }
        }

        public bool IsOnIt(Point point)
        {
            return ((Canvas.GetLeft(this) - this.Width) < point.X && (Canvas.GetLeft(this) + this.Width) > point.X)
               && ((Canvas.GetTop(this) - this.Height) < point.Y && (Canvas.GetTop(this) + this.Height) > point.Y);
        }
        public int number;
        private bool _isActive;

        public BattleIcon(string image = "", int number = 0)
        {
            this.InitializeComponent();
            this.DataContext = this;
            ImageItem = image;
            this.number = number;
            numberText.Text = number.ToString();
        }
    }
}
