using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    static class Global
    {
        static void Main()
        {
            new Day13();
        }

        public static string[] GetArgs(string fileName)
        {
            string rawText = System.IO.File.ReadAllText(@"C:\Users\Admin\OneDrive\Documents\Programmes\Petits scripts\AdventOfCode\" + fileName);

            return rawText.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
