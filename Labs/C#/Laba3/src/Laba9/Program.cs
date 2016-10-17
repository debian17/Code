using System;
using System.Linq;
using System.Threading;

namespace Laba9
{
    public class Program
    {
        static object locker = new object();

        public static class Arr
        {
            public static double[] arr;
            static Arr()
            {
                arr = new double[Environment.ProcessorCount];
            } 
        }

        public static void Monte_Carlo(object param)
        {
            double good = 0;
            double x = 0;
            double y = 0;
            double NumberOfPoint = (double)param;
            Random r;

            Monitor.Enter(locker);
            r = new Random();
            Thread.Sleep(30);
            Monitor.Exit(locker);
            
                for (int i = 0; i < NumberOfPoint; i++)
                {
                    x = r.NextDouble() * 2 - 1;
                    y = r.NextDouble() * 2 - 1;
                    if (x * x + y * y <= 1)
                    {
                        good++;
                    }
                }

            double res =(4 * good) / NumberOfPoint;
            Monitor.Enter(locker);
            try
            {
                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
                    if (Arr.arr[i] == 0)
                    {
                        Arr.arr[i] = res;
                    }
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Vvedite kolichestvo tochek:");
            double n = Convert.ToDouble(Console.ReadLine());
            int m = Environment.ProcessorCount;
            Thread[] myThread = new Thread[Environment.ProcessorCount];

            for (int i = 0; i < m; i++)
            {                
                myThread[i] = new Thread(new ParameterizedThreadStart(Monte_Carlo));
                myThread[i].Start(n);
            }

            for(int i = 0; i < myThread.Length; i++)
            {
                myThread[i].Join();
            }
            Console.WriteLine("Main Result=" + (Arr.arr.Sum() / Environment.ProcessorCount));
        }
    }
}
