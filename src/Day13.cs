using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day13
    {
        public Day13()
        {
            string[] inputs = Global.GetArgs("Day13.txt");

            (int, bool)[] folds;
            (int, int)[] points = ManageData(inputs, out folds);

            Console.WriteLine(Part2(points, folds, 1500));
        }

        private string Part1((int, int)[] points, (int, bool) fold, int size)
        {
            bool[,] matrix = new bool[size, size];

            int lastX = size - 1;
            int lastY = size - 1;

            foreach ((int, int) point in points)
            {
                matrix[point.Item1, point.Item2] = true;
            }
            //Console.Write("Fold : ");
            if (fold.Item2)
            {
                //Console.WriteLine("x");
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < fold.Item1; x++)
                    {
                        if (matrix[x, y])
                        {
                            matrix[2 * fold.Item1 - x, y] = true;
                        }
                    }
                }
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size - fold.Item1; x++)
                    {
                        matrix[x, y] = matrix[x + fold.Item1, y];
                    }
                }
                lastX = fold.Item1;
            }
            else
            {
                //Console.WriteLine("y");
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < fold.Item1; y++)
                    {
                        if (matrix[x, y])
                        {
                            matrix[x, 2 * fold.Item1 - y] = true;
                        }
                    }
                }
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size - fold.Item1; y++)
                    {
                        matrix[x, y] = matrix[x, y + fold.Item1];
                    }
                }
                lastY = fold.Item1;
                
            }

            //Console.WriteLine($"x: {lastX}, y: {lastY}");
            int count = 0;
            for (int x = 0; x <= lastX; x++)
            {
                for (int y = 0; y <= lastY; y++)
                {
                    if (matrix[x, y]) count++;
                }
            }

            //DisplayMatrix(matrix);

            return count.ToString();
        }

        private string Part2((int, int)[] points, (int, bool)[] folds, int size)
        {
            bool[,] matrix = new bool[size, size];

            int lastX = size;
            int lastY = size;

            foreach ((int, int) point in points)
            {
                matrix[point.Item1, point.Item2] = true;
            }
            foreach ((int, bool) fold in folds)
            {
                //Console.Write("Fold : ");
                if (fold.Item2)
                {
                    //Console.WriteLine("x");
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < fold.Item1; x++)
                        {
                            if (matrix[x, y])
                            {
                                matrix[2 * fold.Item1 - x, y] = true;
                            }
                        }
                    }
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < size - fold.Item1; x++)
                        {
                            matrix[x, y] = matrix[x + fold.Item1, y];
                        }
                    }
                    lastX = fold.Item1;
                }
                else
                {
                    //Console.WriteLine("y");
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < fold.Item1; y++)
                        {
                            if (matrix[x, y])
                            {
                                matrix[x, 2 * fold.Item1 - y] = true;
                            }
                        }
                    }
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size - fold.Item1; y++)
                        {
                            matrix[x, y] = matrix[x, y + fold.Item1];
                        }
                    }
                    lastY = fold.Item1;
                }
            }

            //Console.WriteLine($"x: {lastX}, y: {lastY}");
            int count = 0;
            for (int x = 0; x <= lastX; x++)
            {
                for (int y = 0; y <= lastY; y++)
                {
                    if (matrix[x, y]) count++;
                }
            }

            DisplayMatrix(matrix);

            return count.ToString();
        }

        private (int, int)[] ManageData(string[] inputs, out (int, bool)[] fold)
        {
            List<(int, int)> dataList = new List<(int, int)>();
            List<(int, bool)> foldList = new List<(int, bool)>();

            bool state = false;
            foreach (string line in inputs)
            {
                //Console.WriteLine(line);
                if (!state && line[0] == 'f')
                {
                    state = true;
                }
                else if (!state)
                {
                    string[] raw = line.Split(",");
                    dataList.Add((int.Parse(raw[0]), int.Parse(raw[1])));
                }
                if (state)
                {
                    //Console.WriteLine("hi!");
                    string[] raw = line.Split("=");
                    foldList.Add((int.Parse(raw[1]), raw[0][11] == 'x'));
                }
            }

            fold = foldList.ToArray();
            return dataList.ToArray();
        }

        private void DisplayMatrix(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
