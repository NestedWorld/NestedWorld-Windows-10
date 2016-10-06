using System.Collections.Generic;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public abstract class Patern
    {
        protected List<int> patern { get; set; }

        protected BattleController controller { get; set; }

        public Patern(BattleController core, List<int> patern)
        {
            this.patern = patern;
            this.controller = core;
        }

        public bool isThisPatern(List<int> paternCompart)
        {
            if (patern.Count != paternCompart.Count)
                return false;
            for (int i = 0; i < patern.Count; i++)
            {

                if (paternCompart[i] != patern[i])
                    return false;
            }
            return true;
        }

        public abstract void Execute();
    }
}
