using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.DataBase
{
    public class MonsterDataBase : DataBase
    {
        public MonsterDataBase()
            : base("monster.sqlite")
        {

        }

        public void CreateDataBase()
        {
            this.connection.CreateTable<Classes.ElementsGame.Monsters.Monster>();
        }

        public void Query(string query, object[] param)
        {
            using (var con = this.connection)
            {
                //                con.Query().
                //con.Release();
            }
        }
    }
}
