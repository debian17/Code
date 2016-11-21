using System;
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

        static void DoSomeMagic(object fileName)
        {
            string[] s = fileName.ToString().Split(' ');
            for (int i = 0; i < s.Length - 1; i++)
            {
                StreamReader sr = new StreamReader()
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

        public static void Main(string[] args)
        {
            /*StreamWriter sw;
            FileInfo file;
            string[] Users = { "Fedora", "Debian", "Ubunty", "Arch", "OpenSuse", "Gentoo" };
            string[] Domens = { "google.com", "youtube.com", "wikipedia.com", "instagram.com", "vk.com", "rambler.ru" };
            string[] Dates = { "19.10.2016", "20.10.2016", "21.10.2016", "22.10.2016", "23.10.2016", "24.10.2016" };

            int l;
            Console.WriteLine("Vvedite kolichestvo failov:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Vvedite kolichestvo strok v failax:");
            l = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Random rand = new Random();
                file = new FileInfo("logfile" + i + ".txt");
                sw = file.AppendText();
                for (int j = 0; j < l; j++)
                {
                    sw.WriteLine(Users[rand.Next(0, 6)] + " " + Domens[rand.Next(0, 6)] + " " + Dates[rand.Next(0, 6)] + " " + rand.Next(1, 100000));
                }
                sw.Flush();
            }*/






        }
    }
}
