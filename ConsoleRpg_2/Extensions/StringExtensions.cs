using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleRpg_2.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            var result = new List<string>();

            var lines = str.Split("\n");

            foreach (var line in lines)
            {
                if (line.Length > chunkSize)
                {
                    var words = new Stack<string>(line.Split(" ").Reverse());

                    var resultLines = new List<string>();
                    var buffer = new StringBuilder();
                        
                    while (words.Any())
                    {
                        var word = words.Pop();

                        if (buffer.Length + word.Length + 1 < chunkSize)
                        {
                            buffer.Append(word + " ");
                        }
                        else
                        {
                            words.Push(word);
                            resultLines.Add(buffer.ToString());
                            buffer.Clear();
                        }
                    }
                    resultLines.Add(buffer.ToString());
                    result.AddRange(resultLines);
                }
                else
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}