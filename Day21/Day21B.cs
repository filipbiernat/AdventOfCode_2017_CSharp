namespace AdventOfCode2017.Day21
{
    public class Day21B : IDay
    {
        public void Run()
        {
            // You find a program trying to generate some art. It uses a strange process that involves repeatedly enhancing the
            // detail of an image through a set of rules. The artist's book of enhancement rules is nearby (your puzzle input);
            // however, it seems to be missing rules.
            string[] input = File.ReadAllLines(@"..\..\..\Day21\Day21.txt");
            EnhancementRules.ProcessRules(input.ToList());

            Image image = new();
            // Then, the program repeats the following process:
            for (int step = 0; step < 18; ++step)
            {
                image.Enhance();
            }

            // How many pixels stay on after 18 iterations?
            int output = image.CountPixelsOn();
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
