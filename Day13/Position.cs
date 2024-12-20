namespace Day13;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"X={X}, Y={Y}";
    }

    public string ToStringWithPlus()
    {
        return $"X+{X}, Y+{Y}";
    }
}
