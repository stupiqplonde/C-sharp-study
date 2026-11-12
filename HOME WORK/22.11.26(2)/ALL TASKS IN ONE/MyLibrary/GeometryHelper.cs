using System;

namespace MyLibrary
{
    public static class GeometryHelper
    {
        public static double SquareArea(double side) { return side * side; }
        public static double RectangleArea(double w, double h) { return w * h; }
        public static double TriangleArea(double baseSide, double height) { return 0.5 * baseSide * height; }
    }
}