namespace AdventOfCode2017.Day4
{
    public class Day4A : IDay
    {
        public void Run()
        {
            // The system's full passphrase list is available as your puzzle input.
            string[] input = File.ReadAllLines(@"..\..\..\Day4\Day4.txt");

            // How many passphrases are valid?
            int output = input.Where(IsValidPassPhrase).Count();
            Console.WriteLine("Solution: {0}.", output);
        }

        private bool IsValidPassPhrase(string passphrase)
        {
            // To ensure security, a valid passphrase must contain no duplicate words.
            IEnumerable<string> words = passphrase.Split();
            return words.Count() == words.Distinct().Count();
        }
    }
}
