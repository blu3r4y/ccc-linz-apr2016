using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace CCC_Linz16
{
    public class Drone
    {
        public static Map theMap;

        public int id;
        public VecD pos;
        public VecD vel;
        public VecD rot;

        public VecD target;

        public bool onGround = true;

        public Drone()
        {

        }

        public Drone(int id, VecD pos, VecD vel, VecD rot, VecD target)
        {
            this.id = id;
            this.pos = pos;
            this.vel = vel;
            this.rot = rot;
        }

        public Drone(int id, VecD target)
        {
            this.id = id;
            this.target = target;
            renew();
        }

        public void renew()
        {
            string str = Program.status(this.id);

            var split = str.Split(' ');
            double x = double.Parse(split[0], CultureInfo.InvariantCulture);
            double y = double.Parse(split[1], CultureInfo.InvariantCulture);
            double z = double.Parse(split[2], CultureInfo.InvariantCulture);

            double vx = double.Parse(split[3], CultureInfo.InvariantCulture);
            double vy = double.Parse(split[4], CultureInfo.InvariantCulture);
            double vz = double.Parse(split[5], CultureInfo.InvariantCulture);

            double rx = double.Parse(split[6], CultureInfo.InvariantCulture);
            double ry = double.Parse(split[7], CultureInfo.InvariantCulture);
            double rz = double.Parse(split[8], CultureInfo.InvariantCulture);
            
            this.pos = new VecD(x, y, z);
            this.vel = new VecD(vx, vy, vz);
            this.rot = new VecD(rx, ry, rz);
        }

        public new string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"drone id: {id}");
            sb.AppendLine($"    pos: ({pos.x} / {pos.y} / {pos.z})");
            sb.AppendLine($"    vel: ({vel.x} / {vel.y} / {vel.z})");
            sb.AppendLine($"    rot: ({rot.x} / {rot.y} / {rot.z})");
            sb.AppendLine($"    target: ({target.x} / {target.y} / {target.z})");

            return sb.ToString();
        }

        public void targetFix()
        {
            if (Math.Abs(target.z) < 0.1) target.z = 1;
            //target.z = 10;
        }

        public void moveToLinear(VecD tar)
        {
            targetFix();

            startToHeight(1);

            double stepSpeed = 0.1;//should be <10!!!
            
            VecD ori;
            double distance = 9;
            while (distance > 1)
            {
                // orientation
                ori = tar.Clone();
                ori.sub(pos);
                distance = ori.betrag();
                VecD up = new VecD(0, 0, Program.grav);
                ori.add(up);
                ori.normalize();

                // turn
                Program.turn(id, ori.x, ori.y, ori.z);

                // move
                tstep8(stepSpeed);
                tick();
                tstep8(-stepSpeed);
                tick();
            }

            tstep8(0);
            tick();
        }

        public void startToHeight(double height)
        {
            double stepSpeed = 0.1;//should be <10!!!

            for (double i = 0; i < height; i = i + stepSpeed)
            {
                tstep8(stepSpeed);
                tick();
                tstep8(-stepSpeed);
                tick();
            }

            tstep8(0);
            tick();
        }

        private double dist(double acc)
        {
            return acc/2;
        }

        private void tstep(double val)
        {
            Program.print(Program.throttle(id, val));
            //tick();
        }
        private  void tstep8(double val)
        {
            Program.print(Program.throttle(id, Program.throttle8(val)));
            //tick();
        }

        private void tick()
        {
            Program.print(Program.tick(1));
            renew();
            Program.print(Program.status(this.id));
        }


    }
}
