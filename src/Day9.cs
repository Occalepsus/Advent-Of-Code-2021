using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day9
    {
        int[,] heightMap;

        public Day9()
        {
            string[] inputs = Global.GetArgs("Day9.txt");

            heightMap = new int[inputs.Length, inputs[0].Length];
            for (int line = 0; line < heightMap.GetLength(0); line++)
            {
                for (int col = 0; col < heightMap.GetLength(1); col++)
                {
                    heightMap[line, col] = inputs[line][col] - '0';
                }
            }
            Part2();
        }

        private void Part2()
        {
            List<(int, int)> tubes = new List<(int, int)>();

            for (int line = 0; line < heightMap.GetLength(0); line++)
            {
                for (int col = 0; col < heightMap.GetLength(1); col++)
                {
                    int height = heightMap[line, col];

                    bool isLower = true;
                    if (line > 0)
                    {
                        isLower &= height < heightMap[line - 1, col];
                    }
                    if (col > 0)
                    {
                        isLower &= height < heightMap[line, col - 1];
                    }
                    if (line < heightMap.GetLength(0) - 1)
                    {
                        isLower &= height < heightMap[line + 1, col];
                    }
                    if (col < heightMap.GetLength(1) - 1)
                    {
                        isLower &= height < heightMap[line, col + 1];
                    }

                    if (isLower)
                    {
                        //Console.WriteLine($"{line}, {col} = {height}");
                        tubes.Add((line, col));
                    }
                }
            }

            List<(int, int)>[] bassins = new List<(int, int)>[tubes.Count];

            int first = 0;
            int fstQ = 0;
            int second = 0;
            int secQ = 0;
            int third = 0;
            int trdQ = 0;

            for (int i = 0; i < tubes.Count; i++)
            {
                bassins[i] = FindBassin(tubes[i], tubes);
                if (bassins[i].Count > fstQ)
                {
                    third = second; trdQ = secQ;
                    second = first; secQ = fstQ;
                    first = i; fstQ = bassins[i].Count;
                }
                else if (bassins[i].Count > secQ)
                {
                    third = second; trdQ = secQ;
                    second = i; secQ = bassins[i].Count;
                }
                else if (bassins[i].Count > trdQ)
                {
                    third = i; trdQ = bassins[i].Count;
                }
            }

            Console.WriteLine(fstQ * secQ * trdQ);
        }

        private List<(int, int)> FindBassin((int, int) tube, List<(int, int)> tubes)
        {
            List<(int, int)> bassin = new List<(int, int)>();

            bassin.Add(tube);

            for (int i = 0; i < bassin.Count; i++)
            {
                if (bassin[i].Item1 > 0 
                    && heightMap[bassin[i].Item1 - 1, bassin[i].Item2] != 9 && !bassin.Contains((bassin[i].Item1 - 1, bassin[i].Item2)))
                {
                    bassin.Add((bassin[i].Item1 - 1, bassin[i].Item2));
                    tubes.Remove(bassin[bassin.Count - 1]);
                }
                if (bassin[i].Item2 > 0 
                    && heightMap[bassin[i].Item1, bassin[i].Item2 - 1] != 9 && !bassin.Contains((bassin[i].Item1, bassin[i].Item2 - 1)))
                {
                    bassin.Add((bassin[i].Item1, bassin[i].Item2 - 1));
                    tubes.Remove(bassin[bassin.Count - 1]);
                }
                if (bassin[i].Item1 < heightMap.GetLength(0) - 1
                    && heightMap[bassin[i].Item1 + 1, bassin[i].Item2] != 9 && !bassin.Contains((bassin[i].Item1 + 1, bassin[i].Item2)))
                {
                    bassin.Add((bassin[i].Item1 + 1, bassin[i].Item2));
                    tubes.Remove(bassin[bassin.Count - 1]);
                }
                if (bassin[i].Item2 < heightMap.GetLength(1) - 1
                    && heightMap[bassin[i].Item1, bassin[i].Item2 + 1] != 9 && !bassin.Contains((bassin[i].Item1, bassin[i].Item2 + 1)))
                {
                    bassin.Add((bassin[i].Item1, bassin[i].Item2 + 1));
                    tubes.Remove(bassin[bassin.Count - 1]);
                }
            }
            return bassin;
        }


        private void Part1()
        {
            int value = 0;

            for (int line = 0; line < heightMap.GetLength(0); line++)
            {
                for (int col = 0; col < heightMap.GetLength(1); col++)
                {
                    int height = heightMap[line, col];

                    bool isLower = true;
                    if (line > 0)
                    {
                        isLower &= height < heightMap[line - 1, col];
                    }
                    if (col > 0)
                    {
                        isLower &= height < heightMap[line, col - 1];
                    }
                    if (line < heightMap.GetLength(0) - 1)
                    {
                        isLower &= height < heightMap[line + 1, col];
                    }
                    if (col < heightMap.GetLength(1) - 1)
                    {
                        isLower &= height < heightMap[line, col + 1];
                    }

                    if (isLower)
                    {
                        Console.WriteLine($"{line}, {col} = {height}");
                        value += height + 1;
                    }
                }
            }

            Console.WriteLine(value);
        }
    }
}
