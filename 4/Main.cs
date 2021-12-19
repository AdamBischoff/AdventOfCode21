using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode21._4
{
    class Main
    {
        internal static void Program1()
        {
            const int BOARD_SIZE = 5;
            string[] input = ReadFile();

            int[] drawnNumbers = Array.ConvertAll(input[0].Split(','), s => int.Parse(s));

            List<Board> boards = new List<Board>();

            for (int i = 2; i < input.Length; i += BOARD_SIZE + 1)
            {
                string[] boardLines = new string[BOARD_SIZE];

                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    boardLines[j] = input[i + j];
                }

                boards.Add(new Board(boardLines));
            }


            Board winner = null;
            int winningNumber = 0;

            for (int i = 0; i < drawnNumbers.Length; i++)
            {
                if (winner != null) break;

                boards.ForEach(b => b.MarkNumber(drawnNumbers[i]));

                foreach (Board board in boards)
                {
                    if (board.HasWon())
                    {
                        winner = board;
                        winningNumber = drawnNumbers[i];
                        break;
                    }
                }
            }

            if (winner == null)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    boards[i].PrintBoard();
                    Console.WriteLine();
                }
            } else
            {
                winner.PrintBoard();
                Console.WriteLine("Score " + winningNumber * winner.SumOfUnmarked());
            }
        }

        internal static void Program2()
        {
            const int BOARD_SIZE = 5;
            string[] input = ReadFile();

            int[] drawnNumbers = Array.ConvertAll(input[0].Split(','), s => int.Parse(s));

            List<Board> boards = new List<Board>();

            for (int i = 2; i < input.Length; i += BOARD_SIZE + 1)
            {
                string[] boardLines = new string[BOARD_SIZE];

                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    boardLines[j] = input[i + j];
                }

                boards.Add(new Board(boardLines));
            }


            Board lastWinner = null;
            int winningScore = 0;

            List<Board> toRemove = new List<Board>();

            for (int i = 0; i < drawnNumbers.Length; i++)
            {
                boards.ForEach(b => b.MarkNumber(drawnNumbers[i]));

                foreach (Board board in boards)
                {
                    if (board.HasWon())
                    {
                        lastWinner = board;
                        winningScore = drawnNumbers[i] * board.SumOfUnmarked();
                        toRemove.Add(board);
                    }
                }
                toRemove.ForEach(b => boards.Remove(b));
                toRemove.Clear();
            }

            if (lastWinner == null)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    boards[i].PrintBoard();
                    Console.WriteLine();
                }
            }
            else
            {
                lastWinner.PrintBoard();
                Console.WriteLine("Score " + winningScore);
            }
        }

        private static string[] ReadFile()
        {
            return System.IO.File.ReadAllLines(@"C:\Users\adam\git\AdventOfCode21\4\Data.txt");
        }
    }

    class Board
    {
        Number[][] Numbers;
        
        public Board(string[] lines)
        {
            Numbers = new Number[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                Numbers[i] = new Number[lines.Length];
            }


            for (int i = 0; i < lines.Length; i++)
            {
                var rowNumbers = lines[i].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                int[] row = Array.ConvertAll(rowNumbers, s => int.Parse(s));

                for (int j = 0; j < row.Length; j++)
                {
                    Numbers[i][j] = new Number(row[j]);
                }
            }
        }

        public void MarkNumber(int drawnNumber)
        {
            foreach (Number[] row in Numbers)
                foreach (Number num in row)
                    num.CheckNumber(drawnNumber);        
        }

        public bool HasWon()
        {
            //Check rows
            foreach (Number[] row in Numbers)
            {
                if (Array.TrueForAll(row, n => n.Marked))
                    return true;
            }

            //Check columns
            for (int col = 0; col < Numbers.Length; col++)
            {
                bool allMarked = true;
                for (int row = 0; row < Numbers.Length; row++)
                {
                    if (!Numbers[row][col].Marked)
                    {
                        allMarked = false;
                        break;
                    }
                }

                if (allMarked) return true;
            }

            return false;
        }

        public int SumOfUnmarked()
        {
            int sum = 0;
            foreach (Number[] row in Numbers)
            {
                foreach (Number num in row)
                {
                    sum += num.Marked ? 0 : num.Value;
                }
            }

            return sum;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Numbers[i][j].Marked) Console.Write("*");
                    Console.Write(Numbers[i][j].Value + "\t");
                }
                Console.WriteLine();
            }
        }
    } 

    class Number
    {
        public int Value;
        public bool Marked;

        public Number(int Value)
        {
            this.Value = Value;
            Marked = false;
        }

        public void CheckNumber(int drawnNumber)
        {
            if (Value == drawnNumber)
            {
                Marked = true;
            }
        }
    }
}
