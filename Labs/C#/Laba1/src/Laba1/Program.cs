using System;
using System.Collections.Generic;
using System.Linq;
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

            var props = t.GetProperties().Where(
                   prop => prop.IsDefined(typeof(DebugPrintAttribute)));

            var fields = t.GetFields().Where(
                   field => field.IsDefined(typeof(DebugPrintAttribute)));

            Console.WriteLine("Fields:");
            foreach (FieldInfo info in fields)
            {
                DebugPrintAttribute myAttr = (DebugPrintAttribute)info.GetCustomAttributes(typeof(DebugPrintAttribute)).ElementAt(0);
                Console.WriteLine(myAttr.Format, info.GetValue(obj), info.Name, t.Name);
            }

            Console.WriteLine("Propertys:");
            foreach (PropertyInfo info in props)
            {
                DebugPrintAttribute myAttr = (DebugPrintAttribute)info.GetCustomAttributes(typeof(DebugPrintAttribute)).ElementAt(0);
                Console.WriteLine(myAttr.Format, info.GetValue(obj), info.Name, t.Name);
            }
        }
    }
    
    
    public class TestClass
    {
        [DebugPrint("{0}---{1}---{2}")]
        public int a;
        [DebugPrint("{0}___{1}___{2}")]
        public int b;

        [DebugPrint("{0}+++{1}+++{2}")]
        public string c { get; set; }
        //[DebugPrint]
        public string d { get; set; }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            TestClass a = new TestClass();
            a.a = 123;
            a.b = 456;
            a.c = "some prorerty";

            RefLab.DebugPrint(a);
        }
    }
}
