namespace AdventOfCode2017.Day25
{
    public class Rule
    {
        // Based on whether the cursor is pointing at a 0 or a 1, the current state says:
        //   - what value to write at the current position of the cursor,
        private readonly bool WriteOne;
        //   - whether to move the cursor left or right one slot,
        private readonly bool MoveRight;
        //   - and which state to use next.
        private readonly char NextState;

        public Rule(string[] input)
        {
            WriteOne = Utils.ReadWordInPosition(input.ElementAt(0), 3) == "1";
            MoveRight = Utils.ReadWordInPosition(input.ElementAt(1), 5) == "right";
            NextState = Convert.ToChar(Utils.ReadWordInPosition(input.ElementAt(2), 3));
        }

        public char Run(Tape tape, ref int cursorPosition)
        {
            tape.Write(cursorPosition, WriteOne);
            MoveCursor(ref cursorPosition);
            return NextState;
        }

        private void MoveCursor(ref int cursorPosition)
        {
            if (MoveRight)
            {
                --cursorPosition;
            }
            else
            {
                ++cursorPosition;
            }
        }
    }
}
