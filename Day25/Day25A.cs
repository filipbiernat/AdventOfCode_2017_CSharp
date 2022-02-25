namespace AdventOfCode2017.Day25
{
    public class Day25A : IDay
    {
        public void Run()
        {
            // You find the Turing machine blueprints (your puzzle input) on a tablet in a nearby pile of debris.
            string[] input = File.ReadAllText(@"..\..\..\Day25\Day25.txt").Split("\r\n\r\n");

            // Recreate the Turing machine and save the computer!
            TuringMachine machine = new(input);
            machine.Run();

            // What is the diagnostic checksum it produces once it's working again?
            int output = machine.ReadDiagnosticChecksum();
            Console.WriteLine("Solution: {0}.", output);
        }
    }
}
