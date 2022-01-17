namespace AdventOfCode2017.Day12
{
    public class Day12B : IDay
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
            // There are more programs than just the ones in the group containing program ID 0.
            // The rest of them have no way of reaching that group, and still might have no way of reaching each other.
            List<int> unvisitedPrograms = pipes.Values.Aggregate((lhs, rhs) => lhs.Concat(rhs).ToList())
                .Concat(pipes.Keys).Distinct().ToList();

            // A group is a collection of programs that can all communicate via pipes either directly or indirectly.
            // The programs you identified just a moment ago are all part of the same group.
            // Now, they would like you to determine the total number of groups.
            int totalNumberOfGroups;
            for (totalNumberOfGroups = 0; unvisitedPrograms.Any(); ++totalNumberOfGroups)
            {
                int parentProgram = unvisitedPrograms.First();
                DiscoverPrograms(pipes, parentProgram, unvisitedPrograms);
            }

            // How many groups are there in total?
            int output = totalNumberOfGroups;
            Console.WriteLine("Solution: {0}.", output);
        }

        private static KeyValuePair<int, List<int>> ReadLine(string line)
        {
            IEnumerable<int> entries = line.Split(new string[] { "<->", "," }, StringSplitOptions.TrimEntries).Select(int.Parse);
            return KeyValuePair.Create(entries.First(), entries.Skip(1).ToList());
        }

        private static void DiscoverPrograms(Dictionary<int, List<int>> pipes, int parentProgram, List<int> unvisitedPrograms)
        {
            if (unvisitedPrograms.Contains(parentProgram))
            {
                unvisitedPrograms.Remove(parentProgram);
                pipes[parentProgram].ForEach(nextProgram => DiscoverPrograms(pipes, nextProgram, unvisitedPrograms));
            }
        }
    }
}
