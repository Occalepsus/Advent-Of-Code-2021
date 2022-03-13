using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day5
    {
        public Day5()
        {
            string[] inputs = Global.GetArgs("Day5.txt");

            List<int[]> data = GetData(inputs, false);

            Part2(data, 1000);
        }

        private List<int[]> GetData(string[] inputs, bool select)
        {
            List<int[]> globalData = new List<int[]>();

            for (int index = 0; index < inputs.Length; index++)
            {
                int[] data = new int[4];
                string[] rawData = inputs[index].Split(new string[] { ",", " -> " }, StringSplitOptions.None);
                for (int i = 0; i < 4; i++)
                {
                    data[i] = int.Parse(rawData[i]);
                }

                if (!select || (data[0] == data[2] || data[1] == data[3]))
                {
                    globalData.Add(data);
                }
            }
            return globalData;
        }

        private void Part2(List<int[]> data, int max)
        {
            List<(int, int)> coords = new List<(int, int)>();

            data.ForEach(a => coords.AddRange(GetCoords2(a)));

            Console.WriteLine(coords.Count);

            int[,] map = new int[max, max];
            coords.ForEach(a => map[a.Item2, a.Item1]++);

            int count = 0;
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    //Console.Write(map[i, j]);
                    if (map[i, j] > 1)
                    {
                        count++;
                    }
                }
                //Console.WriteLine();
            }
            Console.WriteLine($"Trouvé ! {count}");
        }

        private List<(int,int)> GetCoords2(int[] data)
        {
            int dx = data[2] - data[0];
            int dy = data[3] - data[1];

            if (dx * dy == 0)
            {
                return GetCoords(data);
            }
            else if (dx > 0 && dy > 0)
            {
                List<(int, int)> coords = new List<(int, int)>();

                for (int i = 0; i <= dx; i++)
                {
                    coords.Add((data[0] + i, data[1] + i));
                }
                return coords;
            }
            else if (dx < 0 && dy > 0)
            {
                List<(int, int)> coords = new List<(int, int)>();

                for (int i = 0; i <= -dx; i++)
                {
                    coords.Add((data[0] - i, data[1] + i));
                }
                return coords;
            }
            else if (dx < 0 && dy < 0)
            {
                List<(int, int)> coords = new List<(int, int)>();

                for (int i = 0; i <= -dx; i++)
                {
                    coords.Add((data[2] + i, data[3] + i));
                }
                return coords;
            }
            else if (dx > 0 && dy < 0)
            {
                List<(int, int)> coords = new List<(int, int)>();

                for (int i = 0; i <= dx; i++)
                {
                    coords.Add((data[0] + i, data[1] - i));
                }
                return coords;
            }
            
            else { throw new System.Exception(); }
        }




        private void Part1(List<int[]> data, int max)
        {
            List<(int, int)> coords = new List<(int, int)>();

            data.ForEach(a => coords.AddRange(GetCoords(a)));

            int[,] map = new int[max, max];
            coords.ForEach(a => map[a.Item1, a.Item2]++);


            int count = 0;
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (map[i,j] > 1)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine($"Trouvé ! {count}");
        }

        private List<(int, int)> GetCoords(int[] data)
        {
            if (data[0] == data[2] && data[1] <= data[3])
            {
                List<(int, int)> coords = new List<(int, int)>();
                for (int y = data[1]; y <= data[3]; y++)
                {
                    coords.Add((data[0], y));
                }
                return coords;
            }
            else if (data[0] == data[2] && data[1] > data[3])
            {
                List<(int, int)> coords = new List<(int, int)>();
                for (int y = data[3]; y <= data[1]; y++)
                {
                    coords.Add((data[0], y));
                }
                return coords;
            }
            else if (data[1] == data[3] && data[0] <= data[2])
            {
                List<(int, int)> coords = new List<(int, int)>();
                for (int x = data[0]; x <= data[2]; x++)
                {
                    coords.Add((x, data[1]));
                }
                return coords;
            }
            else if (data[1] == data[3] && data[0] > data[2])
            {
                List<(int, int)> coords = new List<(int, int)>();
                for (int x = data[2]; x <= data[0]; x++)
                {
                    coords.Add((x, data[1]));
                }
                return coords;
            }
            else
            {
                throw new System.Exception();
            }
        }
    }
}