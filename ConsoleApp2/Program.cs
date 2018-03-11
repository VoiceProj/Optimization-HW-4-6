using System;
using System.Globalization;


/// <summary>
/// Программа для поиска минимума функции методом деления пополам по двум точкам
/// </summary>
/// 
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Поиск минимума функции:");
            Console.WriteLine("y=x^2+2*x+4");            
            double a=0, b=0, eps=0;
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
                int count = 0;
                double L = b - a;
                double x2 = (b + a) / 2;
                while (L > eps)
                {
                    count++;
                    double F2 = Y(x2);
                    double x1 = a + L / 4;
                    double x3 = b - L / 4;
                    double F1 = Y(x1);
                    double F3 = Y(x3);
                    Console.WriteLine("Итерация #{0}:", count);
                    Console.WriteLine("x1={0}, x2={1}, x3={2}", x1, x2, x3);
                    Console.WriteLine("F(x1)={0}, F(x2)={1}, F(x3)={2}:", F1, F2, F3);
                    if (F1 < F2)
                    {
                        b = x2;
                        x2 = x1;
                        Console.WriteLine("Берем левый отрезок");
                    }
                    else if (F2 > F3)
                    {
                        a = x2;
                        x2 = x3;
                        Console.WriteLine("Берем правый отрезок");
                    }
                    else
                    {
                        a = x1;
                        b = x3;
                        Console.WriteLine("Берем центральный отрезок");
                    }
                    L = b - a;
                    Console.WriteLine("----------------------");
                }

                Console.WriteLine("Минимум функции F(x)={0} и находится в точке {1}", Y(x2), x2);
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
