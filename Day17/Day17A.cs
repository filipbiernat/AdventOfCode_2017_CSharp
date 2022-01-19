namespace AdventOfCode2017.Day17
{
    public class Day17A : IDay
    {
        public void Run()
        {
            string input = File.ReadAllLines(@"..\..\..\Day17\Day17.txt").First();
            int numberOfSteps = int.Parse(input);

            // This spinlock's algorithm is simple but efficient, quickly consuming everything in its path. It starts with a
            // circular buffer containing only the value 0, which it marks as the current position.
            List<int> buffer = new() { 0 };
            int currentPosition = 0;

            // It repeats this process of stepping forward, inserting a new value, and using the location of the inserted value as
            // the new current position a total of 2017 times, inserting 2017 as its final operation, and ending with a total of
            // 2018 values (including 0) in the circular buffer.
            for (int newValue = 1; newValue <= 2017; ++newValue)
            {
                // It then steps forward through the circular buffer some number of steps (your puzzle input)
                currentPosition = (currentPosition + numberOfSteps) % newValue;
                // before inserting the (...) new value (...) after the value it stopped on.
                buffer.Insert(currentPosition + 1, newValue);
                // The inserted value becomes the current position.
                ++currentPosition;
            }

            // Perhaps, if you can identify the value that will ultimately be after the last value written (2017), you can
            // short-circuit the spinlock. In this example, that would be 638.
            int valueAfter2017 = buffer.ElementAt(currentPosition + 1);

            // What is the value after 2017 in your completed circular buffer?
            int output = valueAfter2017;
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
