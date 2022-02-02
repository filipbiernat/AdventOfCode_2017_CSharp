namespace AdventOfCode2017.Day24
{
    public class Day24B : IDay
    {
        public void Run()
        {
            // You take an inventory of the components by their port types (your puzzle input).
            string[] input = File.ReadAllLines(@"..\..\..\Day24\Day24.txt");
            List<Component> components = input.Select(component => new Component(component)).ToList();

            // What is the strength of the longest bridge you can make?
            int output = FindLengthAndStrengthOfTheLongestBridge(components).strength;
            Console.WriteLine("Solution: {0}.", output);
        }

        // Your side of the pit is metallic; a perfect surface to connect a magnetic, zero-pin port. Because of this, the first
        // port you use must be of type 0. It doesn't matter what type of port you end with; your goal is just to make the bridge
        // as strong as possible.
        private static (int length, int strength) FindLengthAndStrengthOfTheLongestBridge(List<Component> componentsLeft,
            Component? currentComponent = null,
            int currentConnectionPort = 0,
            int lengthOfTheBridgeSoFar = 0,
            int strengthOfTheBridgeSoFar = 0)
        {
            if (currentComponent != null)
            {
                componentsLeft.Remove(currentComponent);
                ++lengthOfTheBridgeSoFar;
                // The strength of a bridge is the sum of the port types in each component.
                strengthOfTheBridgeSoFar += currentComponent.GetStrength();
            }

            // If you can make multiple bridges of the longest length, pick the strongest one.
            return componentsLeft
                .Where(nextComponent => nextComponent.HasPort(currentConnectionPort))
                .Select(nextComponent =>
                    FindLengthAndStrengthOfTheLongestBridge(componentsLeft.ToList(),
                        nextComponent,
                        nextComponent.GetAnotherPort(currentConnectionPort),
                        lengthOfTheBridgeSoFar,
                        strengthOfTheBridgeSoFar))
                .OrderByDescending(restOfTheBridge => restOfTheBridge.length)
                .ThenByDescending(restOfTheBridge => restOfTheBridge.strength)
                .FirstOrDefault((lengthOfTheBridgeSoFar, strengthOfTheBridgeSoFar));
        }
    }
}
