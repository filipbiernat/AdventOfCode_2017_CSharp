namespace AdventOfCode2017.Day7
{
    public class Day7B : IDay
    {
        private readonly Dictionary<string, List<string>> SubTowers = new();
        private readonly Dictionary<string, int> Weights = new();

        private int NewWeightOfTheProgramToBalance = int.MinValue;

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

            BalanceTheTreeAndReturnWeight(bottomProgram);

            // Given that exactly one program is the wrong weight, what would its weight need to be to balance the entire tower?
            int output = NewWeightOfTheProgramToBalance;
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

        private int BalanceTheTreeAndReturnWeight(string bottomProgram)
        {
            int sumOfWeightsOfSubTowers = 0;

            if (SubTowers.ContainsKey(bottomProgram))
            {
                // For any program holding a disc, each program standing on that disc forms a sub-tower.
                List<string> subTowers = SubTowers[bottomProgram];
                List<int> weightsOfSubTowers = subTowers
                    .Select(childProgram => BalanceTheTreeAndReturnWeight(childProgram))
                    .ToList();
                IEnumerable<int> weightsOfSubTowersGroupedAndOrderedByCount = weightsOfSubTowers
                    .GroupBy(weight => weight)
                    .OrderByDescending(group => group.Count())
                    .Select(group => group.Key);

                // Each of those sub-towers are supposed to be the same weight, or the disc itself isn't balanced.
                int desiredWeightOfTheSubTower = weightsOfSubTowersGroupedAndOrderedByCount.First();
                sumOfWeightsOfSubTowers = subTowers.Count * desiredWeightOfTheSubTower;

                // Apparently, one program has the wrong weight.
                if (weightsOfSubTowersGroupedAndOrderedByCount.Count() > 1)
                {
                    int wrongWeightOfTheSubTower = weightsOfSubTowersGroupedAndOrderedByCount.Last();
                    string programToBalance = subTowers.ElementAt(weightsOfSubTowers.IndexOf(wrongWeightOfTheSubTower));
                    int deltaWeightOfTheProgramToBalance = desiredWeightOfTheSubTower - wrongWeightOfTheSubTower;
                    NewWeightOfTheProgramToBalance = Weights[programToBalance] + deltaWeightOfTheProgramToBalance;
                }
            }

            // The weight of a tower is the sum of the weights of the programs in that tower.
            return Weights[bottomProgram] + sumOfWeightsOfSubTowers;
        }

        private static readonly string[] Separators = new string[] { "(", ") ->", ")", "," };
    }
}
