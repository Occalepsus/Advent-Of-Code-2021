using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day2
    {
        public Day2()
        {
            string[] inputs = GetArgs("Day2.txt");

            Console.WriteLine(Part2(inputs));
        }

        static int Part1(string[] inputs)
        {
            int forward = 0;
            int depht = 0;

            foreach (string input in inputs)
            {
                int[] data = TreatData(input);
                forward += data[0];
                depht += data[1];
            }
            return forward * depht;
        }
        
        static int Part2(string[] inputs)
        {
            int aim = 0;
            int forward = 0;
            int depht = 0;

            foreach (string input in inputs)
            {
                bool hasAdvanced;
                int[] data = TreatData2(input, out hasAdvanced);
                forward += data[0];
                aim += data[1];

                if (hasAdvanced)
                {
                    depht += aim * data[0];
                }
            }
            return forward * depht;
        }

        static int[] TreatData2(string input, out bool hasAdvanced)
        {
            hasAdvanced = false;
            string[] data = input.Split(new char[] { ' ' });

            int[] result = new int[2];
            if (data[0] == "forward")
            {
                result[0] += int.Parse(data[1]);
                hasAdvanced = true;
            }
            else if (data[0] == "up")
            {
                result[1] -= int.Parse(data[1]);
            }
            else if (data[0] == "down")
            {
                result[1] += int.Parse(data[1]);
            }
            else
            {
                throw new System.Exception();
            }
            return result;
        }

        static int[] TreatData(string input)
        {
            string[] data = input.Split(new char[] { ' ' });

            int[] result = new int[2];
            if (data[0] == "forward")
            {
                result[0] += int.Parse(data[1]);
            }
            else if (data[0] == "up")
            {
                result[1] += int.Parse(data[1]);
            }
            else if (data[0] == "down")
            {
                result[1] -= int.Parse(data[1]);
            }
            else
            {
                throw new System.Exception();
            }
            return result;
        }
        

        static string[] GetArgs(string fileName)
        {
            string rawText = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\" + fileName);

            return rawText.Split(new char[] { '\n' });
        }
    }
}
