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
            tcp = new ContestTcpConnection(7000, 2, 3);
            tcp.Connect();
            
            Thread.Sleep(100);

            string vals = tcp.Read();
            iniObj = parseInput(vals.Split('\n'));
            Console.WriteLine(iniObj.ToString());

            for (int n = 0; n < iniObj.dronesNum; n++) print(status(n));
          
            for (int i = 0; i <2; i++)
            {

                tstep8(13);
                tstep8(0);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        static void tstep(double val)
        {
            for (int i = 0; i < 20; i++)
            print(throttle(i, val));

            print(tick(1));
            //print(status(0));
        }
        static void tstep8(double val)
        {
            for (int i = 0; i < 20; i++)
                print(throttle(i, throttle8(val)));

            print(tick(1));
            //print(status(0));
        }

        static void wait()
        {
            Console.ReadLine();
        }

        static void print(object str)
        {
            Console.WriteLine(str.ToString());
        }

    }
}
