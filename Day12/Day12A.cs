namespace AdventOfCode2017.Day12
{
    public class Day12A : IDay
    {
        public void Run()
        {
            // You walk through the village and record the ID of each program and the IDs with which it can communicate directly.
            string[] input = File.ReadAllLines(@"..\..\..\Day12\Day12.txt");

            // Walking along the memory banks of the stream, you find a small village that is experiencing a little confusion:
            // some programs can't communicate with each other. Programs in this village communicate using a fixed system of pipes.
            // Messages are passed between programs using these pipes, but most programs aren't connected to each other directly.
            // Instead, programs pass messages between each other until the message reaches the intended recipient.
            Dictionary<int, List<int>> pipes = input.Select(ReadLine).ToDictionary(pipe => pipe.Key, pair => pair.Value);

            // Each program has one or more programs with which it can communicate, and these pipes are bidirectional.
            // You need to figure out how many programs are in the group that contains program ID 0.
            int parentProgram = 0;
            List<int> visitedPrograms = new();
            int programsInTheGroupThatContainsProgramId0 = CountPrograms(pipes, parentProgram, visitedPrograms);

            // How many programs are in the group that contains program ID 0?
            int output = programsInTheGroupThatContainsProgramId0;
            Console.WriteLine("Solution: {0}.", output);
        }

        private static KeyValuePair<int, List<int>> ReadLine(string line)
        {
            IEnumerable<int> entries = line.Split(new string[] { "<->", "," }, StringSplitOptions.TrimEntries).Select(int.Parse);
            return KeyValuePair.Create(entries.First(), entries.Skip(1).ToList());
        }

        private static int CountPrograms(Dictionary<int, List<int>> pipes, int parentProgram, List<int> visitedPrograms)
        {
            if (visitedPrograms.Contains(parentProgram))
            {
                return 0;
            }
            else
            {
                visitedPrograms.Add(parentProgram);
                return 1 + pipes[parentProgram].Select(nextProgram => CountPrograms(pipes, nextProgram, visitedPrograms)).Sum();
            }
        }
    }
}
