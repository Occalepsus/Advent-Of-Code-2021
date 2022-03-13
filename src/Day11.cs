using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day11
    {
        public Day11()
        {
            string[] inputs = Global.GetArgs("Day11.txt");

            int[,] data = GetData(inputs);

            Part2(data);
        }

        public int[,] GetData(string[] inputs)
        {
            int[,] data = new int[inputs.Length, inputs[0].Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    data[i, j] = int.Parse(inputs[i][j].ToString());
                }
            }
            return data;
        }

        private void Part1(int[,] data, int waves)
        {
            int count = 0;
            for (int i = 0; i < waves; i++)
            {
                count += Step(data);
            }

            Console.WriteLine(count);
        }

        private void Part2(int[,] data)
        {
            int i = 0;
            int count;
            do
            {
                count = Step(data);
                i++;
            }
            while (count < data.GetLength(0) * data.GetLength(1));
            Console.WriteLine(i);
        }

        private void Blink(int[,] data, bool[,] hadBlinked, int i, int j)
        {
            data[i, j] = 0;
            hadBlinked[i, j] = true;
            for (int iOff = Math.Max(0, i - 1); iOff <= Math.Min(i + 1, data.GetLength(0) - 1); iOff++)
            {
                for (int jOff = Math.Max(0, j - 1); jOff <= Math.Min(j + 1, data.GetLength(1) - 1); jOff++)
                {
                    if (!hadBlinked[iOff, jOff])
                    {
                        data[iOff, jOff]++;
                        if (data[iOff, jOff] > 9)
                        {
                            Blink(data, hadBlinked, iOff, jOff);
                        }
                    }
                }
            }
        }

        private int Step(int[,] data)
        {
            bool[,] hadBlinked = new bool[data.GetLength(0), data.GetLength(1)];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j]++;
                    hadBlinked[i, j] = false;
                }
            }
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i,j] > 9)
                    {
                        Blink(data, hadBlinked, i, j);
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (hadBlinked[i, j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void DisplayData(int[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
