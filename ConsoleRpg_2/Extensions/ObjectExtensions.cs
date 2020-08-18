using System;

namespace ConsoleRpg_2.Extensions
{
    public static class ObjectExtensions
    {
        public static void Print(this object o)
        {
            Console.WriteLine(o.ToString());
        }
        
        public static void Print(this object o, string name)
        {
            Console.WriteLine($"{name}: {o}");
        }
    }
}