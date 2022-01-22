namespace AdventOfCode2017.Day18
{
    public class Day18A : IDay
    {
        // It seems like the assembly is meant to operate on a set of registers that are each named with a single letter and that
        // can each hold a single integer.
        private readonly Dictionary<string, long> Registers = new();

        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day18\Day18.txt");

            // You discover a tablet containing some strange assembly code labeled simply "Duet". Rather than bother the sound card
            // with it, you decide to run the code yourself. Unfortunately, you don't see any documentation, so you're left to
            // figure out what the instructions mean on your own.
            long playedSound = long.MaxValue;
            long recoveredSound = long.MaxValue;

            // After each jump instruction, the program continues with the instruction to which the jump jumped. After any other
            // instruction, the program continues with the next instruction. Continuing (or jumping) off either end of the program
            // terminates it.
            for (long instructionIndex = 0; instructionIndex < input.Length; ++instructionIndex)
            {
                string[] command = input[instructionIndex].Split();
                string instruction = command.ElementAt(0);
                string registerX = command.ElementAt(1);
                long argumentX = ReadRegisterOrNumber(registerX);
                long argumentY = ReadRegisterOrNumber(command.ElementAtOrDefault(2) ?? "0");

                // There aren't that many instructions, so it shouldn't be hard to figure out what they do.
                if (instruction == "snd")
                {
                    // snd X plays a sound with a frequency equal to the value of X.
                    playedSound = argumentX;
                }
                else if (instruction == "set")
                {
                    // set X Y sets register X to the value of Y.
                    FillRegister(registerX, argumentY);
                }
                else if (instruction == "add")
                {
                    // add X Y increases register X by the value of Y.
                    FillRegister(registerX, argumentX + argumentY);
                }
                else if (instruction == "mul")
                {
                    // mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
                    FillRegister(registerX, argumentX * argumentY);
                }
                else if (instruction == "mod")
                {
                    // mod X Y sets register X to the remainder of dividing the value contained in register X by the value of Y
                    // (that is, it sets X to the result of X modulo Y).
                    FillRegister(registerX, argumentX % argumentY);
                }
                else if (instruction == "rcv")
                {
                    // rcv X recovers the frequency of the last sound played, but only when the value of X is not zero.
                    // (If it is zero, the command does nothing.)
                    if (argumentX != 0)
                    {
                        recoveredSound = playedSound;
                        break;
                    }
                }
                else if (instruction == "jgz")
                {
                    // jgz X Y jumps with an offset of the value of Y, but only if the value of X is greater than zero.
                    // (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)
                    if (argumentX > 0)
                    {
                        instructionIndex += argumentY - 1;
                    }
                }
            }

            // What is the value of the recovered frequency (the value of the most recently played sound) the first time a rcv
            // instruction is executed with a non-zero value?
            long output = recoveredSound;
            Console.WriteLine("Solution: {0}.", output);
        }

        private void FillRegister(string register, long value)
        {
            InitializeRegisterIfNecessary(register);
            Registers[register] = value;
        }

        private long ReadRegisterOrNumber(string argument)
        {
            // Many of the instructions can take either a register (a single letter) or a number.
            bool isNumber = long.TryParse(argument, out long number);
            // The value of a register is the integer it contains; the value of a number is that number.
            return isNumber ? number : ReadRegister(argument);
        }

        private long ReadRegister(string register)
        {
            InitializeRegisterIfNecessary(register);
            return Registers[register];
        }

        private void InitializeRegisterIfNecessary(string register)
        {
            // You suppose each register should start with a value of 0.
            if (!Registers.ContainsKey(register))
            {
                Registers[register] = 0;
            }
        }
    }
}
