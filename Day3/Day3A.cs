namespace AdventOfCode2017.Day3
{
    public class Day3A : IDay
    {
        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day3\Day3.txt");
            int inputNumber = int.Parse(input.First());

            // You come across an experimental new kind of memory stored on an infinite two-dimensional grid.
            // Each square on the grid is allocated in a spiral pattern starting at a location marked 1 and then counting up
            // while spiraling outward.
            SquareOnTheGrid squareOnTheGrid = new(targetNumber: inputNumber);
            for (int distanceToTheCentre = 0;; ++distanceToTheCentre)
            {
                if (squareOnTheGrid.MoveUp(2 * distanceToTheCentre - 1)) break;
                if (squareOnTheGrid.MoveLeft(2 * distanceToTheCentre)) break;
                if (squareOnTheGrid.MoveDown(2 * distanceToTheCentre)) break;
                if (squareOnTheGrid.MoveRight(2 * distanceToTheCentre + 1)) break;
            }

            // How many steps are required to carry the data from the square identified in your puzzle input all the way to
            // the access port?
            int output = squareOnTheGrid.GetDistanceToTheAccessPort();
            Console.WriteLine("Solution: {0}.", output);
        }

        private class SquareOnTheGrid
        {
            private readonly int TargetNumber;
            private int Number = 1;
            private int Row = 0;
            private int Column = 0;

            public SquareOnTheGrid(int targetNumber)
            {
                TargetNumber = targetNumber;
            }

            public int GetDistanceToTheAccessPort() => Math.Abs(Row) + Math.Abs(Column);

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
                    if (Number == TargetNumber)
                    {
                        return true;
                    }
                }
                return false;
            }

            private void UpdateNumber() => ++Number;
        }
    }
}
