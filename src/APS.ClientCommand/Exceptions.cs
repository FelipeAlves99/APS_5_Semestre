using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.ClientCommand
{
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException(string message): base(message) { }
    }
}
