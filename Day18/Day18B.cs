namespace AdventOfCode2017.Day18
{
    public class Day18B : IDay
    {
        public void Run()
        {
            string[] input = File.ReadAllLines(@"..\..\..\Day18\Day18.txt");

            // As you congratulate yourself for a job well done, you notice that the documentation has been on the back of the
            // tablet this entire time. While you actually got most of the instructions correct, there are a few key differences.
            // This assembly code isn't about sound at all - it's meant to be run twice at the same time.
            Program program0 = new(input, 0);
            Program program1 = new(input, 1);
            program0.EstablishOutgoingCommunication(program1);
            program1.EstablishOutgoingCommunication(program0);
            while (!program0.IsLocked() || !program1.IsLocked())
            {
                program0.Run();
                program1.Run();
            }

            // Once both of your programs have terminated (regardless of what caused them to do so), how many times did program 1
            // send a value?
            long output = program1.GetSendCount();
            Console.WriteLine("Solution: {0}.", output);
        }

        private class Program
        {
            // Each running copy of the program has its own set of registers and follows the code independently - in fact, the
            // programs don't even necessarily run at the same speed.
            private readonly Dictionary<string, long> Registers = new();
            private readonly string[] Input;

            // These values wait in a queue until that program is ready to receive them. Each program has its own message queue,
            // so a program can never receive a message it sent. Values are received in the order they are sent.
            private readonly Queue<long> IncomingMessageQueue = new();
            private Queue<long> OutgoingMessageQueue = new();

            // It should be noted that it would be equally valid for the programs to run at different speeds; for example,
            // program 0 might have sent all three values and then stopped at the first rcv before program 1 executed even its
            // first instruction.
            private long InstructionIndex = 0;

            private bool Locked = false;
            private int OutgoingMessageCount = 0;

            public Program(string[] input, int programId)
            {
                Input = input;
                // Each program also has its own program ID (one 0 and the other 1); the register p should begin with this value.
                Registers["p"] = programId;
            }

            public void EstablishOutgoingCommunication(Program other) => OutgoingMessageQueue = other.IncomingMessageQueue;
            public bool IsLocked() => Locked;
            public int GetSendCount() => OutgoingMessageCount;

            public void Run()
            {
                // After each jump instruction, the program continues with the instruction to which the jump jumped. After any other
                // instruction, the program continues with the next instruction. Continuing (or jumping) off either end of the program
                // terminates it.
                if (InstructionIndex < Input.Length)
                {
                    string[] command = Input[InstructionIndex].Split();
                    string instruction = command.ElementAt(0);
                    string registerX = command.ElementAt(1);
                    long argumentX = ReadRegisterOrNumber(registerX);
                    long argumentY = ReadRegisterOrNumber(command.ElementAtOrDefault(2) ?? "0");

                    // There aren't that many instructions, so it shouldn't be hard to figure out what they do.
                    if (instruction == "snd")
                    {
                        // snd X sends the value of X to the other program.
                        OutgoingMessageQueue.Enqueue(argumentX);
                        ++OutgoingMessageCount;
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
                        if (IncomingMessageQueue.Count > 0)
                        {
                            // rcv X receives the next value and stores it in register X.
                            FillRegister(registerX, IncomingMessageQueue.Dequeue());
                            Locked = false;
                        }
                        else
                        {
                            // If no values are in the queue, the program waits for a value to be sent to it. Programs do not
                            // continue to the next instruction until they have received a value.
                            --InstructionIndex;
                            Locked = true;
                        }
                    }
                    else if (instruction == "jgz")
                    {
                        // jgz X Y jumps with an offset of the value of Y, but only if the value of X is greater than zero.
                        // (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)
                        if (argumentX > 0)
                        {
                            InstructionIndex += argumentY - 1;
                        }
                    }
                    ++InstructionIndex;
                }
                else
                {
                    Locked = true;
                }
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
}
