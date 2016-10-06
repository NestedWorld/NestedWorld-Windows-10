using System.Collections.Generic;
using System.Diagnostics;

namespace NestedWorld.Classes.ElementsGame.Battle.Paterns
{
    public class UseToolsPatern : Patern
    {
       
        public UseToolsPatern(BattleController core) : base(core, new List<int> { 3, 5, 2, 6 })
        { }


        public override void Execute()
        {
            Debug.WriteLine("tool");
        }
    }
}