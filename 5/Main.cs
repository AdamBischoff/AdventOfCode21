using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode21._5
{
    class Main
    {
        internal static void Program1()
        {
            string[] input = ReadFile();
            Line[] lines = new Line[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                Line newLine = new Line();
                string[] split = input[i].Split(',');

                newLine.X1 = int.Parse(split[0]);
                newLine.Y2 = int.Parse(split[2]);

                split = split[1].Split(" -> ");

                newLine.Y1 = int.Parse(split[0]);
                newLine.X2 = int.Parse(split[1]);

                lines[i] = newLine;
            }

            lines = lines.Where(l => l.IsHorizontalOrVertical()).ToArray();

            int height = lines.Max(l => l.GetMaxHeight());
            int width = lines.Max(l => l.GetMaxWidth());

            Diagram diagram = new Diagram(width, height);

            foreach (Line line in lines)
            {
                diagram.AddLine(line);
            }

            Console.WriteLine("Points with more than two lines: " + diagram.PointsWithMoreThanTwo());

        }

        internal static void Program2()
        {
            string[] input = ReadFile();
            Line[] lines = new Line[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                Line newLine = new Line();
                string[] split = input[i].Split(',');

                newLine.X1 = int.Parse(split[0]);
                newLine.Y2 = int.Parse(split[2]);

                split = split[1].Split(" -> ");

                newLine.Y1 = int.Parse(split[0]);
                newLine.X2 = int.Parse(split[1]);

                lines[i] = newLine;
            }

            int height = lines.Max(l => l.GetMaxHeight());
            int width = lines.Max(l => l.GetMaxWidth());

            Diagram diagram = new Diagram(width, height);

            foreach (Line line in lines)
            {
                diagram.AddLineWithDiagonal(line);
            }

            Console.WriteLine("Points with more than two lines: " + diagram.PointsWithMoreThanTwo());
        }

        private static string[] ReadFile()
        {
            return System.IO.File.ReadAllLines(@"C:\Users\adam\git\AdventOfCode21\5\Data.txt");
        }
    }

    class Diagram
    {
        public int[][] Board;

        public Diagram(int width, int height)
        {
            Board = new int[width][];
            
            for (int i = 0; i < width; i++)
            {
                Board[i] = new int[height];
            }
        }

        public void AddLine(Line line)
        {
            bool isHorizontal = line.Y1 == line.Y2;
            bool isGrowing = line.Y1 < line.Y2 || line.X1 < line.X2;

            if (isHorizontal)
            {
                int tmpX1 = line.X1;
                int stopX2 = line.X2 + (isGrowing ? 1 : -1);
                while (tmpX1 != stopX2)
                {
                    Board[tmpX1][line.Y1]++;
                    tmpX1 += isGrowing ? 1 : -1;
                }
            } else
            {
                int tmpY1 = line.Y1;
                int stopY2 = line.Y2 + (isGrowing ? 1 : -1);
                while (tmpY1 != stopY2)
                {
                    Board[line.X1][tmpY1]++;
                    tmpY1 += isGrowing ? 1 : -1;
                }
            }
        }

        public void AddLineWithDiagonal(Line line)
        {
            if (line.IsHorizontalOrVertical()) 
            {
                AddLine(line);
                return;
            }

            bool isGrowingX = line.X1 < line.X2;
            bool isGrowingY = line.Y1 < line.Y2;

            int tmpX1 = line.X1;
            int tmpY1 = line.Y1;
            int stopX2 = line.X2 + (isGrowingX ? 1 : -1);

            while (tmpX1 != stopX2) //Could also check tmpY1 != Y2, but is the same
            {
                Board[tmpX1][tmpY1]++;
                tmpX1 += isGrowingX ? 1 : -1;
                tmpY1 += isGrowingY ? 1 : -1;
            }
        }

        public int PointsWithMoreThanTwo()
        {
            int res = 0; 

            for (int row = 0; row < Board.Length; row++)
            {
                for (int col = 0; col < Board[row].Length; col++)
                {
                    res += Board[row][col] >= 2 ? 1 : 0;
                }
            }
            return res;
        }
    }

    class Line
    {
        public int X1, Y1, X2, Y2;

        public bool IsHorizontalOrVertical()
        {
            return X1 == X2 || Y1 == Y2;
        }

        public int GetMaxHeight()
        {
            return Math.Max(Y1, Y2) + 1;
        }

        public int GetMaxWidth()
        {
            return Math.Max(X1, X2) + 1;
        }
    }
}
