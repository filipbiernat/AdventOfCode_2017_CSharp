using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day20
{
    public class Day20A : IDay
    {
        public void Run()
        {
            // Suddenly, the GPU contacts you, asking for help. Someone has asked it to simulate too many particles, and it won't
            // be able to finish them all in time to render the next frame at this rate. It transmits to you a buffer (your puzzle
            // input) listing each particle in order (starting with particle 0, then particle 1, particle 2, and so on).
            string[] input = File.ReadAllLines(@"..\..\..\Day20\Day20.txt");
            List<Particle> particles = input.Select((line, id) => new Particle(line, id)).ToList();

            // Because of seemingly tenuous rationale involving z-buffering, the GPU would like to know which particle will stay
            // closest to position <0,0,0> in the long term. Measure this using the Manhattan distance, which in this situation
            // is simply the sum of the absolute values of a particle's X, Y, and Z position.
            int closestParticleId = particles.OrderBy(particle => particle.GetAccelerationManhattanDistance())
                .ThenBy(particle => particle.GetVelocityManhattanDistance())
                .ThenBy(particle => particle.GetPositionManhattanDistance())
                .First()
                .GetId();

            // Which particle will stay closest to position <0,0,0> in the long term?
            int output = closestParticleId;
            Console.WriteLine("Solution: {0}.", output);
        }

        private class Particle
        {
            // For each particle, it provides the X, Y, and Z coordinates for the particle's position (p), velocity (v), and
            // acceleration (a), each in the format <X,Y,Z>.
            private readonly Coords Position;
            private readonly Coords Velocity;
            private readonly Coords Acceleration;
            private readonly int Id;

            public Particle(string input, int id)
            {
                List<string> coordinates = Regex.Matches(input, @"<(.*?)>").Select(match => match.Groups[1].Value).ToList();
                Position = new (coordinates.ElementAt(0));
                Velocity = new (coordinates.ElementAt(1));
                Acceleration = new (coordinates.ElementAt(2));
                Id = id;
            }

            public int GetId() => Id;
            public int GetPositionManhattanDistance() => Position.GetManhattanDistance();
            public int GetVelocityManhattanDistance() => Velocity.GetManhattanDistance();
            public int GetAccelerationManhattanDistance() => Acceleration.GetManhattanDistance();
        }
    }
}
