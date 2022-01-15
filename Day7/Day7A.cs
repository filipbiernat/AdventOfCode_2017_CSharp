namespace AdventOfCode2017.Day7
{
    public class Day7A : IDay
    {
        private readonly Dictionary<string, List<string>> SubTowers = new();
        private readonly Dictionary<string, int> Weights = new();

        public void Run()
        {
            // You offer to help, but first you need to understand the structure of these towers. You ask each program to yell
            // out their name, their weight, and (if they're holding a disc) the names of the programs immediately above them
            // balancing on that disc. You write this information down (your puzzle input)
            string[] input = File.ReadAllLines(@"..\..\..\Day7\Day7.txt");
            input.ToList().ForEach(ParseLine);

            // Before you're ready to help them, you need to make sure your information is correct.
            IEnumerable<string> programs = Weights.Select(elem => elem.Key);
            IEnumerable<string> childPrograms = SubTowers.SelectMany(elem => elem.Value);
            string bottomProgram = programs.Except(childPrograms).First();

            //  What is the name of the bottom program?
            string output = bottomProgram;
            Console.WriteLine("Solution: {0}.", output);
        }

        private void ParseLine(string line)
        {
            string[] entries = line.Split(Separators, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string programName = entries.ElementAt(0);
            Weights.Add(programName, int.Parse(entries.ElementAt(1)));
            for (int i = 2; i < entries.Length; ++i)
            {
                if (!SubTowers.ContainsKey(programName))
                {
                    SubTowers.Add(programName, new());
                }
                SubTowers[programName].Add(entries.ElementAt(i));
            }
        }

        private static readonly string[] Separators = new string[] { "(", ") ->", ")", "," };
    }
}
