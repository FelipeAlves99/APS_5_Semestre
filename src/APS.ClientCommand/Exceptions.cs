using System;

namespace APS.ClientCommand
{
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException(string message): base(message) { }
    }
}
