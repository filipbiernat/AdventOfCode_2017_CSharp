namespace AdventOfCode2017.Day20
{
    public class Coords
    {
        public int X, Y, Z;

        public Coords(int x, int y, int z) => FillCoordinates(x, y, z);

        public Coords(string input)
        {
            List<int> coords = input.Split(",").Select(int.Parse).ToList();
            FillCoordinates(coords.ElementAt(0), coords.ElementAt(1), coords.ElementAt(2));
        }

        public int GetManhattanDistance() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public static Coords operator +(Coords lhs, Coords rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        public override int GetHashCode() => (X.GetHashCode() << 4) ^ (Y.GetHashCode() << 2) ^ Z.GetHashCode();
        public override bool Equals(object? obj) =>
            (obj != null) &&
            (X == ((Coords)obj).X) &&
            (Y == ((Coords)obj).Y) &&
            (Z == ((Coords)obj).Z);

        private void FillCoordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
