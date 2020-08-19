using System;

namespace ConsoleRpg_2.Extensions
{
    public static class ConsoleEx
    {
        public static void WriteLine(object o, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
        {
            var oldBackgroundColor = Console.BackgroundColor;            
            if (backgroundColor != null)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }
            
            var oldForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            
            Console.WriteLine(o);
            
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }
        
        public static void Write(object o, ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
        {
            var oldBackgroundColor = Console.BackgroundColor;            
            if (backgroundColor != null)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }
            
            var oldForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            
            Console.Write(o);
            
            Console.ForegroundColor = oldForegroundColor;
            Console.BackgroundColor = oldBackgroundColor;
        }
    }
}