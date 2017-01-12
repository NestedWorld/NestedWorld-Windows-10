using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.View.BattleViews.BattleList;
using NestedWorldNotificationLib.Notifications;
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
                    TowerBattle.view = _view.portalBattle;
                }
            }
        }

        public BattleList WildBattle { get; set; }
        public BattleList OppBattle { get; set; }
        public BattleList TowerBattle { get; set; }

        public void Remove(Battle battle)
        {
            battle.Decline();
            switch (battle.ContextBattle)
            {
                case (Context.WILD):
                    WildBattle.Remove(battle);
                    WildBattle.view = _view.wildBattle;
                    break;
                case (Context.PVP):
                    OppBattle.Remove(battle);
                    OppBattle.view = _view.pvpBattle;
                    break;
                case (Context.TOWER):
                    TowerBattle.Remove(battle);
                    break;
            }
        }

        public BattleRouter()
        {
            WildBattle = new BattleList("PVE");
            OppBattle = new BattleList("PVP");
            TowerBattle = new BattleList("Portals");
            view = null;
        }

        public void Init()
        {
            App.network.serveurMessageList["combat:available"].OnCompled += CombatAvalaible_OnCompled;
        }

        private async void CombatAvalaible_OnCompled(object value)
        {
            NotificationToast toast = null;
            MessagePack.Serveur.Combat.Available av = value as MessagePack.Serveur.Combat.Available;

            if (av.origin == "wild-monster")
            {
                WildBattle.Add(new Battle() { OpponentImage = App.core.monsterList[av.monster_id].Image, OpponentName = App.core.monsterList[av.monster_id].Name, BattleID = av.id, ContextBattle = Context.WILD, StateBattle = State.AVALAIBLE });
                toast = new NotificationToast(ToastInputType.none)
                {
                    title = "A soul wants a fight",
                    content = string.Format("{0} appears, You want to fight ?", App.core.monsterList[av.monster_id].Name),
                    image = App.core.monsterList[av.monster_id].Image,
                    notificationId = av.id
                };
            }
            else if (av.origin == "duel")
            {
                var ret = await App.network.GetUser(av.user.Id);
                ret.ShowError();
                User user = ret.Content as User;
                OppBattle.Add(new Battle() { OpponentImage = user.Image, OpponentName = user.Name, BattleID = av.id, ContextBattle = Context.PVP, StateBattle = State.AVALAIBLE });
                toast = new NotificationToast(ToastInputType.none)
                {
                    title = "A allie wants a fight",
                    content = string.Format("{0} (level {1})challenges you, You want to fight ?", av.user.Name, 2),
                    image = user.Image,
                    notificationId = av.id
                };
            }
            else
            {
                TowerBattle.Add(new Battle() { OpponentImage = "", OpponentName = "Portal", BattleID = av.id, ContextBattle = Context.TOWER });
                
            }



            toast.Add(new NotificationAction()
            {
                Name = "okay-battle",
                Text = "Go",
                type = Microsoft.Toolkit.Uwp.Notifications.ToastActivationType.Background
            });
            toast.Add(new NotificationAction()
            {
                Name = "cancel-battle",
                Text = "Nope",
                type = Microsoft.Toolkit.Uwp.Notifications.ToastActivationType.Background
            });


            toast.Show();
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
                var ret = await App.network.GetUser(avaible.user.Id);

                OppBattle[avaible.id] = new Battle() { OpponentImage = (ret.Content as User).Image, OpponentName = avaible.user.Name, BattleID = avaible.id, ContextBattle = Context.PVP, StateBattle = State.AVALAIBLE };
                return OppBattle[avaible.id];
            }
            return null;
        }

    }
}
