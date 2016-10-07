using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Utils.AppData
{
    public class Composite
    {
        private Windows.Storage.ApplicationDataContainer localSettings
        { get { return Windows.Storage.ApplicationData.Current.LocalSettings; } set { } }

        private Windows.Storage.StorageFolder localFolder
        { get { return Windows.Storage.ApplicationData.Current.LocalFolder; } set { } }


        private string _key;
        private Windows.Storage.ApplicationDataCompositeValue _composite;

        public Composite(string key)
        {
            this._key = key;
        }

        private void Load()
        {
            _composite = (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values[_key];
            if (_composite == null)
                _composite = new Windows.Storage.ApplicationDataCompositeValue();
        }

        public void Save()
        {
            localSettings.Values[_key] = _composite;
        }

        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }
        }

        private void Add(string key, object value)
        {
            _composite[key] = value;
        }

        private object Get(string key)
        {
            object value;
            if (_composite.TryGetValue(key, out value))
            {
                Utils.Log.Info(key, value);
                return value;

            }
            return null;
        }

        public static Composite Instance(string name)
        {
            Composite ret = new Composite(name);
            ret.Load();
            return ret;
        }

        public override string ToString()
        {
            string ret = "key " + this._key + "=>";

            foreach (string s in _composite.Keys)
            {
                ret += s + " = " + _composite[s];
            }
            return ret;
        }
    }
}
