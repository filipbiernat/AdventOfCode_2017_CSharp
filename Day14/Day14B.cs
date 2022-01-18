namespace AdventOfCode2017.Day14
{
    public class Day14B : IDay
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

            // Now, all the defragmenter needs to know is the number of regions. A region is a group of used squares that are all
            // adjacent, not including diagonals. Every used square is in exactly one region: lone used squares form their own
            // isolated regions, while several adjacent squares all count as a single region.
            // How many regions are present given your key string?
            int output = CountRegions(grid);
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

        private static int CountRegions(List<List<bool>> grid)
        {
            HashSet<List<Coords>> regions = new();
            for (int row = 0; row < grid.Count; ++row)
            {
                for (int column = 0; column < grid[0].Count; ++column)
                {
                    if (grid[row][column] == true)
                    {
                        Coords coords = new(row, column);
                        regions.Add(FindRegion(grid, startSquare: coords, currentSquare: coords));
                    }
                }
            }
            return regions.Count;
        }

        private static List<Coords> FindRegion(List<List<bool>> grid, Coords startSquare, Coords currentSquare)
        {
            // Skip if the current square is out-of-range or empty.
            if (!currentSquare.InRange(new Coords(0, 0), new Coords(grid.Count, grid[0].Count)) ||
                !grid[currentSquare.Row][currentSquare.Column])
            {
                return new();
            }

            // Mark the visited square as false.
            grid[currentSquare.Row][currentSquare.Column] = false;

            // Search the neighbour squares and append the relative coords of the current square.
            return NeighbourCoords
                .Select(coords => coords + currentSquare)
                .Select(coords => FindRegion(grid, startSquare, currentSquare: coords))
                .Aggregate((lhs, rhs) => lhs.Concat(rhs).ToList())
                .Append(currentSquare - startSquare)
                .ToList();
        }

        private static readonly List<Coords> NeighbourCoords = new() { new(-1, 0), new(0, -1), new(1, 0), new(0, 1) };

        private class Coords
        {
            public int Row;
            public int Column;

            public Coords(int row, int column)
            {
                Row = row;
                Column = column;
            }


            public bool InRange(Coords topLeft, Coords bottomRight)
            {
                return Row >= topLeft.Row && Row < bottomRight.Row &&
                    Column >= topLeft.Column && Column < bottomRight.Column;
            }

            public static Coords operator +(Coords lhs, Coords rhs) => new(lhs.Row + rhs.Row, lhs.Column + rhs.Column);
            public static Coords operator -(Coords lhs, Coords rhs) => new(lhs.Row - rhs.Row, lhs.Column - rhs.Column);
        }
    }
}
