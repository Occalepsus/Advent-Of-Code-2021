using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Day6
    {
        private List<int> states;

        private System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        public Day6()
        {
            string[] inputs = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\Day6.txt").Split(new char[] { ',' });

            states = new List<int>();

            Array.ForEach<string>(inputs, a => states.Add(int.Parse(a)));

            Part2(256);
        }

        private void Part2(int iterations)
        {
            ulong[] bornAtDay = new ulong[iterations + 9];

            states.ForEach(a => bornAtDay[a]++);

            ulong count = 0;
            for (int day = 0; day < iterations + 9; day++)
            {
                for (int child = day + 9; child < iterations + 9; child += 7)
                {
                    bornAtDay[child] += bornAtDay[day];
                }
                count += bornAtDay[day];
                Console.Write($"{bornAtDay[day]}, ");
            }
            Console.WriteLine();
            Console.WriteLine(count);
        }




        private void Part1(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                DoIteration(states);
            }
            Console.WriteLine(states.Count);
        }

        private void DoIteration(List<int> states)
        {
            int born = 0;
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] == 0)
                {
                    states[i] = 6;
                    born++;
                }
                else if (states[i] > 0)
                {
                    states[i]--;
                }
                else throw new Exception();
            }
            for (int i = 0; i < born; i++)
            {
                states.Add(8);
            }
        }
    }
}
