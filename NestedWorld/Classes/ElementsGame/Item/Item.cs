using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class Item
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public int id { get; set; }
        public bool Prenium { get; set; }


        public int AttackEffect { get; private set; }
        public int DeffEffect { get; private set; }
        public int LifeEffect { get; private set; }
        public int ExpEffect { get; private set; }

        public Item() { }

        public Item(Item source)
        {
            Name = source.Name;
            Price = source.Price;
            Description = source.Description;
            Image = source.Image;
            AttackEffect = source.AttackEffect;
            DeffEffect = source.DeffEffect;
            LifeEffect = source.LifeEffect;
            ExpEffect = source.ExpEffect;
        }

        public Item(string name, int price, string description, string image, int att, int deff, int life, int exp)
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

        public static Item NewFromJson(JObject obj)
        {
            string name = obj["name"].ToObject<string>();
            string image = obj["image"].ToObject<string>();
            string description = obj["description"].ToObject<string>();
            int price = obj["price"].ToObject<int>();
            bool prenium = obj["premium"].ToObject<bool>();
            int id = obj["id"].ToObject<int>();

            return new Item()
            {
                Name = name,
                Image = image,
                Description = description,
                Price = price,
                Prenium = prenium,
                id = id
            };



        }
    }
}
