using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day20
{
    public class Day20B : IDay
    {
        public void Run()
        {
            // Suddenly, the GPU contacts you, asking for help. Someone has asked it to simulate too many particles, and it won't
            // be able to finish them all in time to render the next frame at this rate. It transmits to you a buffer (your puzzle
            // input) listing each particle in order (starting with particle 0, then particle 1, particle 2, and so on).
            string[] input = File.ReadAllLines(@"..\..\..\Day20\Day20.txt");
            List<Particle> particles = input.Select(line => new Particle(line)).ToList();

            // To simplify the problem further, the GPU would like to remove any particles that collide. Particles collide if
            // their positions ever exactly match. Because particles are updated simultaneously, more than two particles can
            // collide at the same time and place. Once particles collide, they are removed and cannot collide with anything
            // else after that tick.
            for (int tick = 0; tick < 50; ++tick)
            {
                particles.ForEach((particle) => particle.Tick());
                List<Coords> safePositions = particles.GroupBy(particle => particle.GetPosition())
                    .Where(group => group.Count() < 2)
                    .Select(group => group.Key)
                    .ToList();
                particles = particles.Where(particle => safePositions.Contains(particle.GetPosition()))
                    .ToList();
            }

            // How many particles are left after all collisions are resolved?
            int output = particles.Count;
            Console.WriteLine("Solution: {0}.", output);
        }

        private class Particle
        {
            // For each particle, it provides the X, Y, and Z coordinates for the particle's position (p), velocity (v), and
            // acceleration (a), each in the format <X,Y,Z>.
            private Coords Position;
            private Coords Velocity;
            private readonly Coords Acceleration;

            public Particle(string input)
            {
                List<string> coordinates = Regex.Matches(input, @"<(.*?)>").Select(match => match.Groups[1].Value).ToList();
                Position = new(coordinates.ElementAt(0));
                Velocity = new(coordinates.ElementAt(1));
                Acceleration = new(coordinates.ElementAt(2));
            }

            public void Tick()
            {
                // Each tick, all particles are updated simultaneously. A particle's properties are updated in the following order:
                // - Increase the X velocity by the X acceleration.
                // - Increase the Y velocity by the Y acceleration.
                // - Increase the Z velocity by the Z acceleration.
                Velocity += Acceleration;
                // - Increase the X position by the X velocity.
                // - Increase the Y position by the Y velocity.
                // - Increase the Z position by the Z velocity.
                Position += Velocity;
            }

            public Coords GetPosition() => Position;
        }
    }
}
