namespace AdventOfCode2017.Day14
{
    public class Day14A : IDay
    {
        public void Run()
        {
            // The hash inputs are a key string (your puzzle input), a dash, and a number from 0 to 127 corresponding to the row.
            string input = File.ReadAllLines(@"..\..\..\Day14\Day14.txt").First();
            IEnumerable<string> hashInputs = Enumerable.Range(0, 128).Select(number => input + "-" + number);

            // A total of 128 knot hashes are calculated, each corresponding to a single row in the grid; each hash contains 128
            // bits which correspond to individual grid squares. Each bit of a hash indicates whether that square is free (0) or
            // used (1).
            IEnumerable<string> knotHashes = hashInputs.Select(hashInput => KnotHash.Generate(hashInput));

            // The output of a knot hash is traditionally represented by 32 hexadecimal digits; each of these digits correspond
            // to 4 bits, for a total of 4 * 32 = 128 bits. To convert to bits, turn each hexadecimal digit to its equivalent
            // binary value, high-bit first: 0 becomes 0000, 1 becomes 0001, e becomes 1110, f becomes 1111, and so on.
            List<List<bool>> grid = knotHashes.Select(ConvertHexStringToListOfBool).ToList();

            // Given your actual key string, how many squares are used?
            int output = grid.Select(row => row.Count(bit => bit == true)).Sum();
            Console.WriteLine("Solution: {0}.", output);
        }

        private static List<bool> ConvertHexStringToListOfBool(string hexString)
        {
            return Convert.FromHexString(hexString)
                .Select(decimalNumber => Convert.ToString(decimalNumber, 2)
                    .PadLeft(8, '0')
                    .Select(binaryCharacter => binaryCharacter.Equals('1')))
                .Aggregate((lhs, rhs) => lhs.Concat(rhs))
                .ToList();
        }
    }
}
