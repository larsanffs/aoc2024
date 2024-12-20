
namespace Day13;

public class Button
{
    public char Name { get; set; }
    public Position Movement { get; set; }

    public Button(char name, Position movement)
    {
        Name = name;
        Movement = movement;
    }

    public override string ToString()
    {
        return $"Button {Name}: {Movement.ToStringWithPlus()}";
    }

    internal object CalculateFinalPosition(Position prizePosition)
    {
        return new Position(prizePosition.X + Movement.X, prizePosition.Y + Movement.Y);
    }
}
