using Microsoft.Graphics.Canvas.Effects;
using NestedWorld.Classes.ElementsGame.Attack;
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
using System.Threading;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.BattleViews
{
    public sealed partial class AnnimationCanvas : UserControl
    {
        private Attack Attack;

        public Attack Next { get;  set; }

        public AnnimationCanvas()
        {
            this.InitializeComponent();

            this.informationBatteView.okbutton.Click += Okbutton_Click;

            this.DataContextChanged += AnnimationCanvas_DataContextChanged;

            Next = null;
        }

        private void Okbutton_Click(object sender, RoutedEventArgs e)
        {
            this.informationBatteView.SpriteAnnimation.Stop();
            this.Visibility = Visibility.Collapsed;
            if (Next != null)
            {

                this.DataContext = Next;
            }
            Next = null;
        }

        private void AnnimationCanvas_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this.DataContext == null)
                return;
            this.Attack = this.DataContext as Attack;
            Show();
        }

        private void Show()
        {
            this.informationBatteView.SpriteAnnimation.DataContext = 
                App.core.Resources.AttackSprite.GetSprite(Attack.AttackRessourcesName);
            this.informationBatteView.header.Text = Attack.Name;
            this.Visibility = Visibility.Visible;

        }
    }
}
