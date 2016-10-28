using MessagePack.Client.Combat;
using System.Collections.Generic;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class DefenceSpePatern : Patern
    {
      

        public DefenceSpePatern(BattleController core) : base(core, new List<int> { 1, 6, 5, 4, 3, 2 })
        { }


        public override void Execute()
        {
            if (controller.round)
            {
                controller.annimationCanvas.Sprite = App.core.Resources.AttackSprite[controller.UserMonster.attackList[Attack.AttackType.DEFSPE].AttackRessourcesName];
                var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.DEFSPE].Id, controller.start.OppomentMonster.Id);
                App.network.SendRequest(tmp);
            }
        }
    }
}
