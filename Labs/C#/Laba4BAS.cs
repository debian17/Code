using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_No3
{

    class Program
    {
        static Dictionary<string, int> SumUserDic = new Dictionary<string, int>();
        static Dictionary<string, int> SumDomenDic = new Dictionary<string, int>();
        static Dictionary<string, int> SumDateDic = new Dictionary<string, int>();
        static object locker = new object();

        static void DoSomeMagic(object fileName)
        {
            string[] s = fileName.ToString().Split(' ');
            for (int i = 0; i < s.Length - 1; i++)
            {
                StreamReader sr = new StreamReader("log"+i+".txt");
                Dictionary<string, int> User = new Dictionary<string, int>();
                Dictionary<string, int> Domen = new Dictionary<string, int>();
                Dictionary<string, int> Date = new Dictionary<string, int>();
                string str;
                string[] temp = new string[4];
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine();
                    temp = str.Split(' ');
                    if (UserDic.ContainsKey(temp[0]))
                    {
                        UserDic[temp[0]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        UserDic.Add(temp[0], Convert.ToInt32(temp[3]));
                    }
                    if (DomenDic.ContainsKey(temp[1]))
                    {
                        DomenDic[temp[1]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        DomenDic.Add(temp[1], Convert.ToInt32(temp[3]));
                    }
                    if (DateDic.ContainsKey(temp[2]))
                    {
                        DateDic[temp[2]] += Convert.ToInt32(temp[3]);
                    }
                    else
                    {
                        DateDic.Add(temp[2], Convert.ToInt32(temp[3]));
                    }
                }
                DropToDics(UserDic, DomenDic, DateDic);
            }
        }

        static void DropToDics(Dictionary<string, int> UserDic, Dictionary<string, int> DomenDic, Dictionary<string, int> DateDic)
        {
            lock (locker)
            {
                for (int i = 0; i < UserDic.Count; i++)
                {
                    if (SumUserDic.ContainsKey(UserDic.ElementAt(i).Key))
                    {
                        SumUserDic[UserDic.ElementAt(i).Key] += UserDic.ElementAt(i).Value;
                    }
                    else
                    {
                        SumUserDic.Add(UserDic.ElementAt(i).Key, UserDic.ElementAt(i).Value);
                    }
                }

                for (int i = 0; i < DomenDic.Count; i++)
                {
                    if (SumDomenDic.ContainsKey(DomenDic.ElementAt(i).Key))
                    {
                        SumDomenDic[DomenDic.ElementAt(i).Key] += DomenDic.ElementAt(i).Value;
                    }
                    else
                    {
                        SumDomenDic.Add(DomenDic.ElementAt(i).Key, DomenDic.ElementAt(i).Value);
                    }
                }

                for (int i = 0; i < DateDic.Count; i++)
                {
                    if (SumDateDic.ContainsKey(DateDic.ElementAt(i).Key))
                    {
                        SumDateDic[DateDic.ElementAt(i).Key] += DateDic.ElementAt(i).Value;
                    }
                    else
                    {
                        SumDateDic.Add(DateDic.ElementAt(i).Key, DateDic.ElementAt(i).Value);
                    }
                }
            }
        }

        static void SaveResult()
        {
            StreamWriter sw;
            FileInfo file;
            file = new FileInfo("UserLog.txt");
            sw = file.AppendText();
            for (int i = 0; i < SumUserDic.Count; i++)
            {
                sw.WriteLine(SumUserDic.ElementAt(i).Key + " " + SumUserDic.ElementAt(i).Value);
            }
            sw.Close();

            file = new FileInfo("DomenLog.txt");
            sw = file.AppendText();
            for (int i = 0; i < SumDomenDic.Count; i++)
            {
                sw.WriteLine(SumDomenDic.ElementAt(i).Key + " " + SumDomenDic.ElementAt(i).Value);
            }
            sw.Close();

            file = new FileInfo("DateLog.txt");
            sw = file.AppendText();
            for (int i = 0; i < SumDateDic.Count; i++)
            {
                sw.WriteLine(SumDateDic.ElementAt(i).Key + " " + SumDateDic.ElementAt(i).Value);
            }
            sw.Close();
        }

        static void Main(string[] args)
        {
            //StreamWriter sw;
            //FileInfo file;
            //string[] Users = { "Ваня", "Катя", "Петя", "Роман", "Иннокентий", "Тамара" };
            //string[] Domens = { "google.com", "youtube.com", "kp.ru", "er.ru", "donstu.ru", "rutracker.org" };
            //string[] Dates = { "19.10.2016", "08.08.2016", "19.04.2016", "31.12.2016", "29.02.2016", "01.06.2016" };

            //int l;
            //Console.WriteLine("Введите кол-во файлов:");
            //n = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Введите кол-во записей в файлах:");
            //l = Convert.ToInt32(Console.ReadLine());
            //for (int i = 0; i < n; i++)
            //{
            //    Random rand = new Random();
            //    file = new FileInfo("log" + i + ".txt");
            //    sw = file.AppendText();
            //    for (int j = 0; j < l; j++)
            //    {
            //        sw.WriteLine(Users[rand.Next(0, 6)] + " " + Domens[rand.Next(0, 6)] + " " + Dates[rand.Next(0, 6)] + " " + rand.Next(1, 100000));
            //    }
            //    sw.Close();
            //}

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Logs");
            int p = di.GetFiles().Length;
            int threadCount = Environment.ProcessorCount;
            Thread[] MyThreads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                MyThreads[i] = new Thread(new ParameterizedThreadStart(DoSomeMagic));
            }
            
            int m = p / threadCount;
            int a = 0;
            int t = 0;
            for (int i = 0; i < threadCount; i++)
            {
                string pars = "";
                if (i != threadCount-1)
                {
                    for (int j = a; j < m+t; j++)
                    {
                        pars += j + " ";
                    }
                }
                else
                {
                    for (int j = a; j < p; j++)
                    {
                        pars += j + " ";
                    }
                }
                MyThreads[i].Start(pars);
                a += m;
                t = m + t;
            }

            for (int i = 0; i < threadCount; i++)
            {
                MyThreads[i].Join();
            }
            SaveResult();
        }
    }
}
