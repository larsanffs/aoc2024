namespace Day13;

public class TestCase
{
    public Button ButtonA { get; set; }
    public Button ButtonB { get; set; }
    public Prize Prize { get; set; }

    public TestCase(Button buttonA, Button buttonB, Prize prize)
    {
        ButtonA = buttonA;
        ButtonB = buttonB;
        Prize = prize;
    }

    public override string ToString()
    {
        return $"{ButtonA}\n{ButtonB}\n{Prize}\n";
    }
}
