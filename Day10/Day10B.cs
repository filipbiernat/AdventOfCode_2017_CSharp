using System.Text;

namespace AdventOfCode2017.Day10
{
    public class Day10B : IDay
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
            // First, from now on, your input should be taken not as a list of numbers, but as a string of bytes instead. Unless
            // otherwise specified, convert characters to bytes using their ASCII codes. This will allow you to handle arbitrary
            // ASCII strings, and it also ensures that your input lengths are never larger than 255.
            IEnumerable<int> sequenceOfLengths = Encoding.ASCII.GetBytes(input).Select(character => (int)character);

            // Once you have determined the sequence of lengths to use, add the following lengths to the end of the sequence:
            List<int> standardLengthSuffixValues = new() { 17, 31, 73, 47, 23 };
            sequenceOfLengths = sequenceOfLengths.Concat(standardLengthSuffixValues);

            // Second, instead of merely running one round like you did above, run a total of 64 rounds, using the same length
            // sequence in each round. The current position and skip size should be preserved between rounds.
            for (int round = 0; round < 64; ++round)
            {
                foreach (int length in sequenceOfLengths)
                {
                    // Reverse the order of that length of elements in the list, starting with the element at the current position.
                    Reverse(list, currentPosition, length);
                    // Move the current position forward by that length plus the skip size.
                    currentPosition = (currentPosition + length + skipSize) % list.Count;
                    // Increase the skip size by one.
                    ++skipSize;
                }
            }

            // Once the rounds are complete, you will be left with the numbers from 0 to 255 in some order, called the sparse hash.
            // Your next task is to reduce these to a list of only 16 numbers called the dense hash.
            // To do this, use numeric bitwise XOR to combine each consecutive block of 16 numbers in the sparse hash
            // (there are 16 such blocks in a list of 256 numbers).
            IEnumerable<int> denseHash = Enumerable.Range(0, 16)
                .Select(blockIndex => list.GetRange(16 * blockIndex, 16).Aggregate((lhs, rhs) => lhs ^ rhs));

            // Finally, the standard way to represent a Knot Hash is as a single hexadecimal string; the final output is the dense
            // hash in hexadecimal notation. Because each number in your dense hash will be between 0 and 255 (inclusive), always
            // represent each number as two hexadecimal digits (including a leading zero as necessary).
            string output = string.Join("", denseHash.Select(number => number.ToString("x2")));

            // What is the Knot Hash of your puzzle input?
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
