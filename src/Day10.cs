using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day10
    {
        char[] possible = { '(', ')', '[', ']', '{', '}', '<', '>' };
        int[] values = { 0, 3, 57, 1197, 25137 };

        public Day10()
        {
            string[] data = Global.GetArgs("Day10.txt");

            Part2(data);
        }

        public void Part1(string[] data)
        {
            int count = 0;

            for (int i = 0; i < data.Length; i++)
            {
                List<int> pile = new List<int>();
                count += values[(ErrorOnLine(data[i], i, ref pile) + 1) / 2];
            }

            Console.WriteLine(count);
        }

        public void Part2(string[] data)
        {
            List<ulong> autoComplete = new List<ulong>();

            for (int i = 0; i < data.Length; i++)
            {
                List<int> pile = new List<int>();
                int code = ErrorOnLine(data[i], i, ref pile);
                if (pile.Count >= 0 && code == -1)
                {
                    autoComplete.Add(AutoComplete(pile));
                }
            }

            autoComplete.Sort();
            Console.WriteLine(autoComplete[autoComplete.Count / 2]);
        }

        public int ErrorOnLine(string line, int i, ref List<int> pile)
        {
            foreach (char letter in line)
            {
                int val = Array.FindIndex(possible, a => a == letter);
                if (val % 2 == 0) {
                    pile.Add(val);
                }
                else if (!(val - 1 == pile[pile.Count - 1]))
                {
                    Console.WriteLine($"{i} : Error on char '{possible[val]}' instead of '{possible[pile[pile.Count - 1] + 1]}'");
                    return val;
                }
                else
                {
                    pile.RemoveAt(pile.Count - 1);
                }
            }
            return -1;
        }

        public ulong AutoComplete(List<int> pile)
        {
            ulong count = 0;

            for (int i = pile.Count - 1; i >= 0; i--)
            {
                count *= 5;
                count += (ulong)(pile[i] / 2 + 1);
            }
            Console.WriteLine($"Incomplete line : {count}");

            return count;
        }
    }
}
