using NestedWorld.Classes.ElementsGame.Item;
using System.Collections.Generic;

namespace NestedWorld.Classes.DesignUtilities
{
    public class CircularPresentorItem : CirularPresentor
    {
        public List<Item> ItemList { get; private set; }

        public CircularPresentorItem(string namePresentor, string imagePresentor, List<Item> itemList, double Size, double Top = 0.0f, double Left = 0.0f) 
            : base(namePresentor, imagePresentor, Size, Top, Left, true)
        {
            this.ItemList = itemList;
        }

        public void Init()
        {
            foreach (Item item in ItemList)
            {
                Add(item);
            }
        }

        public void Add(Item item)
        {
            Add(new UI.CircularItem(item));
        }
    }
}
