using System.Net;
using System.Net.Sockets;

namespace SocketLib
{
    public class SocketTemplate
    {
        public int port { get; set; }
        public bool offline { get; set; } = false;
        public string hostname { get; set; }
      
        public Windows.Networking.HostName HostName { get { return new Windows.Networking.HostName(hostname); } set { } }
        
        public DnsEndPoint NewEndPoint()
        {
            return new DnsEndPoint(hostname, port);
        }
    }
}
