namespace AdventOfCode2017.Day15
{
    public class Day15A : IDay
    {
        public void Run()
        {
            // Here, you encounter a pair of dueling generators. The generators, called generator A and generator B, are trying
            // to agree on a sequence of numbers. However, one of them is malfunctioning, and so the sequences don't always match.
            // To calculate each generator's first value, it instead uses a specific starting value as its "previous value"
            // (as listed in your puzzle input).
            string[] input = File.ReadAllLines(@"..\..\..\Day15\Day15.txt");
            uint generatorA = uint.Parse(input.ElementAt(0).Split().Last());
            uint generatorB = uint.Parse(input.ElementAt(1).Split().Last());

            int count = 0;

            // To get a significant sample, the judge would like to consider 40 million pairs.
            for (int i = 0; i < 40000000; ++i)
            {
                // The generators both work on the same principle. To create its next value, a generator will take the previous
                // value it produced, multiply it by a factor (generator A uses 16807; generator B uses 48271), and then keep the
                // remainder of dividing that resulting product by 2147483647. That final remainder is the value it produces next.
                generatorA = (generatorA * 16807) % 2147483647;
                generatorB = (generatorB * 48271) % 2147483647;

                // As they do this, a judge waits for each of them to generate its next value, compares the lowest 16 bits of
                // both values, and keeps track of the number of times those parts of the values match.
                if ((generatorA & 0xFFFF) == (generatorB & 0xFFFF))
                {
                    ++count;
                }
            }

            // After 40 million pairs, what is the judge's final count?
            int output = count;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
