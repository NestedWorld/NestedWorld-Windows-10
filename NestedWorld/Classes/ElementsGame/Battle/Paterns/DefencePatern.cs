using MessagePack.Client.Combat;
using System.Collections.Generic;
using System.Diagnostics;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class DefencePatern : Patern
    {
      

        public DefencePatern(BattleController core) : base(core, new List<int> { 3, 5 })
        { }


        public override void Execute()
        {
            controller.annimationCanvas.DataContext = controller.UserMonster.attackList[Attack.AttackType.DEF];
            var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.DEF].Id, controller.start.OppomentMonster.Monster_Id);
            App.network.SendRequest(tmp);

        }
    }
}

