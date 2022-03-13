using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public struct Table
    {
        public int i;
        public int j;
        private int[,] table;
        private bool[,] validation;

        public Table(string[] inputs)
        {
            i = 5;
            j = 5;
            table = new int[i, j];
            validation = new bool[i, j];
            for (int line = 0; line < i; line++)
            {
                //string[] rawVal = inputs[line].Replace("  ", " ").Split(new char[] { ' ' });
                for (int col = 0; col < j; col++)
                {
                    table[line, col] = int.Parse(inputs[line].Substring(3 * col, 2));
                    Console.Write(table[line, col] + " ");
                    validation[line, col] = false;
                }
                Console.Write("\n");
            }
            Console.Write("\n\n");
        }

        public bool TestNumber(int number)
        {
            for (int line = 0; line < i; line++)
            {
                for (int col = 0; col < j; col++)
                {
                    if (table[line, col] == number)
                    {
                        validation[line, col] = true;
                        return TestValidationAround(line, col);
                    }
                }
            }
            return false;
        }

        public bool TestValidationAround(int line, int col)
        {
            bool colState = true;
            for (int testCol = 0; testCol < i; testCol++)
            {
                colState &= validation[line, testCol];
            }
            bool lineState = true;
            for (int testLine = 0; testLine < j; testLine++)
            {
                lineState &= validation[testLine, col];
            }
            return colState || lineState;
        }

        public int CalculateSum()
        {
            int sum = 0;
            for (int line = 0; line < i; line++)
            {
                for (int col = 0; col < j; col++)
                {
                    if (!validation[line, col])
                    {
                        sum += table[line, col];
                    }
                }
            }
            return sum;
        }

        public override string ToString()
        {
            string name = "";
            for (int line = 0; line < i; line++)
            {
                for (int col = 0; col < j; col++)
                {
                    if (validation[line, col]) name += "o ";
                    else name += "x ";
                }
                name += "\n";
            }
            return name;
        }
    }

    class Day4
    {
        public Day4()
        {
            string[] inputs = Global.GetArgs("Day4.txt");
            int[] values;
            Table[] tables = GetTables(inputs, out values);

            Part2(tables, values);
        }

        private static Table[] GetTables(string[] inputs, out int[] values)
        {
            string[] rawValues = inputs[0].Split(new char[] { ',' });
            values = new int[rawValues.Length - 1];
            for (int i = 0; i < rawValues.Length - 1; i++)
            {
                values[i] = int.Parse(rawValues[i]);
            }

            int tableNb = (inputs.Length - 1) / 6;

            Table[] tables = new Table[tableNb];

            for (int i = 0; i < tableNb; i++)
            {
                Console.WriteLine($"Table nb {i}");
                tables[i] = new Table(inputs[new Range(i * 6 + 2, (i + 1) * 6 + 1)]);
            }
            return tables;
        }

        public void Part1(Table[] tables, int[] values)
        {
            foreach (int value in values)
            {
                for (int i = 0; i < tables.Length; i++)
                {
                    if (tables[i].TestNumber(value))
                    {
                        Console.WriteLine($"Ending score = {tables[i].CalculateSum() * value}");
                        return;
                    }
                }
            }
        }

        public void Part2(Table[] tables, int[] values)
        {
            bool[] hasWon = new bool[tables.Length];
            for (int i = 0; i < hasWon.Length; i++)
            {
                hasWon[i] = false;
            }

            int wonNb = 0;
            int last = 0;
            foreach (int value in values)
            {
                for (int i = 0; i < tables.Length; i++)
                {
                    if (!hasWon[i])
                    {
                        hasWon[i] = tables[i].TestNumber(value);
                        if (hasWon[i]) wonNb++;
                    }
                }
                if (wonNb == hasWon.Length - 1)
                {
                    for (int i = 0; i < hasWon.Length; i++)
                    {
                        if (!hasWon[i])
                        {
                            last = i;
                            break;
                        }
                    }
                }
                if (wonNb == hasWon.Length)
                {
                    Console.WriteLine($"Ending score = {tables[last].CalculateSum()}, {value} : {tables[last].CalculateSum() * value}");
                    return;
                }
            }
        }
    }
}
