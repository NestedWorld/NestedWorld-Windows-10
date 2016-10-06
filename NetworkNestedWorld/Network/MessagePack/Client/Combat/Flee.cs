using NestedWorld.Classes.MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NestedWorld.Classes.Network.MessagePack.Client.Combat
{
    public class Flee : RequestBase
    {
        public Flee()
            : base("combat:flee")
        {

        }

        public override MemoryStream GetStream()
        {
            throw new NotImplementedException();
        }
    }
}
