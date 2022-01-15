namespace AdventOfCode2017.Day5
{
    public class Day5A : IDay
    {
        public void Run()
        {
            // The message includes a list of the offsets for each jump.
            string[] input = File.ReadAllLines(@"..\..\..\Day5\Day5.txt");
            List<int> jumpOffsets = input.Select(int.Parse).ToList();

            // Start at the first instruction in the list.
            int currentInstruction = 0;
            int step = 0;

            // The goal is to follow the jumps until one leads outside the list.
            for (; 0 <= currentInstruction && currentInstruction < jumpOffsets.Count; ++step)
            {
                int instruction = currentInstruction;
                // Jumps are relative: -1 moves to the previous instruction, and 2 skips the next one.
                currentInstruction += jumpOffsets[instruction];
                // After each jump, the offset of that instruction increases by 1.
                ++jumpOffsets[instruction];
            }

            // How many steps does it take to reach the exit?
            int output = step;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
