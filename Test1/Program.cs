using System;
using System.Reflection;

namespace Test1
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Assembly shape = Assembly.LoadFrom("Shape.dll");
            Console.WriteLine(shape.FullName);

            try
            { 
                Type type = shape.GetType("Shape.MyClass");
      
                MethodInfo mi = type.GetMethod("AreaRectangle");
             //   MethodInfo mi = type.GetMethod("AreaSquare");
             //   MethodInfo mi = type.GetMethod("AreaTreangle");

                double a = 5, b = 4;
                double area;
                object[] parameters = new object[] { a, b };

                area = (double)mi.Invoke(null,parameters); 
                Console.WriteLine("Area = {0}", area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
