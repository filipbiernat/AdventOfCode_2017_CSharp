namespace AdventOfCode2017.Day17
{
    public class Day17B : IDay
    {
        public void Run()
        {
            string input = File.ReadAllLines(@"..\..\..\Day17\Day17.txt").First();
            int numberOfSteps = int.Parse(input);

            // This spinlock's algorithm is simple but efficient, quickly consuming everything in its path. It starts with a
            // circular buffer containing only the value 0, which it marks as the current position.
            int currentPosition = 0;

            // The good news is that you have improved calculations for how to stop the spinlock. They indicate that you actually
            // need to identify the value after 0 in the current state of the circular buffer.
            int valueAfter0 = int.MaxValue;

            // The bad news is that while you were determining this, the spinlock has just finished inserting its fifty millionth
            // value (50000000).
            for (int newValue = 1; newValue <= 50000000; ++newValue)
            {
                // It then steps forward through the circular buffer some number of steps (your puzzle input)
                currentPosition = (currentPosition + numberOfSteps) % newValue;
                // before inserting the (...) new value (...) after the value it stopped on.
                if (currentPosition == 0)
                {
                    valueAfter0 = newValue;
                }
                // The inserted value becomes the current position.
                ++currentPosition;
            }

            // What is the value after 0 the moment 50000000 is inserted?
            int output = valueAfter0;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
