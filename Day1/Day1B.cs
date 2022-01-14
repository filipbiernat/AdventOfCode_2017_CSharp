namespace AdventOfCode2017.Day1
{
    public class Day1B : IDay
    {
        public void Run()
        {
            // The captcha requires you to review a sequence of digits (your puzzle input).
            string[] input = File.ReadAllLines(@"..\..\..\Day1\Day1.txt");
            IEnumerable<int> sequenceOfDigits = input.First()
                .ToCharArray()
                .Select(elem => (int)char.GetNumericValue(elem));

            // Now, instead of considering the next digit, it wants you to consider the digit halfway around the circular list.
            // That is, if your list contains 10 items, only include a digit in your sum if the digit 10/2 = 5 steps forward
            // matches it. Fortunately, your list has an even number of elements.
            int output = sequenceOfDigits.Skip(sequenceOfDigits.Count() / 2)
                .Concat(sequenceOfDigits.Take(sequenceOfDigits.Count() / 2))
                .Zip(sequenceOfDigits, (halfwayAround, current) => halfwayAround == current ? current : 0)
                .Sum();

            // What is the solution to your captcha?
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
