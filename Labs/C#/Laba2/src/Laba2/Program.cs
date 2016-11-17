using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Laba2
{
    [Serializable]
    public class Car
    {
        public int k_K;
        public Car(int k_K)
        {
            this.k_K = k_K;
        }
    }

    [Serializable]
    public class Place
    {
        public Queue<Car> queue;
        public Place()
        {
            queue = new Queue<Car>();
        }            
    }

    [Serializable]
    public class Station
    {
        public Place[] m;
        public int lost;
        public int n_N;
        public Station(int count)
        {
            m = new Place[count];
            for(int a = 0; a < count; a++)
            {
                Place p = new Place();
                m[a] = p;
            }
            lost = 0;
        }

        public void generate_n_N()
        {
            Random r = new Random();
            n_N = r.Next(2, 3);
        }
    }

    public class Program
    {
        public static void SerializeCarTo(string fileName, Station s)
        {
            Stream st = new FileStream(fileName, FileMode.Create);
            IFormatter f = new BinaryFormatter();
            f.Serialize(st, care);
            st.Close();
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Vvedite kolichestvo mest: ");
            int c = Convert.ToInt32(Console.ReadLine());

            Station S = new Station(c);
            int CountPlace = S.m.Length;
            int[] timeK = new int[CountPlace];
            S.generate_n_N();
            int timeN = 0;

            for (int i = 0; i < 400; i++)
            {
                Console.WriteLine(i);
                if (S.n_N == timeN)
                {
                    timeN = 0;
                    int min = 4;
                    int minIndex = 0;
                    for(int j = 0; j<CountPlace; j++)
                    {
                        if (S.m[j].queue.Count < min)
                        {
                            min = S.m[j].queue.Count;
                            minIndex = j;
                        }
                    }
                    if (min == 3)
                    {
                        S.lost++;  
                    }
                    else
                    {
                        Random r = new Random();
                        Car temp = new Car(r.Next(10, 25));
                        S.m[minIndex].queue.Enqueue(temp);
                        Console.WriteLine("Tachka pod'exala na kolonky #{0}", minIndex);
                        S.generate_n_N();
                    }                  
                }

                for(int m = 0; m < CountPlace; m++)
                {
                    if (S.m[m].queue.Count!=0)
                    {
                        if (S.m[m].queue.ElementAt(0).k_K == timeK[m])
                        {
                            S.m[m].queue.Dequeue();
                            timeK[m] = 0;
                            Console.WriteLine("Tachka ot'exala ot kolonki #{0}", m);
                        }
                    }                   
                }
                timeN++;
                for(int l = 0; l < timeK.Length; l++)
                {
                    timeK[l]++;
                }
            }
            Console.WriteLine(S.lost);
        }
    }
}
