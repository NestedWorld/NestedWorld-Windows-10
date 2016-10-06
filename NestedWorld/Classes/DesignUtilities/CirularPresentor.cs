using NestedWorld.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NestedWorld.Classes.DesignUtilities
{
    public class CirularPresentor
    {
        #region private att
        private Canvas _canvas;
        private bool addButonEnemable;
        private CircularItemAdd addButton;
        private CircularContenor _contenor;
        #endregion

        #region public att
        public Canvas canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                _canvas = value;
                _canvas.Children.Add(addButton);
               
            }
        }
        public List<CircularItem> ListDisplay { get; set; }
        public double Size { get; private set; }
        public double Left { get; private set; }
        public double Top { get; private set; }
        public string NamePresentor { get; private set; }
        public string ImagePresentor { get; private set; }

        public int ItemCount
        {
            get
            {
                if (addButonEnemable)
                    return CirculatItemList.Count + 1;
                return CirculatItemList.Count;
            }
            set { int i = 0; i++; }
        }
        public CircularContenor contenor
        {
            get
            {
                return _contenor;
            }
            set
            {
                _contenor = value;
                value.DataContext = this;
                _canvas = value.mainCanvas;

            }
        }
        public List<CircularItem> CirculatItemList { get; set; }
        #endregion
        public CirularPresentor(string namePresentor, string imagePresentor, double Size, double Top = 0.0f, double Left = 0.0f, bool AddButon = false)
        {
            this.addButonEnemable = AddButon;
            this.Size = Size;
            this.Top = Top;
            this.Left = Left;
            this.NamePresentor = namePresentor;
            this.ImagePresentor = imagePresentor;

            CirculatItemList = new List<CircularItem>();
            addButton = new CircularItemAdd(Add_Element);

        }

        protected void Add(CircularItem newItem)
        {
            CirculatItemList.Add(newItem);
            _canvas.Children.Add(newItem);
            Display();
        }

        protected void Display()
        {
            if (ItemCount == 0)
               return;

            double PidivTwo = (Math.PI / 2);
            double alpha = (2 * Math.PI) / ItemCount;
            double defautTop = ((Size) / 2) - 75 + Top;
            double defautLeft = ((Size) / 2) - 75 + Left;
            int index = 0;

            foreach (CircularItem item in CirculatItemList)
            {
                item.top = ((Math.Sin(PidivTwo + index * alpha)) * 175) + defautTop;
                item.left = ((Math.Cos(PidivTwo + index * alpha)) * 175) + defautLeft;
                Debug.WriteLine(item.top + "  " + item.left);
                index++;
            }
            if (addButonEnemable)
            {
                addButton.top = ((Math.Sin(PidivTwo + index * alpha)) * 175) + defautTop;
                addButton.left = ((Math.Cos(PidivTwo + index * alpha)) * 175) + defautLeft;

            }
        }

        private void Add_Element(object sender, RoutedEventArgs e)
        {

        }



    }
}
