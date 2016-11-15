using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba2
{
    class Place
    {
        bool[] car;
        public int n_N { get; }
        public int k_K { get; }

        public Place()
        {
            car = new bool[3];
        }
    }

    public class Station
    {
      
        private int lost;
        private Place[] m;

        public Station(int m)
        {
            this.m = new Place[m];
            Random r = new Random();
        }

        public void add_car()
        {

        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Vvedite kolichestvo mest: ");
            int m = Convert.ToInt32(Console.ReadLine());

            Station S = new Station(m);
            int time = 0;
            
            for(int i = 0; i < 200; i++)
            {

            }


        }
    }
}
