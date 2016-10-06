using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class Item
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }


        public int AttackEffect { get; private set; }
        public int DeffEffect { get; private set; }
        public int LifeEffect { get; private set; }
        public int ExpEffect { get; private set; }


        public Item(string name, double price, string description, string image, int att, int deff, int life, int exp)
        {
            Name = name;
            Price = price;
            Description = description;
            Image = image;
            AttackEffect = att;
            DeffEffect = deff;
            LifeEffect = life;
            ExpEffect = exp;
        }

        public void Use()
        {

        }

        public void Buy()
        {

        }
    }
}
