using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CCC_Linz16
{
    static partial class Program
    {
        public const double grav = 9.80665;
        public const double maxSpeed = 13;

        public static string status(int n)
        {
            return tcp.WriteRead("STATUS " + n);
        }

        public static bool throttle(int n, double x)
        {
            if (x < 0 || x > 1) throw new ArgumentException("x needs to be 0..1");
            string send = "THROTTLE " + n + " " + toDbl(x);
            Console.WriteLine(send);
            string str = tcp.WriteRead(send);

            return str == "OK\r";
        }

        public static bool land(int n)
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

        public static bool turn(int n, double dx, double dy, double dz)
        {
            string send = $"TURN {n} {toDbl(dx)} {toDbl(dy)} {toDbl(dz)}";
            Console.WriteLine(send);
            string str = tcp.WriteRead(send);

            return str == "OK\r";
        }

        public static string tick(int seconds)
        {
            if (seconds < 0) throw new ArgumentException("seconds negative!");

            string str = tcp.WriteRead("TICK " + seconds);

            return str;
        }

        public static string toDbl(double d)
        {
            return d.ToString("0.0000000000", CultureInfo.InvariantCulture);
        }

        // 1 rotor throttle do wanted thrust
        public static double throttle1(double thrust)
        {
            double th = Math.Pow(Math.Pow(thrust, 3), 0.15625);
            th *= 0.5172496842054825;

            return th;
        }

        // 1 rotor throttle to wanted acceleration
        public static double throttle8(double acc)
        {
            double th = 0.027100266680146246 + 0.0082903743929312*acc;
            th += 0.0008453829180128993 * Math.Pow(acc, 2);
            th += 0.000028735022255744805 * Math.Pow(acc, 3);
            th = Math.Pow(th, 0.15625);

            return th;
        }

        // 1 rotor throttle to wanted acceleration
        public static double throttle8(double acc, VecD orientation)
        {
            double th = 0.027100266680146246 + 0.0082903743929312 * acc;
            th += 0.0008453829180128993 * Math.Pow(acc, 2);
            th += 0.000028735022255744805 * Math.Pow(acc, 3);
            th = Math.Pow(th, 0.15625);

            return th;
        }

        public static double thrust1(double throttle)
        {
            double th = Math.Pow(Math.Pow(throttle, 6.4), 1f/3f);
            th *= 4.081058626948094;

            return th;
        }
    }
}
