using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Stats
{
    public class Charac
    {
        private const int SIZEMAX = 300;
        public string Name { get; set; }
        public int Value { get; set; }
        public int ValueMax { get; set; }
        public double Size { get { return Value * SIZEMAX / ValueMax; } set { } }
    }
}
