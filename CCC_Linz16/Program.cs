using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BlueLib.Network;

namespace CCC_Linz16
{
    partial class Program
    {
        static ContestTcpConnection tcp;
        static InitVal iniObj;

        static void Main(string[] args)
        {
            try
            {
            tcp = new ContestTcpConnection(7000, 1, 3);
            tcp.Connect();
            
            Thread.Sleep(100);

            string vals = tcp.Read();
            iniObj = parseInput(vals.Split('\n'));
            Console.WriteLine(iniObj.ToString());
            
            for (int i = 0; i <10; i++)
            {
                    //Console.WriteLine(throttle(0, 0.7));
                    //if (i > 5) Console.WriteLine(throttle(0, 0.6));
                    //else Console.WriteLine(throttle(0, Math.Cos(i/20f)-0.4));
                    Console.WriteLine(throttle(0, 0.7 - (i / 30f)));
                    //Console.WriteLine(throttle(0, i % 2 == 0 ? 0.7 : 0.5-(i/30f)));
                Console.WriteLine(tick(1));
                Console.WriteLine(status(0));
                Thread.Sleep(500);
            }

            tcp.StartConsole();

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            Console.Read();
        }

    }
}
