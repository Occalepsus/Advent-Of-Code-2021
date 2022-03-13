using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day8
    {
        private string[][] states;

        public Day8()
        {
            string[] inputs = Global.GetArgs("Day8.txt");

            states = new string[inputs.Length][];

            Part2(inputs);
        }

        private void Part2(string[] inputs)
        {
            int count = 0;
            for (int line = 0; line < inputs.Length; line++)
            {
                string[] temp = inputs[line].Split(new char[] { '|' });
                string[] code = temp[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int[] decodeIdx = new int[10];
                List<int> possible5 = new List<int>(3);
                List<int> possible6 = new List<int>(3);

                char a;
                char b = ' ';
                char c = ' ';
                char d = ' ';
                char e = ' ';
                char f = ' ';
                char g = ' ';
                string cf;
                string bd;
                string eg;

                for (int i = 0; i < 10; i++)
                {
                    switch (code[i].Length)
                    {
                        case 2:
                            decodeIdx[1] = i;
                            break;
                        case 3:
                            decodeIdx[7] = i;
                            break;
                        case 4:
                            decodeIdx[4] = i;
                            break;
                        case 5:
                            possible5.Add(i);
                            break;
                        case 6:
                            possible6.Add(i);
                            break;
                        case 7:
                            decodeIdx[8] = i;
                            break;
                        default:
                            throw new Exception();
                    }
                }

                //I :
                cf = code[decodeIdx[1]];
                a = Diff(code[decodeIdx[7]], code[decodeIdx[1]])[0];
                bd = Diff(code[decodeIdx[4]], code[decodeIdx[1]]);
                eg = Diff(code[decodeIdx[8]], code[decodeIdx[7]] + code[decodeIdx[4]]);

                //II - III :
                foreach (int number in possible6)
                {
                    string diff = Diff(code[decodeIdx[8]], code[number]);

                    //Si la différence entre 8 et number est dans cf, alors number est 6, et c est la diff entre 6 et 8
                    if (IsaInb(diff, cf))
                    {
                        decodeIdx[6] = number;
                        c = diff[0];
                        f = Diff(cf, c.ToString())[0];
                    }
                    else if (IsaInb(diff, eg))
                    {
                        decodeIdx[9] = number;
                        e = diff[0];
                        g = Diff(eg, e.ToString())[0];
                    }
                    else if (IsaInb(diff, bd))
                    {
                        decodeIdx[0] = number;
                        d = diff[0];
                        b = Diff(bd, d.ToString())[0];
                    }
                }

                //IV :
                foreach (int number in possible5)
                {
                    if (Equ(code[number], "" + a + c + d + e + g))
                    {
                        decodeIdx[2] = number;
                    }
                    else if (Equ(code[number], "" + a + c + d + f + g))
                    {
                        decodeIdx[3] = number;
                    }
                    else if (Equ(code[number], "" + a + b + d + f + g))
                    {
                        decodeIdx[5] = number;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(decodeIdx[i]);
                }
                Console.WriteLine();



                //V :
                string[] values = temp[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int result = 0;

                foreach (string value in values)
                {
                    result *= 10;
                    for (int number = 0; number < 10; number++)
                    {
                        if (Equ(value, code[decodeIdx[number]]))
                        {
                            result += number;
                            break;
                        }
                    }
                }
                count += result;
                Console.WriteLine(temp[1] + " : " + result);
            }

            Console.WriteLine(count);
        }

        public static bool Equ(string a, string b)
        {
            return IsaInb(a, b) && IsaInb(b, a);
        }

        public static string Diff(string big, string small)
        {
            string result = "";
            for (int i = 0; i < big.Length; i++)
            {
                for (int j = 0; j < small.Length; j++)
                {
                    if (big[i] == small[j]) goto End;
                }
                result += big[i];
                End:;
            }
            return result;
        }

        public static bool IsaInb(string a, string b)
        {
            foreach (char letter in a)
            {
                foreach (char comp in b)
                {
                    if (letter == comp)
                    {
                        goto End;
                    }
                }
                return false;
            End:;
            }
            return true;
        }

        private void Part1(string[] inputs)
        {
            int occ1 = 0;
            int occ4 = 0;
            int occ7 = 0;
            int occ8 = 0;


            for (int line = 0; line < inputs.Length; line++)
            {
                states[line] = inputs[line].Split(new string[] { "|" }, StringSplitOptions.None)[1].Split(new char[] { ' ' }, StringSplitOptions.None);

                for (int i = 0; i < states[line].Length; i++)
                {
                    Console.Write($"{states[line][i]}, ");
                    switch (states[line][i].Length)
                    {
                        case 2:
                            occ1++;
                            break;
                        case 3:
                            occ7++;
                            break;
                        case 4:
                            occ4++;
                            break;
                        case 7:
                            occ8++;
                            break;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(occ1 + occ4 + occ7 + occ8);
        }

        public static string EscapeIt(string value)
        {
            var builder = new StringBuilder();
            foreach (var cur in value)
            {
                switch (cur)
                {
                    case '\t':
                        builder.Append(@"\t");
                        break;
                    case '\r':
                        builder.Append(@"\r");
                        break;
                    case '\n':
                        builder.Append(@"\n");
                        break;
                    // etc ...
                    default:
                        builder.Append(cur);
                        break;
                }
            }
            return builder.ToString();
        }
    }
}
