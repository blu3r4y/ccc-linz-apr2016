using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CCC_Linz16
{
    partial class Program
    {
        const double grav = 9.80665;

        static string status(int n)
        {
            return tcp.WriteRead("STATUS " + n);
        }

        static bool throttle(int n, double x)
        {
            if (x < 0 || x > 1) throw new ArgumentException("x needs to be 0..1");
            string send = "THROTTLE " + n + " " + toDbl(x);
            //Console.WriteLine(send);
            string str = tcp.WriteRead(send);

            return str == "OK\r";
        }

        static bool land(int n)
        {
            // assert dst to gnd < 0.3m
            // assert throttle 0
            // assert |vel| < 5m/s
            // assert vel absolute in z < 0.5 m/s

            string send = "LAND " + n;
            Console.WriteLine(send);
            string str = tcp.WriteRead(send);

            return str == "OK\r";
        }

        static string tick(int seconds)
        {
            if (seconds < 0) throw new ArgumentException("seconds negative!");

            string str = tcp.WriteRead("TICK " + seconds);

            return str;
        }
        
        // initial input
        static InitVal parseInput(string[] str)
        {
            var split = str[0].Split(' ');
            double xmin = double.Parse(split[0], CultureInfo.InvariantCulture);
            double xmax = double.Parse(split[1], CultureInfo.InvariantCulture);
            double ymin = double.Parse(split[2], CultureInfo.InvariantCulture);
            double ymax = double.Parse(split[3], CultureInfo.InvariantCulture);

            int n = int.Parse(str[1], CultureInfo.InvariantCulture);

            double height = 0;//double.Parse(str[2], CultureInfo.InvariantCulture);

            return new InitVal(height, n, new Point<double>(xmin, ymin),
                new Point<double>(xmax, ymax));
        }

        static string toDbl(double d)
        {
            return d.ToString("0.0000000000", CultureInfo.InvariantCulture);
        }

        static double throttle1(double thrust)
        {
            double th = Math.Pow(Math.Pow(thrust, 3), 0.15625);
            th *= 0.5172496842054825;

            return th;
        }

        static double throttle8(double acc)
        {
            double th = 0.027100266680146246 + 0.0082903743929312*acc;
            th += 0.0008453829180128993 * Math.Pow(acc, 2);
            th += 0.000028735022255744805 * Math.Pow(acc, 3);
            th = Math.Pow(th, 0.15625);

            return th;
        }

        static double thrust1(double throttle)
        {
            double th = Math.Pow(Math.Pow(throttle, 6.4), 1f/3f);
            th *= 4.081058626948094;

            return th;
        }
    }
}
