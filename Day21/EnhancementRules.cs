namespace AdventOfCode2017.Day21
{
    public static class EnhancementRules
    {
        private static readonly Dictionary<string, string> Rules = new();

        public static string[] Enhance(string[] instruction) => Rules[string.Join("/", instruction)].Split("/");

        public static void ProcessRules(List<string> rules) => rules.ForEach(rule => ProcessRule(rule));

        private static void ProcessRule(string rule)
        {
            string[] entries = rule.Split(" => ");
            string before = entries[0];
            string after = entries[1];

            //  The artist explains that sometimes, one must rotate or flip the input pattern to find a match. (Never rotate or
            //  flip the output pattern, though.) Each pattern is written concisely: rows are listed as single units, ordered
            //  top-down, and separated by slashes.
            for (int rotation = 0; rotation < 4; ++rotation)
            {
                AddRule(before, after);

                bool is2x2 = before.Length < 6;
                if (is2x2)
                {
                    before = Rotate2x2(before);
                }
                else
                {
                    AddRule(Flip3x3Horizontally(before), after);
                    AddRule(Flip3x3Vertically(before), after);
                    before = Rotate3x3(before);
                }
            }
        }

        private static void AddRule(string before, string after)
        {
            if (!Rules.ContainsKey(before))
            {
                Rules[before] = after;
            }
        }

        private static string Rotate2x2(string instruction) =>
            string.Format("{0}{1}/{2}{3}",
            instruction[3], instruction[0],
            instruction[4], instruction[1]);

        private static string Rotate3x3(string instruction) =>
            string.Format("{0}{1}{2}/{3}{4}{5}/{6}{7}{8}",
            instruction[8], instruction[4], instruction[0],
            instruction[9], instruction[5], instruction[1],
            instruction[10], instruction[6], instruction[2]);


        private static string Flip3x3Horizontally(string instruction) =>
            string.Format("{0}{1}{2}/{3}{4}{5}/{6}{7}{8}",
            instruction[2], instruction[1], instruction[0],
            instruction[6], instruction[5], instruction[4],
            instruction[10], instruction[9], instruction[8]);


        private static string Flip3x3Vertically(string instruction) =>
            string.Format("{0}{1}{2}/{3}{4}{5}/{6}{7}{8}",
            instruction[8], instruction[9], instruction[10],
            instruction[4], instruction[5], instruction[6],
            instruction[0], instruction[1], instruction[2]);
    }
}
