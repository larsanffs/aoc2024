namespace Day13;

public class Prize
{
    public Position Position { get; set; }

    public Prize(Position position)
    {
        Position = position;
    }

    public override string ToString()
    {
        return $"Prize: X={Position.X}, Y={Position.Y}";
    }
}
