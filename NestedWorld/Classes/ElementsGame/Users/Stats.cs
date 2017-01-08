using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Users
{
    public class StatsMonster
    {
        public int electric;
        public int plant;
        public int water;
        public int earth;
        public int fire;

        public int total { get { return electric + plant + water + earth + fire; } set { } }
        public static StatsMonster LoadJson(JObject obj)
        {
            JObject type = obj["types"].ToObject<JObject>();



            return new StatsMonster()
            {
                electric = type["electric"].ToObject<int>(),
                plant = type["plant"].ToObject<int>(),
                water = type["water"].ToObject<int>(),
                earth = type["earth"].ToObject<int>(),
                fire = type["fire"].ToObject<int>(),
            };
        }
    }

    public class StatsCombat
    {
        public int pvp;
        public int portals;
        public int total;
        public int pve;

        public static StatsCombat LoadJson(JObject obj)
        {
            return new StatsCombat()
            {
                pvp = obj["pvp"].ToObject<int>(),
                portals = obj["portals"].ToObject<int>(),
                total = obj["total"].ToObject<int>(),
                pve = obj["pve"].ToObject<int>(),
            };
        }
    }

    public class Stats
    {
        public StatsCombat Victories { get; set; }
        public StatsCombat Defeats { get; set; }
        public StatsMonster Monsters { get; set; }

        public static Stats LoadJson(JObject obj)
        {
            JObject monsters = obj["monsters"].ToObject<JObject>();
            JObject combats = obj["combats"].ToObject<JObject>();
            JObject victories = combats["victories"].ToObject<JObject>();
            JObject defeats = combats["defeats"].ToObject<JObject>();



            return new Stats()
            {
                Victories = StatsCombat.LoadJson(victories),
                Defeats = StatsCombat.LoadJson(defeats),
                Monsters = StatsMonster.LoadJson(monsters)
            };
        }
    }
}
