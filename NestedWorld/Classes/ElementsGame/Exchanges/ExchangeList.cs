using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Exchanges
{
    public class ExchangeList
    {
        private Dictionary<int, Exchange> _list;

        public ObservableCollection<Exchange> content { get { return new ObservableCollection<Exchange>(_list.Values); } set { } }

        public ExchangeList()
        {
            _list = new Dictionary<int, Exchange>();
        }

        public void Add(Exchange newExchange)
        {
            this._list.Add(newExchange.Id, newExchange);
        }

        public void Remove(int id)
        {
            this._list.Remove(id);
        }
        public void Remove(Exchange exchange)
        {
            this._list.Remove(exchange.Id);
        }

        public Exchange Get(int id)
        {
            Exchange value = null;

            if (_list.TryGetValue(id, out value))
                return value;
            Utils.Log.Error("ExchaneList::Get", "No exchange found", "id : ", id);
            return null;
        }

        public Exchange this[int key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                this.Add(value);
            }
        }

        public static ExchangeList LoadJson(JObject obj)
        {
            ExchangeList ret = new ExchangeList();

            JArray array = obj["exchanges"].ToObject<JArray>();

            foreach(JObject exchangeObj in array)
            {
                ret.Add(Exchange.LoadJson(exchangeObj));
            }

            return ret;
        }
    }
}
