using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode21._2
{
    class Main
    {
        public static void Program1()
        {
            int depth = 0;
            int horizontal = 0;

            foreach (string line in getFile())
            {
                string[] splitLine = line.Split(" ");
                string command = splitLine[0];
                int amount = int.Parse(splitLine[1]);

                if (command == "forward")
                {
                    horizontal += amount;
                } else if (command == "down")
                {
                    depth += amount;
                } else if (command == "up")
                {
                    depth -= amount;
                } else
                {
                    Console.WriteLine("Unexpected command: " + command);
                    return;
                }
            }

            Console.WriteLine("Horizontal: " + horizontal);
            Console.WriteLine("Depth: " + depth);
            Console.WriteLine("Result = " + horizontal * depth);
        }

        public static void Program2()
        {
            int depth = 0;
            int horizontal = 0;
            int aim = 0;

            foreach (string line in getFile())
            {
                string[] splitLine = line.Split(" ");
                string command = splitLine[0];
                int amount = int.Parse(splitLine[1]);

                if (command == "forward")
                {
                    horizontal += amount;
                    depth += aim * amount;
                }
                else if (command == "down")
                {
                    aim += amount;
                }
                else if (command == "up")
                {
                    aim -= amount;
                }
                else
                {
                    Console.WriteLine("Unexpected command: " + command);
                    return;
                }
            }

            Console.WriteLine("Horizontal: " + horizontal);
            Console.WriteLine("Depth: " + depth);
            Console.WriteLine("Result = " + horizontal * depth);
        }


        public static string[] getFile()
        {
            return System.IO.File.ReadAllLines(@"C:\Users\adam\git\AdventOfCode21\2\Data.txt");
        }
    }
}
