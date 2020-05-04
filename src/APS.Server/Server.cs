using System;
using System.Net;
using System.Net.Sockets;

namespace APS.Server
{
    class Server
    {
        static void Main(string[] args)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(localEndPoint);
            socket.Listen(10);
            Socket client = socket.Accept();
        }                
    }
}
