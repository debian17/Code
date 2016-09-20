using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba1
{
    public class Program
    {
        public static int N = 100;
        public static double u = 0.5;
        public static double lt = 100;
        public static double ht = 0.1;
        public static double T = lt / ht;
        public static double hx = 1;
        public static double[] c = new double[N];
        public static double[] c1 = new double[N];
        public static double[] c2 = new double[N];

        public static void f()
        {
            for(int i =1; i < N-1; i++)
            {
                c1[i] = (c[i] - (u * ht * (c[i] - c[i - 1])) / hx) ;
            }
        }

        public static void f1()
        {
            for (int i = 1; i < N-1; i++)
            {
                c1[i] = (c[i] - (u * ht * (c[i+1] - c[i - 1])) / (2 * hx)) ;
            }
        }

        public static void f2()
        {
            for (int i = 1; i < N - 1; i++)
            {
                c2[i] = ((-1) * u) * (((c1[i] - c1[i - 1]) * 2 * ht) / hx) - (c1[i - 1] - c[i - 1]) + c1[i];
            }
        }


        public static void Main(string[] args)
        {
            
            for(int i = 0; i < N; i++)
            {
                if (i < 20)
                {
                    c[i] = 1;
                }
                else
                {
                    c[i] = 0;
                }
            }
            
            for(int i = 0; i < N; i++)
            {
                c1[i] = c[i];
            }


            for(int k = 1; k < T; k++)
            {
                f2();
                for (int i = 0; i < N; i++)
                {
                    c[i] = c1[i];
                    
                }

                for (int i = 0; i < N; i++)
                {
                    c1[i] = c2[i];
                }

            }

            for(int i = 0; i < N; i++)
            {
                Console.WriteLine(c[i]);
            }

        }
    }
}
