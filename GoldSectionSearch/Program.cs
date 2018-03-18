using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSectionSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Поиск минимума функции:");
            Console.WriteLine("y=x^2+2*x+4");
            double a = 0, b = 0, eps = 0;
            try
            {
                Console.WriteLine("Левая граница:");
                a = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Правая граница:");
                b = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Точность:");
                eps = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Неверный формат входных данных");
            }
            Console.WriteLine("----------------------");

            int count = 0;

            double x = goldenSectionSearch(a, b, eps, ref count);

            Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}",Y(x),x);
            Console.WriteLine("Количество итераций: {0}", count);
            Console.ReadLine();

        }

        static double Y(double x)
        {
            double y = Math.Pow(x, 2) + 2 * x + 4;
            return y;
        }

        static double goldenSectionSearch(double l, double r, double eps, ref int count)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double resphi = 2 - phi;
            double x1 = l + resphi * (r - l);
            double x2 = r - resphi * (r - l);
            double f1 = Y(x1);
            double f2 = Y(x2);
            do {
                count++;
                if (f1 < f2) {
                    r = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = l + resphi * (r - l);
                    f1 = Y(x1);
                }
                else {
                    l = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = r - resphi * (r - l);
                    f2 = Y(x2);
                }
            }
            while (Math.Abs(r - l) > eps);
            return (x1 + x2) / 2;
        }
    }
}
