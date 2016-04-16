using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCC_Linz16
{
    public class Vec<T>
    {
        public T x;
        public T y;
        public T z;

        public Vec()
        {
            
        }

        public Vec(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec<T> Clone()
        {
            return new Vec<T>(x, y, z);
        }
    }

    public class VecD : Vec<double>
    {
        public VecD(double x, double y, double z) : base(x, y, z)
        {

        }

        public void normalize()
        {
            double ab = betrag();
            this.x = x/ab;
            this.y = y / ab;
            this.z = z/ ab;
        }

        public void add(VecD vect)
        {
            this.x += vect.x;
            this.y += vect.y;
            this.z += vect.z;
        }

        public void sub(VecD vect)
        {
            this.x -= vect.x;
            this.y -= vect.y;
            this.z -= vect.z;
        }

        public double betrag()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }


        public double skalaresProdukt(VecD vect)
        {
            return x * vect.x + y * vect.y + z * vect.z;
        }

        public double winkel(VecD vect)
        {
            // cos alpha = (a . b) / (|a| * |b|)
            return Math.Acos(skalaresProdukt(vect) / (betrag() * vect.betrag()));
        }

        public new VecD Clone()
        {
            return new VecD(x, y, z);
        }
    }
}
