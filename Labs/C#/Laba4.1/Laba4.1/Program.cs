using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Laba4
{
    public class Program
    {
        static Dictionary<string, int> UserSum = new Dictionary<string, int>();
        static Dictionary<string, int> DomenSum = new Dictionary<string, int>();
        static Dictionary<string, int> DateSum = new Dictionary<string, int>();
        static object locker = new object();

        static void WriteToGlobalDictionary(Dictionary<string, int> User, Dictionary<string, int> Domen, Dictionary<string, int> Date)
        {
            lock (locker)
            {
                for (int i = 0; i < User.Count; i++)
                {
                    if (UserSum.ContainsKey(User.ElementAt(i).Key))
                    {
                        UserSum[User.ElementAt(i).Key] += User.ElementAt(i).Value;
                    }
                    else
                    {
                        UserSum.Add(User.ElementAt(i).Key, User.ElementAt(i).Value);
                    }
                }

                for (int i = 0; i < Domen.Count; i++)
                {
                    if (DomenSum.ContainsKey(Domen.ElementAt(i).Key))
                    {
                        DomenSum[Domen.ElementAt(i).Key] += Domen.ElementAt(i).Value;
                    }
                    else
                    {
                        DomenSum.Add(Domen.ElementAt(i).Key, Domen.ElementAt(i).Value);
                    }
                }

                for (int i = 0; i < Date.Count; i++)
                {
                    if (DateSum.ContainsKey(Date.ElementAt(i).Key))
                    {
                        DateSum[Date.ElementAt(i).Key] += Date.ElementAt(i).Value;
                    }
                    else
                    {
                        DateSum.Add(Date.ElementAt(i).Key, Date.ElementAt(i).Value);
                    }
                }
            }       
        }

        static void DoTaskWork(string fileName)
        {
                StreamReader sr = new StreamReader(fileName);
                //ConcurrentDictionary 
                Dictionary<string, int> User = new Dictionary<string, int>();
                Dictionary<string, int> Domen = new Dictionary<string, int>();
                Dictionary<string, int> Date = new Dictionary<string, int>();
                string str;
                string[] temp = new string[4];
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();
                    temp = str.Split(' ');       
                    if (User.ContainsKey(temp[0]))
                    {
                        User[temp[0]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        User.Add(temp[0], Convert.ToInt32(temp[3]));
                    }
                    if (Domen.ContainsKey(temp[1]))
                    {
                        Domen[temp[1]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        Domen.Add(temp[1], Convert.ToInt32(temp[3]));
                    }
                    if (Date.ContainsKey(temp[2]))
                    {
                        Date[temp[2]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        Date.Add(temp[2], Convert.ToInt32(temp[3]));
                    }
                }
                sr.Close();
                WriteToGlobalDictionary(User, Domen, Date);
        }

        static void GetResult()
        {
            StreamWriter sw;
            FileInfo file;
            file = new FileInfo("User.txt");
            sw = file.AppendText();
            for (int i = 0; i < UserSum.Count; i++)
            {
                sw.WriteLine(UserSum.ElementAt(i).Key + " " + UserSum.ElementAt(i).Value);
            }
            sw.Flush();
            sw.Close();

            file = new FileInfo("Domen.txt");
            sw = file.AppendText();
            for (int i = 0; i < DomenSum.Count; i++)
            {
                sw.WriteLine(DomenSum.ElementAt(i).Key + " " + DomenSum.ElementAt(i).Value);
            }
            sw.Flush();
            sw.Close();

            file = new FileInfo("Date.txt");
            sw = file.AppendText();
            for (int i = 0; i < DateSum.Count; i++)
            {
                sw.WriteLine(DateSum.ElementAt(i).Key + " " + DateSum.ElementAt(i).Value);
            }
            sw.Flush();
            sw.Close();
        }

        public static void Main(string[] args)
        {
            //StreamWriter sw;
            //FileInfo file;
            //string[] Users = { "Fedora", "Debian", "Ubunty", "Arch", "OpenSuse", "Gentoo" };
            //string[] Domens = { "google.com", "youtube.com", "wikipedia.com", "instagram.com", "vk.com", "rambler.ru" };
            //string[] Dates = { "19.10.2016", "20.10.2016", "21.10.2016", "22.10.2016", "23.10.2016", "24.10.2016" };

            //int l;
            //Console.WriteLine("Vvedite kolichestvo failov:");
            //int n = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Vvedite kolichestvo strok v failax:");
            //l = Convert.ToInt32(Console.ReadLine());
            //for (int i = 0; i < n; i++)
            //{
            //    Random rand = new Random();
            //    file = new FileInfo("logfile" + i + ".txt");
            //    sw = file.AppendText();
            //    for (int j = 0; j < l; j++)
            //    {
            //        sw.WriteLine(Users[rand.Next(0, 6)] + " " + Domens[rand.Next(0, 6)] + " " + Dates[rand.Next(0, 6)] + " " + rand.Next(1, 100000));
            //    }
            //    sw.Flush();
            //    sw.Close();
            //}



            Console.WriteLine("Vvedite kolichestvo failov:");
            int n = Convert.ToInt32(Console.ReadLine());

            Task One = Task.Run(() =>
            {
                Task[] t = new Task[n];
                for (int i = 0; i < n; i++)
                {
                    int temp = i;
                    t[temp] = new Task(() => DoTaskWork("logfile" + temp + ".txt"));
                    t[temp].Start();
                    Console.WriteLine("запустилась задача "+temp);
                }
                Task.WaitAll(t);
            }).ContinueWith((t)=> {
                GetResult();
            });

            Task.WaitAll(One); 
        }
    }
}
