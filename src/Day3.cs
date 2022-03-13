using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day3
    {
        public Day3()
        {
            string[] inputs = GetArgs("Day3.txt");
            Part2(inputs);
        }


        static string[] GetArgs(string fileName)
        {
            string rawText = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\" + fileName);

            return rawText.Split(new char[] { '\n' });
        }

        static string Part1(string[] inputs)
        {
            int bits = inputs[0].Length - 1;
            int[] most = new int[bits];

            foreach (string input in inputs)
            {
                for (int i = 0; i < bits; i++)
                {
                    if (input[i] == '1')
                    {
                        most[i]++;
                    }
                }
            }

            int gamma = 0;
            int epsilon = 0;

            for (int i = 0; i < bits; i++)
            {
                gamma *= 2;
                epsilon *= 2;

                if (most[i] >= inputs.Length / 2f)
                {
                    gamma++;
                }
                else
                {
                    epsilon++;
                }
            }
            Console.WriteLine($"gamma : {gamma}, epsilon : {epsilon}");
            return (gamma * epsilon).ToString();
        }

        private static void Part2(string[] inputs)
        {
            int bits = inputs[0].Length - 1;

            List<string> oxygenLeft = new List<string>(inputs);
            List<string> CO2Left = new List<string>(inputs);

            for (int i = 0; i < bits; i++)
            {
                if (oxygenLeft.Count > 1)
                {
                    List<string> ox1 = new List<string>();
                    List<string> ox0 = new List<string>();

                    oxygenLeft.ForEach(a =>
                    {
                        if (a[i] == '1')
                        {
                            ox1.Add(a);
                        }
                        else
                        {
                            ox0.Add(a);
                        }
                    });

                    if (ox1.Count >= ox0.Count)
                    {
                        oxygenLeft = ox1;
                    }
                    else
                    {
                        oxygenLeft = ox0;
                    }
                }

                if (CO2Left.Count > 1)
                {
                    List<string> co20 = new List<string>();
                    List<string> co21 = new List<string>();

                    CO2Left.ForEach(a =>
                    {
                        if (a[i] == '1')
                        {
                            co21.Add(a);
                        }
                        else
                        {
                            co20.Add(a);
                        }
                    });

                    if (co21.Count >= co20.Count)
                    {
                        CO2Left = co20;
                    }
                    else
                    {
                        CO2Left = co21;
                    }
                }
            }

            Console.WriteLine(Base2Parser(oxygenLeft[0].Substring(0, bits)) * Base2Parser(CO2Left[0].Substring(0, bits)));
        }

        public static int Base2Parser(string base2)
        {
            int number = 0;
            foreach (char bit in base2)
            {
                number <<= 1;// *= 2;
                if (bit == '1')
                {
                    number++;
                }
            }
            return number;
        }
    }
}
