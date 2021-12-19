using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode21._3
{
    class Main
    {
        public static void Program1()
        {
            const int NUMBER_LENGTH = 12;

            int[] report = GetFile();

            int[] bitOccurrences = new int[NUMBER_LENGTH];


            for (int i = 0; i < report.Length; i++)
            {
                for (int j = 0; j < NUMBER_LENGTH; j++)
                {
                    bitOccurrences[j] += GetBitDigit(report[i], j, NUMBER_LENGTH) ? 1 : 0;
                }
            }

            char[] gamma = new char[NUMBER_LENGTH];
            char[] epsilon = new char[NUMBER_LENGTH];

            for (int i = 0; i < NUMBER_LENGTH; i++)
            {
                if (bitOccurrences[i] * 2 >= report.Length)
                {
                    gamma[i] = '1';
                    epsilon[i] = '0';
                } else
                {
                    gamma[i] = '0';
                    epsilon[i] = '1';
                }
            }

            int gammaInt = Convert.ToInt32(new string(gamma), 2);
            int epsilonInt = Convert.ToInt32(new string(epsilon), 2);

            Console.WriteLine(gammaInt * epsilonInt);
        }

        public static void Program2()
        {
            const int NUMBER_LENGTH = 12;
            List<int> report = new List<int>(GetFile());

            List<int> oxygenList = new List<int>(report);

            for (int index = 0; index < NUMBER_LENGTH; index++)
            {
                int bitCount = GetBitCount(oxygenList, index, NUMBER_LENGTH);
                bool keepOne = bitCount * 2 >= oxygenList.Count();

                oxygenList = oxygenList.Where(x => GetBitDigit(x, index, NUMBER_LENGTH) == keepOne).ToList();

                if (oxygenList.Count == 1)
                    break;
            }
            int oxygenRating = oxygenList[0];

            List<int> co2List = new List<int>(report);

            for (int index = 0; index < NUMBER_LENGTH; index++)
            {
                int bitCount = GetBitCount(co2List, index, NUMBER_LENGTH);
                bool keepOne = bitCount * 2 < co2List.Count();

                co2List = co2List.Where(x => GetBitDigit(x, index, NUMBER_LENGTH) == keepOne).ToList();

                if (co2List.Count == 1)
                    break;
            }
            int co2Rating = co2List[0];

            Console.WriteLine("Oxygen: " + oxygenRating + " | Co2: " + co2Rating);

            Console.WriteLine("Part2: " + (oxygenRating * co2Rating));
        }

        private static int GetBitCount(List<int> numbers, int digit, int length)
        {
            int bitCount = 0;
            int listCount = numbers.Count;
            for (int i = 0; i < listCount; i++)
            {
                bitCount += GetBitDigit(numbers[i], digit, length) ? 1 : 0;
            }
            return bitCount;
        }


        // true for 1, false for 0
        public static bool GetBitDigit(int number, int digit, int length)
        {
            number >>= (length - digit - 1);    //Shave off bits to the right of "digit"
            number %= 2;                        //Shave off bits to the left of "digit"

            //Now there is at most one bit in the number, i.e. the desired digit
            //If value of number is 0, the bit must be 0

            return number != 0; 
        }

        private static int[] GetFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\adam\git\AdventOfCode21\3\Data.txt");
            List<int> res = new List<int>();
            foreach (string line in lines)
            {
                res.Add(Convert.ToInt32(line, 2));
            }

            return res.ToArray();
        }
    }
}
