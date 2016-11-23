using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public enum State
    {
        USERTURN,
        OPPTURN,
        AVALAIBLE
    }

    public enum Context
    {
        WILD,
        PVP,
        TOWER
    }

    public class BattleOpponent
    {
        public string name { get; set; }
        public string image { get; set; }
    }

    public class Battle
    {
        //private BattleController _controller = new BattleController();

        public string OpponentName { get; set; }
        public string OpponentImage { get; set; }
        public string BattleID { get; set; }
        public Context ContextBattle { get; set; }
        public State StateBattle { get; set; }


        public BattleOpponent Opponent
        {
            get
            {
                return new BattleOpponent() { name = this.OpponentImage, image = this.OpponentImage };
            }
            set { }
        }

        public string combatStatus
        {
            get
            {
                switch (this.StateBattle)
                {
                    case (State.AVALAIBLE):
                        return "New Combat";
                    case (State.OPPTURN):
                        return "Opponenent Turn";
                    case (State.USERTURN):
                        return "Your turn";
                }
                return "";
            }
            set { }
        }

        //WILD
        public int monsterID { get; set; }
        public Monsters.Monster OpponentMonster
        {
            get { return App.core.monsterList[monsterID]; }
            set { }
        }

        public void Accept()
        {
            Utils.Log.Info("battleId", this.BattleID);
            Frame root = Window.Current.Content as Frame;
            if (this.StateBattle == State.AVALAIBLE)
                root.Navigate(typeof(Pages.PrepareBattlePage), this);
            else
                root.Navigate(typeof(Pages.BattlePage), this);

        }

        public void Decline()
        {

        }

//        public BattleController BattleController { get { return _controller; } set { _controller = value; } }

    }

}
