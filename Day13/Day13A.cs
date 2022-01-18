namespace AdventOfCode2017.Day13
{
    public class Day13A : IDay
    {
        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day13\Day13.txt");
            List<Layer> layers = input.Select(line => new Layer(line)).ToList();

            // Your plan is to hitch a ride on a packet about to move through the firewall.
            // The packet will travel along the top of each layer, and it moves at one layer per picosecond.
            // Each picosecond, the packet moves one layer forward (its first move takes it into layer 0).
            // If there is a scanner at the top of the layer as your packet enters it, you are caught.
            IEnumerable<Layer> layersWhereCaught = layers.Where(layer => layer.GetScannerPositionOnEntry() == 0);

            // The severity of the whole trip is the sum of these values.
            int severityOfTheWholeTrip = layersWhereCaught.Select(layer => layer.GetSeverity()).Sum();

            // Given the details of the firewall you've recorded, if you leave immediately, what is the severity of your whole trip?
            int output = severityOfTheWholeTrip;
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

            public int GetScannerPositionOnEntry() => ScannerPositions[Depth % CycleDuration];

            // The severity of getting caught on a layer is equal to its depth multiplied by its range.
            // (Ignore layers in which you do not get caught.)
            public int GetSeverity() => Depth * Range;
        }
    }
}
