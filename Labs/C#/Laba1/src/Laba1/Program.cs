using System;
using System.Reflection;

namespace Laba1
{
    public class DebugPrintAttribute: Attribute
    {
        public string Format { get; set; }

        public DebugPrintAttribute(string Format="{0}")
        {
            this.Format = Format;
        }
    }
    
    [DebugPrint]
    public static class RefLab
    {
        public static void DebugPrint(object obj)
        {
            

        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Type t = typeof(RefLab);
            object[] attrs = t.
            foreach (DebugPrintAttribute a in attrs)
            {
                Console.WriteLine(a.Format);
            }

        }
    }
}
