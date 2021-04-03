using System.Runtime.InteropServices;

namespace Bot_Framework
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;

        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return $"{x}, {y}";
        }
    }
}
