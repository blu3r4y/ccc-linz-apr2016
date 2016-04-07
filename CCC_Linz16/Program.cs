using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueLib.Network;

namespace CCC_Linz16
{
    class Program
    {
        static ContestTcpConnection _connection;

        static void Main(string[] args)
        {
            _connection = new ContestTcpConnection(7000); 
        }
    }
}
