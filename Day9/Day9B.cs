using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day9
{
    public class Day9B : IDay
    {
        public void Run()
        {
            // A large stream blocks your path. According to the locals, it's not safe to cross the stream at the moment because
            // it's full of garbage. You look down at the stream; rather than water, you discover that it's a stream of characters.
            // You sit for a while and record part of the stream(your puzzle input).
            // Your puzzle input represents a single, large group which itself contains many smaller ones.
            string input = File.ReadAllLines(@"..\..\..\Day9\Day9.txt").First();

            // Now, you're ready to remove the garbage.
            // To prove you've removed it, you need to count all of the characters within the garbage.
            // The leading and trailing < and > don't count, nor do any canceled characters or the ! doing thecanceling.
            string regexToRemoveGarbage = @"<(.*?(((!.).*?)*?))>";
            int garbageCharacters = Regex.Matches(input, regexToRemoveGarbage)
                .Select(match => Regex.Replace(match.Groups[1].Value, @"!.", "").Length)
                .Sum();

            // How many non-canceled characters are within the garbage in your puzzle input?
            int output = garbageCharacters;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
