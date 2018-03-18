using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Поиск минимума функции:");
            Console.WriteLine("y=x^2+2*x+4");
            double a = 0, b = 0;
            int n = 0;
            try
            {
                Console.WriteLine("Левая граница:");
                a = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Правая граница:");
                b = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Число итераций:");
                n = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Неверный формат входных данных");
            }
            Console.WriteLine("----------------------");

            double x = FibonacciSearch(a, b, n);

            Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}", Y(x), x);
            Console.WriteLine("Количество итераций: {0}", n);
            Console.ReadLine();
        }

        static double Y(double x)
        {
            double y = Math.Pow(x, 2) + 2 * x + 4;
            return y;
        }

        static double FibonacciSearch(double a, double b, int n)
        {
            double x1 = a + (b - a) * (fib(n - 2) / fib(n));
            double x2 = a + (b - a) * (fib(n - 1) / fib(n));
            double f1 = Y(x1);
            double f2 = Y(x2);
            while (n != 1) 
            {
                n -= 1;
                if (f1 > f2)
                {
                    a = x1;
                    x1 = x2;
                    x2 = b - (x1 - a);
                    f1 = f2;
                    f2 = Y(x2);
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + (b - x2);
                    f2 = f1;
                    f1 = Y(x1);
                }
            }
            return (x1 + x2) / 2;
        }

        static int fib(int n)
        {
            return n > 1 ? fib(n - 1) + fib(n - 2) : n;
        }
    }
}
