using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CCC_Linz16
{
    public class Map
    {
        public Point<double> min;
        public Point<double> max;

        public List<Drone> drones = new List<Drone>(); 

        public int dronesNum;

        public double height;

        public Map()
        {
        }

        public Map(double height, int dronesNum,
            Point<double> min, Point<double> max)
        {
            this.height = height;
            this.dronesNum = dronesNum;
            this.min = min;
            this.max = max;

            Drone.theMap = this;
        }

        public Map(string[] str)
        {
            var split = str[0].Split(' ');
            double xmin = double.Parse(split[0], CultureInfo.InvariantCulture);
            double xmax = double.Parse(split[1], CultureInfo.InvariantCulture);
            double ymin = double.Parse(split[2], CultureInfo.InvariantCulture);
            double ymax = double.Parse(split[3], CultureInfo.InvariantCulture);

            int n = int.Parse(str[1], CultureInfo.InvariantCulture);

            for (int i = 0; i < n; i++)
            {
                var xyz = str[2+i].Split(' ');
                double x = double.Parse(xyz[0], CultureInfo.InvariantCulture);
                double y = double.Parse(xyz[1], CultureInfo.InvariantCulture);
                double z = double.Parse(xyz[2], CultureInfo.InvariantCulture);

                drones.Add(new Drone(i, new VecD(x, y, z)));
            }


            // not used!
            double height = 0;//double.Parse(str[2], CultureInfo.InvariantCulture);


            this.height = height;
            this.dronesNum = n;
            this.min = new Point<double>(xmin, ymin);
            this.max = new Point<double>(xmax, ymax);
        }

        public new string ToString()
        {
            var sb  = new StringBuilder();
            sb.AppendLine($"#area: ({min.x} / {min.y}) .. ({max.x} / {max.y})");
            sb.AppendLine($"#drones: {dronesNum}");
            sb.AppendLine($"#height: {height}");

            foreach (var d in drones)
            {
                sb.Append(d.ToString());
            }

            return sb.ToString();
        }


    }
}