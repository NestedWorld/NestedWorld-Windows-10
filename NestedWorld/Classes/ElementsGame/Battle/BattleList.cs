using NestedWorld.View.BattleViews.BattleList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NestedWorld.Classes.Exception;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public class BattleList
    {
        private BattleListView _view;

        private Dictionary<string, Battle> _map;

        public string header { get; private set; }
        public BattleListView view
        {
            get { return _view; }
            set
            {
                _view = value;
                if (_view != null)
                    _view.DataContext = this;
            }
        }

        public ObservableCollection<Battle> content
        {
            get
            {
                return new ObservableCollection<Battle>(_map.Values);
            }
            set { }
        }

        public BattleList(string header)
        {
            _map = new Dictionary<string, Battle>();
            this.header = header;
            this.view = null;
        }

        public void Add(Battle newBattle)
        {
            _map.Add(newBattle.BattleID, newBattle);
            // if (this.view != null)
            //   this.view.DataContext = this;
        }

        public void Remove(Battle rmBattle)
        {
            _map.Remove(rmBattle.BattleID);
        }

        public override string ToString()
        {
            var ret = header + " ";
            foreach (Battle b in _map.Values)
            {
                ret += b.OpponentName + " ";
            }
            return ret;
        }

        public Battle Get(string key)
        {
            Battle value;
            if (_map.TryGetValue(key, out value))
                return value;
            throw new Exception.InvalideMapKeyException(key);
        }

        public Battle this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(value);
            }
        }
    }
}
