namespace AdventOfCode2017.Day1
{
    public class Day1A : IDay
    {
        public void Run()
        {
            // The captcha requires you to review a sequence of digits (your puzzle input).
            string[] input = File.ReadAllLines(@"..\..\..\Day1\Day1.txt");
            IEnumerable<int> sequenceOfDigits = input.First()
                .ToCharArray()
                .Select(elem => (int)char.GetNumericValue(elem));

            // The list is circular, so the digit after the last digit is the first digit in the list.
            // Find the sum of all digits that match the next digit in the list.
            int output = sequenceOfDigits.Skip(1)
                .Concat(sequenceOfDigits.Take(1))
                .Zip(sequenceOfDigits, (previous, current) => previous == current ? current : 0)
                .Sum();

            // What is the solution to your captcha?
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
