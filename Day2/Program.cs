
var lines = File.ReadAllLines("input.txt");


var reports = lines.Select((line, index) =>
{
    var levels = line.Split(' ').Select(int.Parse).ToList();
    return new Report(levels, index);
}).ToList();


var safeReports = reports.Where(r => r.CheckIfSafe_Part1()).ToList();
Console.WriteLine($"Answer to part 1: {safeReports.Count}");

var safeReports2 = reports.Where(r => r.CheckIfSafe_Part2()).ToList();
Console.WriteLine($"Answer to part 2: {safeReports2.Count}");


Console.WriteLine($"How many reports total: {reports.Count}");



public class Report(List<int> levels, int index)
{
    private readonly int minDiff = 1;
    private readonly int maxDiff = 3;

    public List<int> Levels => levels;

    void RemoveAt(int i)
    {
        levels.RemoveAt(i);
    }

    private Report Clone()
    {
        return new Report(levels.ToList(), index);
    }

    public bool CheckIfSafe_Part1()
    {
        // check if the levels is either all increasing or all decreasing
        var increasing = levels.Zip(levels.Skip(1), (a, b) => a < b).All(x => x);
        var decreasing = levels.Zip(levels.Skip(1), (a, b) => a > b).All(x => x);

        if (!increasing && !decreasing)
        {
            return false;
        }

        // any two adjecent levels differ by at least one and at most three
        var diff = levels.Zip(levels.Skip(1), (a, b) => Math.Abs(a - b)).All(x => x >= minDiff && x <= maxDiff);

        return diff;
    }

    public bool CheckIfSafe_Part2()
    {
        var checkIfSafeWithoutRemovingAnyLevel = CheckIfSafe_Part1();
        if (checkIfSafeWithoutRemovingAnyLevel)
        {
            return true;
        }

        // check if we can remove any level and still have a safe report
        for (int i = 0; i < levels.Count; i++)
        {
            var newReport = Clone();
            newReport.RemoveAt(i);

            if (newReport.CheckIfSafe_Part1())
            {
                return true;
            }
        }

        return false;
    }
};



