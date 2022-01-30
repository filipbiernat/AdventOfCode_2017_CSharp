namespace AdventOfCode2017.Day21
{
    public class Image
    {
        private List<string> Pixels;

        public Image()
        {
            // The program always begins with this pattern:
            string pattern = ".#.\n..#\n###";

            // The image consists of a two-dimensional square grid of pixels that are either on (#) or off (.).
            Pixels = pattern.Split("\n").ToList();
        }

        public int CountPixelsOn() => Pixels.Sum(row => row.Count(pixel => pixel == '#'));

        public void Enhance()
        {
            // Because each square of pixels is replaced by a larger one, the image gains pixels and so its size increases.
            if (Pixels.Count % 2 == 0)
            {
                // If the size is evenly divisible by 2, break the pixels up into 2x2 squares, and convert each 2x2 square into
                // a 3x3 square by following the corresponding enhancement rule.
                Enhance(sizeOfOldSquare: 2);
            }
            else if (Pixels.Count % 3 == 0)
            {
                // Otherwise, the size is evenly divisible by 3; break the pixels up into 3x3 squares, and convert each 3x3
                // square into a 4x4 square by following the corresponding enhancement rule.
                Enhance(sizeOfOldSquare: 3);
            }
        }

        private void Enhance(int sizeOfOldSquare)
        {
            int numberOfSquares = Pixels.Count / sizeOfOldSquare;
            int sizeOfNewSquare = sizeOfOldSquare + 1;
            List<string> oldPixels = Pixels;
            Pixels = new(new string[numberOfSquares * sizeOfNewSquare]);

            // Break the pixels up into squares.
            for (int rowOfSquares = 0; rowOfSquares < numberOfSquares; ++rowOfSquares)
            {
                for (int columnOfSquares = 0; columnOfSquares < numberOfSquares; ++columnOfSquares)
                {
                    // Convert each old square into a new square by following the corresponding enhancement rule.
                    string[] oldSquare = new string[sizeOfOldSquare];
                    for (int rowInsideTheSquare = 0; rowInsideTheSquare < sizeOfOldSquare; ++rowInsideTheSquare)
                    {
                        int row = sizeOfOldSquare * rowOfSquares + rowInsideTheSquare;
                        oldSquare[rowInsideTheSquare] = oldPixels[row].Substring(sizeOfOldSquare * columnOfSquares, sizeOfOldSquare);
                    }

                    string[] newSquare = EnhancementRules.Enhance(oldSquare);
                    for (int rowInsideTheSquare = 0; rowInsideTheSquare < sizeOfNewSquare; ++rowInsideTheSquare)
                    {
                        int row = sizeOfNewSquare * rowOfSquares + rowInsideTheSquare;
                        Pixels[row] += newSquare[rowInsideTheSquare];
                    }
                }
            }
        }
    }
}
