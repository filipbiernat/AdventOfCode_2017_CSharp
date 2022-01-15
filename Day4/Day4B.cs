namespace AdventOfCode2017.Day4
{
    public class Day4B : IDay
    {
        public void Run()
        {
            // The system's full passphrase list is available as your puzzle input.
            string[] input = File.ReadAllLines(@"..\..\..\Day4\Day4.txt");

            // Under this new system policy, how many passphrases are valid?
            int output = input.Where(IsValidPassPhrase).Count();
            Console.WriteLine("Solution: {0}.", output);
        }

        private bool IsValidPassPhrase(string passphrase)
        {
            // To ensure security, a valid passphrase must contain no duplicate words.
            IEnumerable<string> words = passphrase.Split();
            // For added security, yet another system policy has been put in place.
            // Now, a valid passphrase must contain no two words that are anagrams of each other - that is, a passphrase is
            // invalid if any word's letters can be rearranged to form any other word in the passphrase.
            words = words.Select(word => string.Concat(word.OrderBy(letter => letter)));
            return words.Count() == words.Distinct().Count();
        }
    }
}
