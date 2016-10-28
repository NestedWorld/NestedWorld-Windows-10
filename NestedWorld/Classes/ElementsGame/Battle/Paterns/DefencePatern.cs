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
            if (controller.round)            
            {
                controller.annimationCanvas.Sprite = App.core.Resources.AttackSprite[controller.UserMonster.attackList[Attack.AttackType.DEF].AttackRessourcesName];
                var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.DEF].Id, controller.start.OppomentMonster.Id);
                App.network.SendRequest(tmp);
            }
        }
    }
}

