namespace AdventOfCode2017.Day8
{
    public partial class Day8A : IDay
    {
        // The CPU doesn't have the bandwidth to tell you what all the registers are named, and leaves that to you to determine.
        private readonly Dictionary<string, int> Registers = new();

        public void Run()
        {
            // You receive a signal directly from the CPU. Because of your recent assistance with jump instructions, it would like
            // you to compute the result of a series of unusual register instructions.
            string[] input = File.ReadAllLines(@"..\..\..\Day8\Day8.txt");
            input.ToList().ForEach(EvaluateInstruction);

            // What is the largest value in any register after completing the instructions in your puzzle input?
            int output = Registers.Values.Max();
            Console.WriteLine("Solution: {0}.", output);
        }

        private void EvaluateInstruction(string instruction)
        {
            // Each instruction consists of several parts: 
            string[] entries = instruction.Split();
            // - the register to modify
            string register = entries.ElementAt(0);
            // - whether to increase or decrease that register's value
            string operation = entries.ElementAt(1);
            // - the amount by which to increase or decrease it
            int amount = int.Parse(entries.ElementAt(2));
            // - and a condition
            bool condition = EvaluateCondition(entries.ElementAt(4), entries.ElementAt(5), entries.ElementAt(6));

            // If the condition fails, skip the instruction without modifying the register. 
            if (condition)
            {
                RunOperation(register, operation, amount);
            }
        }

        private bool EvaluateCondition(string register, string operand, string amount)
        {
            int lhs = ReadRegister(register);
            int rhs = int.Parse(amount);
            return Conditions[operand](lhs, rhs);
        }

        private void RunOperation(string register, string operation, int amount)
        {
            InitializeRegisterIfNecessary(register);
            if (operation == "inc")
            {
                Registers[register] += amount;
            }
            else if (operation == "dec")
            {
                Registers[register] -= amount;
            }
        }

        private int ReadRegister(string register)
        {
            InitializeRegisterIfNecessary(register);
            return Registers[register];
        }

        private void InitializeRegisterIfNecessary(string register)
        {
            // The registers all start at 0.
            if (!Registers.ContainsKey(register))
            {
                Registers.Add(register, 0);
            }
        }

        private static readonly Dictionary<string, Func<int, int, bool>> Conditions = new()
        {
            { ">", (lhs, rhs) => lhs > rhs },
            { "<", (lhs, rhs) => lhs < rhs },
            { ">=", (lhs, rhs) => lhs >= rhs },
            { "<=", (lhs, rhs) => lhs <= rhs },
            { "==", (lhs, rhs) => lhs == rhs },
            { "!=", (lhs, rhs) => lhs != rhs }
        };
    }
}
