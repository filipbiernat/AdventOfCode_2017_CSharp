namespace AdventOfCode2017.Day10
{
    public class Day10A : IDay
    {
        public void Run()
        {
            // You come across some programs that are trying to implement a software emulation of a hash based on knot-tying.
            // Based on the input to be hashed, the function repeatedly selects a span of string, brings the ends together, and
            // gives the span a half-twist to reverse the order of the marks within it. After doing this many times, the order
            // of the marks is used to build the resulting hash.
            string input = File.ReadAllLines(@"..\..\..\Day10\Day10.txt").First();

            // To achieve this, begin with:
            // - a list of numbers from 0 to 255
            List<int> list = Enumerable.Range(0, 256).ToList();
            // - a current position which begins at 0 (the first element in the list)
            int currentPosition = 0;
            // - a skip size (which starts at 0)
            int skipSize = 0;
            // - and a sequence of lengths (your puzzle input).
            IEnumerable<int> sequenceOfLengths = input.Split(",").Select(int.Parse);

            // Then, for each length:
            foreach (int length in sequenceOfLengths)
            {
                // Reverse the order of that length of elements in the list, starting with the element at the current position.
                Reverse(list, currentPosition, length);
                // Move the current position forward by that length plus the skip size.
                currentPosition = (currentPosition + length + skipSize) % list.Count;
                // Increase the skip size by one.
                ++skipSize;
            }

            // What is the result of multiplying the first two numbers in the list?
            int output = list.ElementAt(0) * list.ElementAt(1);
            Console.WriteLine("Solution: {0}.", output);
        }

        private static void Reverse(List<int> list, int currentPosition, int length)
        {
            for (int i = 0; i < length / 2; ++i)
            {
                int position1 = (currentPosition + i) % list.Count;
                int position2 = (currentPosition - i + length - 1) % list.Count;

                int temp = list[position1];
                list[position1] = list[position2];
                list[position2] = temp;
            }
        }
    }
}
