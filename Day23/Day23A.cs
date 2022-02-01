namespace AdventOfCode2017.Day23
{
    public class Day23A : IDay
    {
        // It seems like the assembly is meant to operate on a set of registers that are each named with a single letter and that
        // can each hold a single integer.
        private readonly Dictionary<string, int> Registers = new();

        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day23\Day23.txt");

            int mulInstructionCount = 0;

            // After each jump instruction, the program continues with the instruction to which the jump jumped. After any other
            // instruction, the program continues with the next instruction. Continuing (or jumping) off either end of the program
            // terminates it.
            for (int instructionIndex = 0; instructionIndex < input.Length; ++instructionIndex)
            {
                string[] command = input.ElementAt(instructionIndex).Split();
                string instruction = command.ElementAt(0);
                string registerX = command.ElementAt(1);
                int argumentX = ReadRegisterOrNumber(registerX);
                int argumentY = ReadRegisterOrNumber(command.ElementAtOrDefault(2) ?? "0");

                if (instruction == "set")
                {
                    // set X Y sets register X to the value of Y.
                    FillRegister(registerX, argumentY);
                }
                else if (instruction == "sub")
                {
                    // sub X Y decreases register X by the value of Y.
                    FillRegister(registerX, argumentX - argumentY);
                }
                else if (instruction == "mul")
                {
                    // mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
                    FillRegister(registerX, argumentX * argumentY);
                    ++mulInstructionCount;
                }
                else if (instruction == "jnz")
                {
                    // jnz X Y jumps with an offset of the value of Y, but only if the value of X is not zero.
                    // (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)
                    if (argumentX != 0)
                    {
                        instructionIndex += argumentY - 1;
                    }
                }
            }

            // If you run the program (your puzzle input), how many times is the mul instruction invoked?
            int output = mulInstructionCount;
            Console.WriteLine("Solution: {0}.", output);
        }

        private void FillRegister(string register, int value)
        {
            InitializeRegisterIfNecessary(register);
            Registers[register] = value;
        }

        private int ReadRegisterOrNumber(string argument)
        {
            // Many of the instructions can take either a register (a single letter) or a number.
            bool isNumber = int.TryParse(argument, out int number);
            // The value of a register is the integer it contains; the value of a number is that number.
            return isNumber ? number : ReadRegister(argument);
        }

        private int ReadRegister(string register)
        {
            InitializeRegisterIfNecessary(register);
            return Registers[register];
        }

        private void InitializeRegisterIfNecessary(string register)
        {
            // The eight registers here, named a through h, all start at 0.
            if (!Registers.ContainsKey(register))
            {
                Registers[register] = 0;
            }
        }
    }
}
