using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba2
{
    public class Place
    {
        public bool[] cars;
        public int[] income;
        public int[] outcome;

        public Place()
        {
            cars = new bool[3];
            income = new int[3];
            outcome = new int[3];
        }
    }

    public class Station
    {    
        private int m;
        private Place[] M;

        public Station(int m)
        {
            this.m = m;
            M = new Place[m];
        }

        public void AddCar(int n, int N, int k, int K)
        {
            Random r = new Random();
            for(int )

        }
    }

    public class Program
    {
        const int n = 0;
        const int N = 10;
        const int k = 0;
        const int K = 20;

        public static void Main(string[] args)
        {
            Console.Write("Vvedite kolichestvo mest: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Station S = new Station(m);

            

        }
    }
}
