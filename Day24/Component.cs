namespace AdventOfCode2017.Day24
{
    // Each component has two ports, one on each end. The ports come in all different types, and only matching types can be
    // connected. Each port is identified by the number of pins it uses; more pins mean a stronger connection for your bridge.
    public class Component
    {
        private readonly List<int> Ports;

        public Component(string component) => Ports = component.Split("/").Select(int.Parse).ToList();

        public bool HasPort(int port) => Ports.Contains(port);
        public int GetAnotherPort(int port) => Ports.Distinct().Count() > 1 ? Ports.Where(p => p != port).First() : port;
        public int GetStrength() => Ports.Sum();
    }
}
