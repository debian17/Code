using System;
using System.Reflection;

namespace Laba1
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DebugPrintAttribute: Attribute
    {
        public string Format { get; set; }

        public DebugPrintAttribute(string Format="{0}")
        {
            this.Format = Format;
        }
    }
    
    
    public static class RefLab
    {
        public static void DebugPrint(object obj)
        {
            Type t = obj.GetType();
            object[] attrs = t.GetCustomAttributes(t.GetType());
            var f = t.GetFields();

            foreach( var i in t.GetFields())
            {
                Console.WriteLine(i.Name);
            }

            foreach (var i in t.GetProperties())
            {
                Console.WriteLine(i.Name);
            }

        }
    }
    
    
    public class TestClass
    {       

        [DebugPrint]
        public int a;
        [DebugPrint]
        public int b;

        [DebugPrint]
        public string c { get; set; }
        [DebugPrint]
        public string d { get; set; }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            TestClass a = new TestClass();

            RefLab.DebugPrint(a);

        }
    }
}
