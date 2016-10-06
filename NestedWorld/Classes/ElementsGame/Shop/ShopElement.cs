using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace NestedWorld.Classes.ElementsGame.Shop
{

    public class ShopElement
    {
        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public ShopElement(string uniqueId, string title, string subtitle, string image, string description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.Image = image;
        }
    }

    public class ShopGroup
    {
        public ShopGroup(string name, string uniqueID, string image, string description)
        {
            Name = name;
            UniqueId = UniqueId;
            Image = image;
            Description = description;
            Items = new ObservableCollection<ShopElement>();
        }
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }
        public string UniqueId { get; private set; }

        public ObservableCollection<ShopElement> Items { get; private set; }


        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class ShopDataSource
    {
        private static ShopDataSource _sampleDataSource = new ShopDataSource();

        private ObservableCollection<ShopGroup> _groups = new ObservableCollection<ShopGroup>();
        public ObservableCollection<ShopGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<ShopGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<ShopGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<ShopElement> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                ShopGroup group = new ShopGroup(groupObject["Title"].GetString(), groupObject["UniqueId"].GetString(), groupObject["Image"].GetString(), groupObject["Description"].GetString());

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new ShopElement(itemObject["UniqueId"].GetString(), itemObject["Title"].GetString(), itemObject["Subtitle"].GetString(), itemObject["Image"].GetString(), itemObject["Description"].GetString()));
                }
                Groups.Add(group);
            }
        }
    }
}
