using System;

namespace ConsoleRpg_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            
            new Engine.Engine().Run();
        }
    }
}