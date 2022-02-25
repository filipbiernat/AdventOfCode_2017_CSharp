namespace AdventOfCode2017.Day25
{
    public class Tape
    {
        // Each slot on the tape has two possible values: 0 (the starting value for all slots) and 1.
        private readonly HashSet<int> OnesOnTape = new();

        public void Write(int position, bool writeOne)
        {
            if (writeOne)
            {
                OnesOnTape.Add(position);
            }
            else
            {
                OnesOnTape.Remove(position);
            }
        }

        public bool IsOne(int position) => OnesOnTape.Contains(position);
        public int CountOnes() => OnesOnTape.Count;
    }
}
