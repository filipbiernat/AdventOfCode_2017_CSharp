namespace AdventOfCode2017.Day11
{
    public class Day11B : IDay
    {
        // Crossing the bridge, you've barely reached the other side of the stream when a program comes up to you, clearly in
        // distress. "It's my child process," she says, "he's gotten lost in an infinite grid!" Fortunately for her, you have
        // plenty of experience with infinite grids. Unfortunately for you, it's a hex grid.
        private int Q = 0, R = 0, S = 0;

        public void Run()
        {
            // You have the path the child process took.
            string input = File.ReadAllLines(@"..\..\..\Day11\Day11.txt").First();
            List<int> distances = input.Split(",").Select(CalculateDistanceAfterStep).ToList();

            // How many steps away is the furthest he ever got from his starting position?
            int output = distances.Max();
            Console.WriteLine("Solution: {0}.", output);
        }

        private int CalculateDistanceAfterStep(string direction)
        {
            MakeStep(direction);
            return CalculateDistance();
        }

        // Cube coordinates: https://www.redblobgames.com/grids/hexagons/#coordinates
        private void MakeStep(string direction)
        {
            // The hexagons ("hexes") in this grid are aligned such that adjacent hexes can be found to the north, northeast,
            // southeast, south, southwest, and northwest. A "step" means to move from the hex you are in to any adjacent hex.
            switch (direction)
            {
                case "ne": ++Q; --R; break;
                case "se": ++Q; --S; break;
                case "s":  ++R; --S; break;
                case "sw": ++R; --Q; break;
                case "nw": ++S; --Q; break;
                case "n":  ++S; --R; break;
            }
        }

        // Distance: https://www.redblobgames.com/grids/hexagons/#distances
        private int CalculateDistance()
        {
            return (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
        }
    }
}
