using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static int x = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            Parallel.For(1, 10, Factorial);

            Console.ReadLine();
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine("Выполняется задача {0}", Task.CurrentId);
            Console.WriteLine("Факториал числа {0} равен {1}", x, result);
            Thread.Sleep(3000);
        }
    }
}
