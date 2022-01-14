namespace AdventOfCode2017.Day2
{
    public class Day2A : IDay
    {
        public void Run()
        {
            // The spreadsheet consists of rows of apparently-random numbers.
            string[] input = File.ReadAllLines(@"..\..\..\Day2\Day2.txt");

            // To make sure the recovery process is on the right track, they need you to calculate the spreadsheet's checksum.
            // the checksum is the sum of all of these differences.
            int output = input.Select(CalculateForRow).Sum();

            // What is the checksum for the spreadsheet in your puzzle input?
            Console.WriteLine("Solution: {0}.", output);
        }

        private static int CalculateForRow(string row)
        {
            // For each row, determine the difference between the largest value and the smallest value.
            IEnumerable<int> values = row.Split().Select(int.Parse);
            return Math.Abs(values.Max() - values.Min());
        }
    }
}
