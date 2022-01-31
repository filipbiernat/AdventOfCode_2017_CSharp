namespace AdventOfCode2017.Day22
{
    public class Day22A : IDay
    {
        private enum Direction
        {
            Up,
            Left,
            Down,
            Right
        }

        // Diagnostics indicate that the local grid computing cluster has been contaminated with the Sporifica Virus. The grid
        // computing cluster is a seemingly-infinite two-dimensional grid of compute nodes. Each node is either clean or
        // infected by the virus.
        private enum Node
        {
            Clean,
            Infected
        }

        public void Run()
        {
            // Diagnostics have also provided a map of the node infection status (your puzzle input).
            string[] input = File.ReadAllLines(@"..\..\..\Day22\Day22.txt");

            Dictionary<Coords, Node> gridComputingCluster = input
                .SelectMany((row, rowIndex) => row
                    .ToCharArray()
                    .Select((elem, colIndex) => new KeyValuePair<Coords, Node>(new Coords(rowIndex, colIndex), DecodedNode[elem])))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            // To prevent overloading the nodes (which would render them useless to the virus) or detection by system
            // administrators, exactly one virus carrier moves through the network, infecting or cleaning nodes as it moves.
            // The virus carrier is always located on a single node in the network (the current node) and keeps track of the
            // direction it is facing. The virus carrier begins in the middle of the map facing up.
            Coords virusPosition = new(input.Length / 2, input.First().Length / 2);
            Direction virusDirection = Direction.Up;

            int infectionCount = 0;

            // To avoid detection, the virus carrier works in bursts; in each burst, it wakes up, does some work, and goes back
            // to sleep.
            for (int burst = 0; burst < 10000; ++burst)
            {
                // This map only shows the center of the grid; there are many more nodes beyond those shown, but none of them are
                // currently infected.
                Node currentNode = gridComputingCluster.ContainsKey(virusPosition) ? gridComputingCluster[virusPosition] : Node.Clean;

                if (currentNode == Node.Infected)
                {
                    // If the current node is infected, it turns to its right
                    virusDirection = TurnRight(virusDirection);
                    // (...) Otherwise, it becomes cleaned.
                    gridComputingCluster[virusPosition] = Node.Clean;
                }
                else if (currentNode == Node.Clean)
                {
                    // (...) Otherwise, it turns to its left.
                    virusDirection = TurnLeft(virusDirection);
                    // If the current node is clean, it becomes infected.
                    gridComputingCluster[virusPosition] = Node.Infected;
                    ++infectionCount;
                }

                // The virus carrier moves forward one node in the direction it is facing.
                virusPosition += StepForward[virusDirection];
            }

            // Given your actual map, after 10000 bursts of activity, how many bursts cause a node to become infected?
            int output = infectionCount;
            Console.WriteLine("Solution: {0}.", output);
        }

        private static Direction TurnLeft(Direction direction) => direction switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => throw new ArgumentOutOfRangeException($"Method: TurnLeft({direction})")
        };

        private static Direction TurnRight(Direction direction) => direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Left => Direction.Up,
            Direction.Down => Direction.Left,
            Direction.Right => Direction.Down,
            _ => throw new ArgumentOutOfRangeException($"Method: TurnRight({direction})")
        };

        private static readonly Dictionary<Direction, Coords> StepForward = new()
        {
            { Direction.Up, new Coords(-1, 0) },
            { Direction.Left, new Coords(0, -1) },
            { Direction.Down, new Coords(1, 0) },
            { Direction.Right, new Coords(0, 1) },
        };

        // Clean nodes are shown as .; infected nodes are shown as #.
        private static readonly Dictionary<char, Node> DecodedNode = new()
        {
            { '.', Node.Clean },
            { '#', Node.Infected }
        };
    }
}
