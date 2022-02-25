namespace AdventOfCode2017.Day25
{
    public class TuringMachine
    {
        // Looking back up at the broken Turing machine above, you can start to identify its parts:
        //  - A tape which contains 0 repeated infinitely to the left and right.
        private readonly Tape Tape = new();
        //  - A cursor, which can move left or right along the tape and read or write values at its current position.
        private int CursorPosition = 0;
        //  - A set of states, each containing rules about what to do based on the current value under the cursor.
        private readonly Dictionary<char, State> States = new();

        private readonly char InitialState;
        private readonly int NumberOfSteps;

        public TuringMachine(string[] input)
        {
            string[] generalDescription = input.First().Split("\r\n");
            InitialState = Convert.ToChar(Utils.ReadWordInPosition(generalDescription.ElementAt(0), 3));
            NumberOfSteps = Convert.ToInt32(Utils.ReadWordInPosition(generalDescription.ElementAt(1), 5));

            foreach (string stateDescription in input.Skip(1))
            {
                State state = new(stateDescription);
                States[state.GetId()] = state;
            }
        }

        // The CPU can confirm that the Turing machine is working by taking a diagnostic checksum after a specific number of steps
        // (given in the blueprint). Once the specified number of steps have been executed, the Turing machine should pause.
        public void Run()
        {
            char nextState = InitialState;
            for (int stepCount = 0; stepCount < NumberOfSteps; ++stepCount)
            {
                nextState = States[nextState].Run(Tape, ref CursorPosition);
            }
        }

        // Once it does, count the number of times 1 appears on the tape.
        public int ReadDiagnosticChecksum() => Tape.CountOnes();
    }
}
