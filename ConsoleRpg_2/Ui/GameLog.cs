using System;
using System.Linq;
using ConsoleRpg_2.Configurations;
using ConsoleRpg_2.Extensions;

namespace ConsoleRpg_2.Ui
{
    public class GameLog
    {   
        private string _gameLog;

        public void WriteLine(string line)
        {
            if (string.IsNullOrEmpty(_gameLog))
            {
                _gameLog += line;
            }
            else
            {
                _gameLog += $"\n{line}";
            }
        }

        public void Render()
        {
            ConsoleEx.WriteLine("".PadRight(Configuration.BufferLength, '_'), ConsoleColor.Green);

            var lines = _gameLog.Split("\n")
                .SelectMany(l => StringExtensions.Split(l, Configuration.BufferLength))
                .TakeLast(Configuration.BufferHeight)
                .ToList();
            
            
            var emptyBufferLines = Configuration.BufferHeight - lines.Count;
            if (emptyBufferLines > 0)
            {
                var emptyLine = "".PadLeft(Configuration.BufferLength, ' ');
                lines.AddRange(Enumerable.Repeat(emptyLine, emptyBufferLines));
            }
            
            ConsoleEx.WriteBlock(lines.Select(l => l.PadRight(Configuration.BufferLength, ' ')), ConsoleColor.White, ConsoleColor.DarkGray);
            
            ConsoleEx.WriteLine("".PadRight(Configuration.BufferLength, '_'), ConsoleColor.Green, ConsoleColor.DarkGray);
        }
    }
}