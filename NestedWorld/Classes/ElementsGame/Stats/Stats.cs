using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Stats
{
    public class Stats
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public int Netral { get; set; }
        public int Loose { get; set; }

        public int Total { get { return Win + Netral + Loose + 1; } set { int i = 0; i++; } }
        public double WinSize { get { return Win * 300 / Total; } set { int i = 0; i++; } }
        public double NetralSize { get { return Netral * 300 / Total; } set { int i = 0; i++; } }
        public double LooseSize { get { return Loose * 300 / Total; } set { int i = 0; i++; } }

        internal static Stats NewStat(string name, int win, int netral, int loose)
        {
            return new Stats { Name = name, Win = win, Loose = loose };
        }


    }
}
