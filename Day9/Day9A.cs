using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day9
{
    public class Day9A : IDay
    {
        public void Run()
        {
            // A large stream blocks your path. According to the locals, it's not safe to cross the stream at the moment because
            // it's full of garbage. You look down at the stream; rather than water, you discover that it's a stream of characters.
            // You sit for a while and record part of the stream(your puzzle input).
            // Your puzzle input represents a single, large group which itself contains many smaller ones.
            string input = File.ReadAllLines(@"..\..\..\Day9\Day9.txt").First();

            // Sometimes, instead of a group, you will find garbage. Garbage begins with < and ends with >. Between those angle
            // brackets, almost any character can appear, including { and }. Within garbage, < has no special meaning.
            string regexToRemoveGarbage = @"<(.*?(((!.).*?)*?))>";
            input = Regex.Replace(input, regexToRemoveGarbage, "");

            // Within a group, there are zero or more other things, separated by commas: either another group or garbage.
            input = input.Replace(",", "");

            // Your goal is to find the total score for all groups in your input.
            int totalScore = 0;

            // The characters represent groups - sequences that begin with { and end with }. Since groups can contain other
            // groups, a } only closes the most-recently-opened unclosed group - that is, they are nestable.
            Stack<char> stack = new();
            foreach (char character in input)
            {
                if (character == '{')
                {
                    stack.Push(character);
                    // Each group is assigned a score which is one more than the score of the group that immediately contains it.
                    // (The outermost group gets a score of 1.)
                    totalScore += stack.Count;
                }
                else if (character == '}')
                {
                    stack.Pop();
                }
            }

            // What is the total score for all groups in your input?
            int output = totalScore;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
