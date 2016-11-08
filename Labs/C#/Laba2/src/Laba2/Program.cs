using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba2
{
    class Place
    {
        bool[] car;
        int[] InTime;
        int[] OutTime;

        public Place()
        {
            car = new bool[3];
            InTime = new int[3];
            OutTime = new int[3];
        }
    }

    public class Station
    {
        const int n = 0;
        const int N = 10;
        const int k = 0;
        const int K = 20;
        public int WorkTime{ get; }

        private Place[] m;

        public Station(int mCount, int WorkTime)
        {
            m = new Place[mCount];
            this.WorkTime = WorkTime;
        }

        public void Check()
        {

        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Vvedite kolichestvo mest: ");
            int mCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Vvedite WorkTime:");
            int wt = Convert.ToInt32(Console.ReadLine());

            Station S = new Station(mCount,wt);
            Random r = new Random();

            for(int i = 0; i <S.WorkTime; i++)
            {
                

            }

            

        }
    }
}
