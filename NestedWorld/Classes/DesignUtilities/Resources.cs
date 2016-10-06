using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.DesignUtilities
{
    public class Resources
    {
        public SpriteDictionnary AttackSprite { get; set; }
        public SpriteDictionnary EffectSprite { get; set; }

        public Resources()
        {
            AttackSprite = SpriteDictionnary.GetSpriteDictionnary("AttackSourceFile.xml");
        }
    }
}
