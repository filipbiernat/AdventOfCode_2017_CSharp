namespace AdventOfCode2017.Day15
{
    public class Day15B : IDay
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

            // The judge is getting impatient; it is now only willing to consider 5 million pairs.
            for (int i = 0; i < 5000000; ++i)
            {
                // The generators both work on the same principle. To create its next value, a generator will take the previous
                // value it produced, multiply it by a factor (generator A uses 16807; generator B uses 48271), and then keep the
                // remainder of dividing that resulting product by 2147483647. That final remainder is the value it produces next.

                // They still generate values in the same way, but now they only hand a value to the judge when it meets their
                // criteria:
                //  - Generator A looks for values that are multiples of 4.
                do
                {
                    generatorA = (generatorA * 16807) % 2147483647;
                }
                while (generatorA % 4 > 0);
                //  - Generator B looks for values that are multiples of 8.
                do
                {
                    generatorB = (generatorB * 48271) % 2147483647;
                }
                while (generatorB % 8 > 0);

                // As they do this, a judge waits for each of them to generate its next value, compares the lowest 16 bits of
                // both values, and keeps track of the number of times those parts of the values match.
                if ((generatorA & 0xFFFF) == (generatorB & 0xFFFF))
                {
                    ++count;
                }
            }

            // After 5 million pairs, but using this new generator logic, what is the judge's final count?
            int output = count;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
