using System.Net;

namespace FTP
{
    // This is a Simple datastructure that is used in packet queues
    public class NetworkMessage
    {
        public IPEndPoint Sender { get; set; }
        public Packet Packet { get; set; }
    }
}