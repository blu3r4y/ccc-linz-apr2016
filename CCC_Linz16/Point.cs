namespace CCC_Linz16
{
    public class Point<T>
    {
        public T x;
        public T y;

        public Point()
        {
        }

        public Point(T x, T y)
        {
            this.x = x;
            this.y = y;
        }
    }
}