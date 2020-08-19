using System;
using System.Collections.Generic;

namespace ConsoleRpg_2.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            for (int i = 0; i < str.Length; i += chunkSize)
            {
                yield return str.Substring(i, Math.Min(chunkSize, str.Length-i));
            }
        }
    }
}