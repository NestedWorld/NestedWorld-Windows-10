using MessagePack.Client.Combat;
using System.Collections.Generic;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class AttackSpePatern : Patern
    {
        
        public AttackSpePatern(BattleController core) : base(core, new List<int> { 1, 2, 3, 4, 5, 6 })
        { }


        public override void Execute()
        {
            if (controller.round)
            {
                controller.annimationCanvas.Sprite = App.core.Resources.AttackSprite[controller.UserMonster.attackList[Attack.AttackType.ATTACKSPE].AttackRessourcesName];
                var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.ATTACKSPE].Id, controller.start.OppomentMonster.Id);

                App.network.SendRequest(tmp);
            }
        }
    }
}
