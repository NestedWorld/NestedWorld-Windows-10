using MessagePack.Client.Combat;
using System.Collections.Generic;
using Windows.UI.Popups;
using System;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class AttackPatern : Patern
    {
        public AttackPatern(BattleController core)
            : base(core, new List<int> { 1, 4 })
        { }

        public override void Execute()
        {
            try
            {
                if (controller.round)
                {                    
                    var tmp = SendAttack.Attack(this.controller.combatID, controller.UserMonster.attackList[Attack.AttackType.ATTACK].Id, controller.start.OppomentMonster.Id);
                    App.network.SendRequest(tmp);
                }
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("AttackPatern::Execute", ex);
            }
        }
    }
}
