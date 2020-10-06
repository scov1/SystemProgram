using System;

namespace Shape
{
    public static class MyClass
    {


        public static double AreaSquare(double side)
        {
            return side * side;
        }


        public static double AreaRectangle(double lenght,double width)
        {
            return lenght * width;
        }

        public static double AreaTreangle(double sideBase,double height)
        {
            return sideBase * height/2;
        }
    }
}
