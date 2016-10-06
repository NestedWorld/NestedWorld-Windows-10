using NestedWorld.View.BattleViews.BattleList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public class BattleRouter
    {
        public int Sum { get { return this.OppBattle.content.Count + this.WildBattle.content.Count + this.TowerBattle.content.Count; } set { } }

        private BattleListMainView _view;

        public BattleListMainView view
        {
            get { return _view; }
            set
            {
                _view = value;
                if (_view != null)
                {
                    WildBattle.view = _view.wildBattle;
                    OppBattle.view = _view.pvpBattle;
                }
            }
        }

        public BattleList WildBattle { get; set; }
        public BattleList OppBattle { get; set; }
        public BattleList TowerBattle { get; set; }

        public BattleRouter()
        {
            WildBattle = new BattleList("PVE");
            OppBattle = new BattleList("PVP");
            TowerBattle = new BattleList("Areas");
            view = null;
        }

        public void Init()
        {
            App.network.serveurMessageList["combat:available"].OnCompled += CombatAvalaible_OnCompled;
        }

        private void CombatAvalaible_OnCompled(object value)
        {
            MessagePack.Serveur.Combat.Available av = value as MessagePack.Serveur.Combat.Available;

            if (av.origin == "wild-monster")
            {
                WildBattle.Add(new Battle() { OpponentImage = App.core.monsterList[av.monster_id].Image, OpponentName = App.core.monsterList[av.monster_id].Name, BattleID = av.id, ContextBattle = Context.WILD, StateBattle = State.AVALAIBLE });
            }
            else if (av.origin == "duel")
            {
                OppBattle.Add(new Battle() { OpponentImage = "", OpponentName = av.user.Name, BattleID = av.id, ContextBattle = Context.PVP, StateBattle = State.AVALAIBLE });
            }
            else
            {

            }
        }

        public async Task<Battle> AddAndReturn(MessagePack.Serveur.Combat.Available avaible)
        {

            if (avaible.origin == "wild-monster")
            {
                WildBattle[avaible.id] = new Battle() { OpponentImage = App.core.monsterList[avaible.monster_id].Image, OpponentName = App.core.monsterList[avaible.monster_id].Name, BattleID = avaible.id, ContextBattle = Context.WILD, StateBattle = State.AVALAIBLE };
                return WildBattle[avaible.id];
            }
            else if (avaible.origin == "duel")
            {
                OppBattle[avaible.id] = new Battle() { OpponentImage = "", OpponentName = avaible.user.Name, BattleID = avaible.id, ContextBattle = Context.PVP, StateBattle = State.AVALAIBLE };
                return OppBattle[avaible.id];
            }
            return null;
        }
    }
}
