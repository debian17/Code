using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] T = new double[100];
            double[] Tn = new double[100];
            int hx = 1;
            int lx = 100;
            double ht = 0.1;
            double Nx = lx / hx; 
            int k = 1;

            for(int i = 0; i <40; i++)
            {
                T[i] = 0;
            }
            for(int i = 40; i < 60; i++)
            {
                T[i] = 1;
            }
            for(int i = 60; i < 100; i++)
            {
                T[i] = 0;
            }

            for (int i = 1; i < 99; i++)
            {
                Tn[i] = ((ht * k * (T[i + 1] - 2 * T[i] + T[i - 1])) / (hx * hx)) + T[i];
            }
        }
    }
}
