namespace AdventOfCode2017.Day25
{
    public class State
    {
        private readonly char ID;
        private readonly Rule RuleIfTheCurrentValueIs0;
        private readonly Rule RuleIfTheCurrentValueIs1;

        public State(string input)
        {
            string[] stateInput = input.Split("\r\n");
            ID = Convert.ToChar(Utils.ReadWordInPosition(stateInput[0], 2));
            RuleIfTheCurrentValueIs0 = new Rule(stateInput[2..5]);
            RuleIfTheCurrentValueIs1 = new Rule(stateInput[6..]);
        }

        public char Run(Tape tape, ref int cursorPosition)
        {
            if (tape.IsOne(cursorPosition))
            {
                return RuleIfTheCurrentValueIs1.Run(tape, ref cursorPosition);
            }
            else
            {
                return RuleIfTheCurrentValueIs0.Run(tape, ref cursorPosition);
            }
        }

        public char GetId() => ID;
    }
}
