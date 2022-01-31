﻿namespace AdventOfCode2017.Day22
{
    public class Coords
    {
        public int Row;
        public int Column;

        public Coords(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Coords operator +(Coords lhs, Coords rhs) => new(lhs.Row + rhs.Row, lhs.Column + rhs.Column);
        public override int GetHashCode() => (Row.GetHashCode() << 2) ^ Column.GetHashCode();
        public override bool Equals(object? obj) =>
            (obj != null) &&
            (Row == ((Coords)obj).Row) &&
            (Column == ((Coords)obj).Column);
    }
}
