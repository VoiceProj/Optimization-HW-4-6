using System;
using System.Globalization;

/// <summary>
/// Программа для поиска минимума функции методом деления пополам по двум точкам
/// </summary>

namespace ConsoleApplication1
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
            if (a != 0 && b != 0 && eps != 0)
            {
                double x = 0;
                int count = 0;
                while (Math.Abs(b - a) / 2 > eps)
                {
                    count++;
                    x = (a + b) / 2;
                    double F1 = Y(x - eps);
                    double F2 = Y(x + eps);
                    Console.WriteLine("Итерация #{0}:", count);
                    Console.WriteLine("a={0}, b={1}", a,b);
                    Console.WriteLine("F(x-eps)={0}, F(x+eps)={1}", F1, F2);
                    if (F1 < F2)
                    {
                        b = x;
                        Console.WriteLine("Берем левый отрезок");
                    }
                    else
                    {
                        a = x;
                        Console.WriteLine("Берем правый отрезок");
                    }
                    Console.WriteLine("----------------------");
                }

                Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}", Y(x), x);
                Console.WriteLine("Количество итераций: {0}", count);
            }
            Console.ReadLine();
        }
        static double Y(double x)
        {
            double y = Math.Pow(x, 2) + 2 * x + 4;
            return y;
        }
    }
}