namespace AdventOfCode2017.Day6
{
    public class Day6B : IDay
    {
        private readonly List<List<int>> MemoryBanksSeenBefore = new();

        public void Run()
        {
            // A debugger program here is having an issue: it is trying to repair a memory reallocation routine,
            // but it keeps getting stuck in an infinite loop.
            // In this area, there are sixteen memory banks; each memory bank can hold any number of blocks. 
            string[] input = File.ReadAllLines(@"..\..\..\Day6\Day6.txt");
            List<int> memoryBanks = input.First().Split().Select(int.Parse).ToList();

            RunReallocationRoutine(memoryBanks);

            // Out of curiosity, the debugger would also like to know the size of the loop: starting from a state that has
            // already been seen, how many block redistribution cycles must be performed before that same state is seen again?
            MemoryBanksSeenBefore.Clear();
            RunReallocationRoutine(memoryBanks);

            // How many cycles are in the infinite loop that arises from the configuration in your puzzle input?
            int output = MemoryBanksSeenBefore.Count;
            Console.WriteLine("Solution: {0}.", output);
        }

        private void RunReallocationRoutine(List<int> memoryBanks)
        {
            // The reallocation routine operates in cycles.
            do
            {
                MemoryBanksSeenBefore.Add(memoryBanks.ToList());

                // In each cycle, it finds the memory bank with the most blocks (ties won by the lowest-numbered memory bank)
                int blocksToRedistribute = memoryBanks.Max();
                int bank = memoryBanks.IndexOf(blocksToRedistribute);

                // and redistributes those blocks among the banks. To do this, it removes all of the blocks from the selected bank,
                memoryBanks[bank] = 0;

                // then moves to the next (by index) memory bank and inserts one of the blocks. It continues doing this until
                // it runs out of blocks; if it reaches the last memory bank, it wraps around to the first one.
                for (int block = 0; block < blocksToRedistribute; ++block)
                {
                    bank = (bank + 1) % memoryBanks.Count;
                    ++memoryBanks[bank];
                }
            }
            // The debugger would like to know how many redistributions can be done before a blocks-in-banks configuration is
            // produced that has been seen before.
            while (!SeenBefore(memoryBanks));
        }

        private bool SeenBefore(List<int> memoryBanksToCheck) => MemoryBanksSeenBefore
            .Select(memoryBanksSeenBefore => Enumerable.SequenceEqual(memoryBanksSeenBefore, memoryBanksToCheck))
            .Any(seenBefore => seenBefore);
    }
}
