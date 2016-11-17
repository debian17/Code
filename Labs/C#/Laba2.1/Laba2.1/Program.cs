using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml.Serialization;

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
        public Car()
        {

        }
    }

    [Serializable]
    public class Place
    {
        [XmlArray("ListCars"), XmlArrayItem("Car")]
        public List<Car> list;
        public Place()
        {
            list = new List<Car>();
        }
    }

    [Serializable]
    public class Station
    {
        [XmlArray("ListPlace"), XmlArrayItem("Place")]
        public List<Place> m;
        public int lost;
        public int n_N;
        public Station(int count)
        {
            m = new List<Place>();
            for (int a = 0; a < count; a++)
            {
                Place p = new Place();
                m.Add(p);
            }
            lost = 0;
        }

        public Station()
        {

        }

        public void generate_n_N()
        {
            Random r = new Random();
            n_N = r.Next(2, 5);
        }
    }

    public class Program
    {
        public static void SerializeStationTo(string fileName, Station s)
        {
            Stream st = new FileStream(fileName, FileMode.Create);
            IFormatter f = new BinaryFormatter();
            f.Serialize(st, s);
            st.Close();
        }

        public static Station DeserializeStationFrom(string fileName)
        {
            Stream st = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            IFormatter f = new BinaryFormatter();
            Station s = (Station)f.Deserialize(st);
            st.Close();
            return s;
        }

        public static void XMLSerializeStationTo(string filename, Station s)
        {
            XmlSerializer formatter = new XmlSerializer(s.GetType());
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            formatter.Serialize(fs, s);
            fs.Close();
        }

        public static Station XMLDeserializeStationFrom(string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Station));
            FileStream fs = new FileStream("1.xml", FileMode.OpenOrCreate);
            Station s = (Station)formatter.Deserialize(fs);
            return s;
        }

        public static void StartWork(Station S, int E)
        {
            int CountPlace = S.m.Count;
            int[] timeK = new int[CountPlace];
            S.generate_n_N();
            int timeN = 0;

            for (int i = 0; i < E; i++)
            {
                //Console.WriteLine(i);
                if (S.n_N == timeN)
                {
                    timeN = 0;
                    int min = 4;
                    int minIndex = 0;
                    for (int j = 0; j < CountPlace; j++)
                    {
                        if (S.m[j].list.Count < min)
                        {
                            min = S.m[j].list.Count;
                            minIndex = j;
                        }
                    }
                    if (min == 3)
                    {
                        S.lost++;
                    }
                    else
                    {
                        Thread.Sleep(20);
                        Random r = new Random();
                        Car temp = new Car(r.Next(10, 25));
                        S.m[minIndex].list.Add(temp);
                        //Console.WriteLine("Tachka pod'exala na kolonky #{0}", minIndex);
                        S.generate_n_N();
                    }
                }

                for (int m = 0; m < CountPlace; m++)
                {
                    if (S.m[m].list.Count != 0)
                    {
                        if (S.m[m].list.ElementAt(0).k_K == timeK[m])
                        {
                            S.m[m].list.RemoveAt(0);
                            timeK[m] = 0;
                            //Console.WriteLine("Tachka ot'exala ot kolonki #{0}", m);
                        }
                    }
                }
                timeN++;
                for (int l = 0; l < timeK.Length; l++)
                {
                    timeK[l]++;
                }
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Vvedite kolichestvo mest: ");
            int c = Convert.ToInt32(Console.ReadLine());
            Station S = new Station(c);
            StartWork(S, 400);
            Console.WriteLine("After step#1 lost={0}", S.lost);
            Console.WriteLine("BinarySerialize...");
            SerializeStationTo("1.bin", S);
            Console.WriteLine("BinaryDeserialize...");
            Station S1 = DeserializeStationFrom("1.bin");
            StartWork(S1, 100);
            Console.WriteLine("After step#2 lost={0}", S1.lost);
            Console.WriteLine("XMLSerialize...");
            XMLSerializeStationTo("1.xml", S1);
            Console.WriteLine("XMLDeserialize...");
            Station S2 = XMLDeserializeStationFrom("1.xml");
            StartWork(S2, 200);
            Console.WriteLine("After step#3 lost={0}", S2.lost);

        }
    }
}
