using System.Text;

namespace CCC_Linz16
{
    public class InitVal
    {
        public Point<double> min;
        public Point<double> max;

        int dronesNum;

        double height;

        public InitVal()
        {
        }

        public InitVal(double height, int dronesNum,
            Point<double> min, Point<double> max)
        {
            this.height = height;
            this.dronesNum = dronesNum;
            this.min = min;
            this.max = max;
        }

        public new string ToString()
        {
            var sb  = new StringBuilder();
            sb.AppendLine($"area: ({min.x} / {min.y}) .. ({max.x} / {max.y})");
            sb.AppendLine($"drones: {dronesNum}");
            sb.AppendLine($"height: {height}");

            return sb.ToString();
        }
    }
}