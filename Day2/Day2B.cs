namespace AdventOfCode2017.Day2
{
    public class Day2B : IDay
    {
        public void Run()
        {
            // The spreadsheet consists of rows of apparently-random numbers.
            string[] input = File.ReadAllLines(@"..\..\..\Day2\Day2.txt");

            // They would like you to find those numbers on each line, divide them, and add up each line's result.
            int output = input.Select(CalculateForRow).Sum();

            // What is the sum of each row's result in your puzzle input?
            Console.WriteLine("Solution: {0}.", output);
        }

        private static int CalculateForRow(string row)
        {
            // It sounds like the goal is to find the only two numbers in each row where one evenly divides the other -
            // that is, where the result of the division operation is a whole number.
            IEnumerable<int> values = row.Split().Select(int.Parse);
            return values.SelectMany(item1 => values.Select(item2 => Tuple.Create(item1, item2)))
                .Where(pair => pair.Item1 > pair.Item2)
                .Where(pair => pair.Item1 % pair.Item2 == 0)
                .Select(pair => pair.Item1 / pair.Item2)
                .First();
        }
    }
}
