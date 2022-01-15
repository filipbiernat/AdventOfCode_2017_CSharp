namespace AdventOfCode2017.Day3
{
    public class Day3B : IDay
    {
        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day3\Day3.txt");
            int inputNumber = int.Parse(input.First());

            // As a stress test on the system, the programs here clear the grid and then store the value 1 in square 1.
            // Then, in the same allocation order as shown above, they store the sum of the values in all adjacent squares,
            // including diagonals.
            SquareOnTheGrid squareOnTheGrid = new(targetNumber: inputNumber);
            for (int distanceToTheCentre = 0; ; ++distanceToTheCentre)
            {
                if (squareOnTheGrid.MoveUp(2 * distanceToTheCentre - 1)) break;
                if (squareOnTheGrid.MoveLeft(2 * distanceToTheCentre)) break;
                if (squareOnTheGrid.MoveDown(2 * distanceToTheCentre)) break;
                if (squareOnTheGrid.MoveRight(2 * distanceToTheCentre + 1)) break;
            }

            // What is the first value written that is larger than your puzzle input?
            int output = squareOnTheGrid.GetNumber();
            Console.WriteLine("Solution: {0}.", output);
        }

        private class SquareOnTheGrid
        {
            private readonly Dictionary<Tuple<int, int>, int> NumbersOnTheGrid = new();
            private readonly int TargetNumber;
            private int Number = 1;
            private int Row = 0;
            private int Column = 0;

            public SquareOnTheGrid(int targetNumber)
            {
                TargetNumber = targetNumber;
                RegisterNumberOnTheGrid();
            }

            public int GetNumber() => Number;

            public bool MoveRight(int steps) => Move(() => ++Column, steps);
            public bool MoveUp(int steps) => Move(() => --Row, steps);
            public bool MoveLeft(int steps) => Move(() => --Column, steps);
            public bool MoveDown(int steps) => Move(() => ++Row, steps);

            private bool Move(Action updatePosition, int steps)
            {
                for (int step = 0; step < steps; ++step)
                {
                    updatePosition();
                    UpdateNumber();
                    RegisterNumberOnTheGrid();
                    if (Number > TargetNumber)
                    {
                        return true;
                    }
                }
                return false;
            }

            private void UpdateNumber()
            {
                Number = NeighbourPositionsOnTheGrid
                    .Select(neighbourPosition => Tuple.Create(Row + neighbourPosition.Item1, Column + neighbourPosition.Item2))
                    .Select(position => NumbersOnTheGrid.ContainsKey(position) ? NumbersOnTheGrid[position] : 0)
                    .Sum();
            }

            private void RegisterNumberOnTheGrid() => NumbersOnTheGrid.Add(Tuple.Create(Row, Column), Number);

            private static readonly List<Tuple<int, int>> NeighbourPositionsOnTheGrid = new()
            {
                Tuple.Create(-1, -1),
                Tuple.Create(-1, 0),
                Tuple.Create(-1, 1),
                Tuple.Create(0, -1),
                Tuple.Create(0, 1),
                Tuple.Create(1, -1),
                Tuple.Create(1, 0),
                Tuple.Create(1, 1)
            };
        }
    }
}
