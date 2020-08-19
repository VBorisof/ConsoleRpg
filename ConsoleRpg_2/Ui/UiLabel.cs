using System;
using ConsoleRpg_2.Extensions;

namespace ConsoleRpg_2.Ui
{
    public class UiLabel : UiElement
    {
        public override void Render()
        {
            if (IsFocused)
            {
                ConsoleEx.Write(Text, ConsoleColor.DarkGray, ConsoleColor.White);
            }
            else
            {
                ConsoleEx.Write(Text, ConsoleColor.Gray);
            }
        }
    }
}