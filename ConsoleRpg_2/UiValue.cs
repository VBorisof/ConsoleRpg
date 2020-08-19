using System;
using ConsoleRpg_2.Extensions;

namespace ConsoleRpg_2
{
    public class UiValue : UiElement
    {
        public override void Render()
        {
            if (IsFocused)
            {
                ConsoleEx.Write(Text, ConsoleColor.Black, ConsoleColor.White);
            }
            else
            {
                ConsoleEx.Write(Text, ConsoleColor.White);
            }
        }
    }
}