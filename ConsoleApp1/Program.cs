using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Поиск минимума функции:");
            Console.WriteLine("y=x^2+2*x+4");
            Console.WriteLine("Левая граница:");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Правая граница:");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Точность:");
            double eps = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("----------------------");
            double x = 0;
            while (Math.Abs(b - a) / 2 > eps)
            {
                x = (a + b) / 2;
                double F1 = Y(x - eps);
                double F2 = Y(x + eps);
                if (F1 < F2)
                    b = x;
                else
                    a = x;
            }

            Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}", Y(x), x);
            Console.ReadLine();
        }
        static double Y(double x)
        {
            double y = Math.Pow(x, 2) + 2 * x + 4;
            return y;
        }
    }
}