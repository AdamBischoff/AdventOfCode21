using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode21._1
{
    class Main
    {
        public static void Program1()
        {
            int[] file = getFile();
            int lastDepth = file[0];
            int timesIncreased = 0;

            foreach (var line in file)
            {
                int depth = line;

                if (depth > lastDepth)
                    timesIncreased++;

                lastDepth = depth;
            }
            Console.WriteLine("Result = " + timesIncreased);
        }

        public static void Program2()
        {
            int[] file = getFile();
            int timesIncreased = 0;

            int lastDepth = file[0] + file[1] + file[2];

            for (int i = 3; i < file.Length; i++)
            {
                int newDepth = lastDepth - file[i - 3] + file[i];

                if (newDepth > lastDepth)
                    timesIncreased++;

                lastDepth = newDepth;
            }

            Console.WriteLine("Result = " + timesIncreased);
        }

        public static int[] getFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\adam\git\AdventOfCode21\1\Data.txt");
            List<int> res = new List<int>();
            foreach (string line in lines)
            {
                res.Add(int.Parse(line));
            }

            return res.ToArray();
        }
    }
}
