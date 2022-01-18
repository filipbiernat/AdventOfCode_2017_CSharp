namespace AdventOfCode2017.Day13
{
    public class Day13B : IDay
    {
        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day13\Day13.txt");
            List<Layer> layers = input.Select(line => new Layer(line)).ToList();

            // Now, you need to pass through the firewall without being caught - easier said than done. You can't control the
            // speed of the packet, but you can delay it any number of picoseconds.
            int delay = 0;

            // For each picosecond you delay the packet before beginning your trip, all security scanners move one step.
            // You're not in the firewall during this time; you don't enter layer 0 until you stop delaying the packet.
            for (delay = 0; layers.Any(layer => layer.GetScannerPositionOnEntry(delay) == 0); ++delay);

            // What is the fewest number of picoseconds that you need to delay the packet to pass through the firewall without
            // being caught?
            int output = delay;
            Console.WriteLine("Solution: {0}.", output);
        }

        private class Layer
        {
            private readonly int Depth;
            private readonly int Range;
            private readonly int CycleDuration;
            private readonly List<int> ScannerPositions;

            public Layer(string input)
            {
                // By studying the firewall briefly, you are able to record (in your puzzle input) the depth of each layer and the
                // range of the scanning area for the scanner within it, written as depth: range.
                IEnumerable<int> entries = input.Split(": ").Select(int.Parse);
                Depth = entries.ElementAt(0);
                Range = entries.ElementAt(1);
                CycleDuration = 2 * (Range - 1);

                // Each security scanner starts at the top and moves down until it reaches the bottom, then moves up until it
                // reaches the top, and repeats.
                ScannerPositions = Enumerable.Range(0, CycleDuration)
                    .Select(picosecond => picosecond < Range ? picosecond : Range - 2 - picosecond % Range)
                    .ToList();
            }

            public int GetScannerPositionOnEntry(int delay) => ScannerPositions[(Depth + delay) % CycleDuration];
        }
    }
}
