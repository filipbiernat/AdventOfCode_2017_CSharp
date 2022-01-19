namespace AdventOfCode2017.Day16
{
    public class Day16A : IDay
    {
        private List<char> Programs = new();

        public void Run()
        {
            // You watch the dance for a while and record their dance moves (your puzzle input).
            string input = File.ReadAllLines(@"..\..\..\Day16\Day16.txt").First();
            List<string> danceMoves = input.Split(",").ToList();

            // There are sixteen programs in total, named a through p. They start by standing in a line: a stands in position 0,
            // b stands in position 1, and so on until p, which stands in position 15.
            Programs = Enumerable.Range('a', 16).Select(program => (char)program).ToList();

            danceMoves.ForEach(MakeADanceMove);

            // In what order are the programs standing after their dance?
            string output = string.Join("", Programs);
            Console.WriteLine("Solution: {0}.", output);
        }

        private void MakeADanceMove(string move)
        {
            string[] parameters = move[1..].Split("/");
            // The programs' dance consists of a sequence of dance moves:
            switch (move.First())
            {
                // - Spin, written sX,
                case 's': Spin(parameters.First()); break;
                // - Exchange, written xA/B,
                case 'x': Exchange(parameters.First(), parameters.Last()); break;
                // - Partner, written pA/B.
                case 'p': Partner(parameters.First(), parameters.Last()); break;
            }
        }

        private void Spin(string parameterX)
        {
            // Spin, written sX, makes X programs move from the end to the front, but maintain their order otherwise.
            int count = int.Parse(parameterX);
            Programs = Programs.Skip(Programs.Count - count)
                .Concat(Programs.Take(Programs.Count - count))
                .ToList();
        }

        private void Exchange(string parameterA, string parameterB)
        {
            // Exchange, written xA/B, makes the programs at positions A and B swap places.
            int positionA = int.Parse(parameterA);
            int positionB = int.Parse(parameterB);
            SwapPlaces(positionA, positionB);
        }

        private void Partner(string parameterA, string parameterB)
        {
            // Partner, written pA/B, makes the programs named A and B swap places.
            int positionA = Programs.FindIndex(program => program == Convert.ToChar(parameterA));
            int positionB = Programs.FindIndex(program => program == Convert.ToChar(parameterB));
            SwapPlaces(positionA, positionB);
        }

        private void SwapPlaces(int positionA, int positionB)
        {
            char temp = Programs[positionA];
            Programs[positionA] = Programs[positionB];
            Programs[positionB] = temp;
        }
    }
}
