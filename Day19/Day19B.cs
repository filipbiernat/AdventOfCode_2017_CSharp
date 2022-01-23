namespace AdventOfCode2017.Day19
{
    public class Day19B : IDay
    {
        public void Run()
        {
            // Somehow, a network packet got lost and ended up here. It's trying to follow a routing diagram (your puzzle input),
            // but it's confused about where to go.
            string[] input = File.ReadAllLines(@"..\..\..\Day19\Day19.txt");
            Dictionary<Coords, char> routingDiagram = input
                .SelectMany((row, rowIndex) => row
                    .ToCharArray()
                    .Select((elem, colIndex) => new KeyValuePair<Coords, char>(new Coords(rowIndex, colIndex), elem))
                    .Where(pair => !char.IsWhiteSpace(pair.Value)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            // Its starting point is just off the top of the diagram. Lines (drawn with |, -, and +) show the path it needs to
            // take, starting by going down onto the only line connected to the top of the diagram.
            Coords currentPosition = routingDiagram.Keys.First();
            Coords previousPosition = new();

            // The packet is curious how many steps it needs to go.
            int steps = 0;

            // The little packet looks up at you, hoping you can help it find the way.
            // It needs to follow this path until it reaches the end (located somewhere within the diagram) and stop there.
            while (true)
            {
                ++steps;

                // Sometimes, the lines cross over each other; in these cases, it needs to continue going the same direction, and
                // only turn left or right when there's no other option.
                Coords positionAhead = currentPosition + currentPosition - previousPosition;
                List<Coords> potentialNextPositions = NeighbourDirections
                    .Select(neighbourDirections => currentPosition + neighbourDirections)
                    .Where(neighbourPosition => routingDiagram.ContainsKey(neighbourPosition))
                    .Where(potentialNextPosition => !previousPosition.Equals(potentialNextPosition))
                    .OrderByDescending(potentialNextPosition => potentialNextPosition.Equals(positionAhead))
                    .ToList();

                if (!potentialNextPositions.Any())
                {
                    break;
                }
                previousPosition = currentPosition;
                currentPosition = potentialNextPositions.First();
            }

            // How many steps does the packet need to go?
            int output = steps;
            Console.WriteLine("Solution: {0}.", output);
        }

        private static readonly List<Coords> NeighbourDirections = new() { new(-1, 0), new(0, -1), new(1, 0), new(0, 1) };
    }
}
