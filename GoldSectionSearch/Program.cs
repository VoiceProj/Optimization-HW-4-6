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
            double phi = (1 + Math.Sqrt(5)) / 2;
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
            double x;
            while (Math.Abs(b - a) > eps)
            {
                Console.WriteLine("Итерация #{0}:", ++count);
                Console.WriteLine("a={0}, b={1}", a, b);
                double x1 = b - (b - a) / phi;
                double x2 = a + (b - 1) / phi;
                double y1 = Y(x1), y2 = Y(x2);
                if (y1 > y2)
                    a = x1;
                else
                    b = x2;
            }

            x = a + b / 2;
            Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}", Y(x), x);
            Console.WriteLine("Количество итераций: {0}", count);
            Console.ReadLine();

        }

        static double Y(double x)
        {
            double y = Math.Pow(x, 2) + 2 * x + 4;
            return y;
        }
    }
}
