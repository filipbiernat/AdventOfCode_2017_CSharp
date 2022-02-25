namespace AdventOfCode2017.Day25
{
    public static class Utils
    {
        public static string ReadWordInPosition(string text, int wordPosition)
        {
            string[] Separators = new string[] { " ", ":", ".", "-" };
            const StringSplitOptions stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
            string[] words = text.Split(Separators, stringSplitOptions);
            return words.ElementAt(wordPosition);
        }
    }
}
