using System;

namespace AdventOfCode
{
    class Day1
    {
        public Day1()
        {
            Console.WriteLine("Hello World!");

            string[] inputs = GetArgs("Day1.txt");

            Console.WriteLine(Part2(inputs));
        }

        static string[] GetArgs(string fileName)
        {
            string rawText = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\" + fileName);

            return rawText.Split(new char[] { '\n' });
        }

        static int Part1(string[] inputs)
        {
            int count = 0;

            for (int i = 0; i < inputs.Length - 1; i++)
            {
                if (int.Parse(inputs[i]) < int.Parse(inputs[i + 1]))
                {
                    count++;
                }
            }
            return count;
        }

        static int Part2(string[] inputs)
        {
            int count = 0;

            int[] low = new int[] { int.Parse(inputs[0]), int.Parse(inputs[1]), int.Parse(inputs[2]) };
            int[] high = new int[] { int.Parse(inputs[1]), int.Parse(inputs[2]), int.Parse(inputs[3]) };

            for (int i = 1; i < inputs.Length - 2; i++)
            {
                if (Sum(low) < Sum(high))
                {
                    count++;
                }
                low = high;
                high = new int[] { int.Parse(inputs[i]), int.Parse(inputs[i + 1]), int.Parse(inputs[i + 2]) };
            }
            if (Sum(low) < Sum(high))
            {
                count++;
            }
            return count;
        }

        static int Sum(int[] array)
        {
            int count = 0;
            foreach (int item in array)
            {
                count += item;
            }
            return count;
        }
    }
}