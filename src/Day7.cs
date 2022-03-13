using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day7
    {
        private int[] positions;

        public Day7()
        {
            string[] inputs = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\Day7.txt").Split(new char[] { ',' });
            positions = new int[inputs.Length];
            
            for (int i = 0; i < inputs.Length; i++)
            {
                positions[i] = int.Parse(inputs[i]);
            }

            Part2();
        }

        private void Part1()
        {
            Array.Sort(positions);
            int median = positions[positions.Length>>1];

            int quantity = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                quantity += Math.Abs(positions[i] - median);
            }

            Console.WriteLine(quantity);
        }

        private void Part2()
        {
            int sum = 0;
            foreach (int val in positions)
            {
                sum += val;
            }
            int mean = sum / positions.Length;

            int quantity = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                quantity += CalcConsumption(Math.Abs(positions[i] - mean));
            }

            Console.WriteLine(quantity);
        }

        private int CalcConsumption(int dist)
        {
            switch (dist)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                default:
                    return CalcConsumption(dist - 1) + dist;
            }
        }
    }
}
