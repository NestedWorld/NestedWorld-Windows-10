using MessagePack.Client.Combat;
using System.Collections.Generic;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class AttackPatern : Patern
    {
        public AttackPatern(BattleController core)
            : base(core, new List<int> { 1, 4 })
        { }
        
        public override void Execute()
        {
            controller.annimationCanvas.DataContext = controller.UserMonster.attackList[Attack.AttackType.ATTACK];
            var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.ATTACK].Id, controller.start.OppomentMonster.Monster_Id);
            App.network.SendRequest(tmp);
        }
    }
}
