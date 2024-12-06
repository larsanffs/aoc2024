public struct Position : IEquatable<Position>
{
    public int X { get; set; }
    public int Y { get; set; }

    public bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }
}
