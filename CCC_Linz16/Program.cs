using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BlueLib.Network;

namespace CCC_Linz16
{
    static partial class Program
    {
        public static ContestTcpConnection tcp;
        static Map map;
        

        public static void Main(string[] args)
        {
            try
            {
            tcp = new ContestTcpConnection(7000, 3, 1);
            tcp.Connect();
            
            Thread.Sleep(1000);


            string vals = tcp.Read();
            map = new Map(vals.Split('\n'));
            Console.WriteLine(map.ToString());
                Console.ReadLine();
                map.drones[0].moveToLinear(map.drones[0].target);
                wait();
            for (int i = 0; i <2; i++)
            {

                tstep8(0.1);
                tstep8(-0.1);
                    tstep8(0.1);
                    tstep8(-0.1);

                    tstep8(-5);
                tstep8(0);
                tstep(0);
                tstep(0);
                tstep8(-(13 - (2 * grav)));

                // hold
                for (int j = 0; j < 20; j++)
                {
                    tstep8(0);
                }
                double h = 35.58004;

                tstep8(-0.1);
                    // fall slow
                    for (int j = 0; j < h/0.1-2; j++)
                    {
                        tstep8(0);
                    }
                    tstep8(0.1);
                    tstep8(0);
                    tstep8(0);
                    tstep8(0);

                    for (int ii = 0; ii < 20; ii++)
                        print(throttle(ii, 0));

                    for (int ii = 0; ii < 20; ii++)
                        land(ii);


                    print(tick(1));
                    print(status(0));


                    Thread.Sleep(500);
            }

            tcp.StartConsole();

                

            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        public static void tstep(double val)
        {
            print(throttle(0, val));

            print(tick(1));
            print(status(0));
        }
        public static void tstep8(double val)
        {
                print(throttle(0, throttle8(val)));

            print(tick(1));
            print(status(0));
        }

        public static void wait()
        {
            Console.ReadLine();
        }

        public static void print(object str)
        {
            Console.WriteLine(str.ToString());
        }

    }
}
