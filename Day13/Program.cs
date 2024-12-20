namespace Day13;

public class Program
{
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var clawMachines = ParseTestCases(lines);

        var sumOfAllTokens = clawMachines.Sum(cm => cm.CalculateTokens() ?? 0);
        Console.WriteLine("Answer to Part 1: " + sumOfAllTokens);
    }

    private static List<ClawMachine> ParseTestCases(string[] lines)
    {
        var testCases = new List<ClawMachine>();

        for (int i = 0; i < lines.Length; i += 4) // Each test case takes 3 lines + 1 blank line
        {
            var buttonA = ParseButton(lines[i]);
            var buttonB = ParseButton(lines[i + 1]);
            var prize = ParsePrize(lines[i + 2]);

            testCases.Add(new ClawMachine { ButtonA = buttonA, ButtonB = buttonB, Prize = prize });
        }

        return testCases;
    }

    private static Button ParseButton(string line)
    {
        // Format: "Button A: X+94, Y+34"
        var parts = line.Split(new[] { ": ", ", " }, StringSplitOptions.None);
        var name = parts[0].Last();
        var x = int.Parse(parts[1].Substring(2)); // Skip "X+"
        var y = int.Parse(parts[2].Substring(2)); // Skip "Y+"

        return new Button(name, new Position(x, y));
    }

    private static Prize ParsePrize(string line)
    {
        // Format: "Prize: X=8400, Y=5400"
        var parts = line.Split(new[] { ": ", ", " }, StringSplitOptions.None);
        var x = int.Parse(parts[1].Substring(2)); // Skip "X="
        var y = int.Parse(parts[2].Substring(2)); // Skip "Y="

        return new Prize(new Position(x, y));
    }
}
