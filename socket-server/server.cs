using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ports
{
    class server
    {
        public static int port = 6000;
        public static bool running = false;
        public static Socket ServerSocket;

        public static void InteruptHandler(object handler, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Received SIGINT, shutting down");

            running = false;
            ServerSocket.Shutdown(SocketShutdown.Both);
            ServerSocket.Close();
        }

        public static void Main(String[] args)
        {
            Socket clientSocket;
            byte[] msg = Encoding.ASCII.GetBytes("Hello Client");

            IPEndPoint serv = new IPEndPoint(IPAddress.Any, port);

            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(serv);
            ServerSocket.Listen(5);

            Console.CancelKeyPress += InteruptHandler;

            running = true;
            Console.WriteLine("Running Server");

            while (running)
            {
                clientSocket = ServerSocket.Accept();
                Console.WriteLine("Incoming connection from {0}, replying.", clientSocket.RemoteEndPoint);

                // Send a reply (blocks)
                clientSocket.Send(msg, SocketFlags.None);

                // Close the connection
                clientSocket.Close();
            }
        }
    }
}
