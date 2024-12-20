using System.Text;
using Dumpify;


var input = File.ReadAllText("exampledata.txt").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

var towelPatterns = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                            .OrderByDescending(x => x.Length)
                            .ThenBy(x => x)
                            .ToArray();

// var towelPatterns = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

var towelDesigns = input[1].Split('\n', StringSplitOptions.RemoveEmptyEntries)
                           .Select(x => new TowelDesign { Design = x }).ToArray();

// i want to match the towel patterns with each of the towel designs.
int matchingDesigns = 0;


for (int i = 0; i < towelDesigns.Length; i++)
{
    if (towelDesigns[i].MatchPattern(towelPatterns))
    {
        matchingDesigns++;
        PrintMatchedDesign(towelDesigns[i]);
    }
    else
    {
        Console.WriteLine($"{towelDesigns[i].Design} is impossible");
        Console.WriteLine();
    }
}

void PrintMatchedDesign(TowelDesign towelDesign)
{
    Console.WriteLine($"Matched design: {towelDesign.Design}");
    Console.WriteLine($"Matched patterns: {string.Join(", ", towelDesign.MatchedPatterns)}");
    Console.WriteLine();
}

Console.WriteLine($"Total matching designs: {matchingDesigns}");

public class TowelDesign
{
    public string Design { get; set; }
    public List<string> MatchedPatterns { get; set; } = new List<string>();

    public bool MatchPattern(string[] patterns)
    {
        string clone = new string(Design);
        foreach (var pattern in patterns)
        {
            while (clone.Contains(pattern))
            {
                clone = clone.ReplaceFirst(pattern, string.Empty);
                MatchedPatterns.Add(pattern);
            }
        }
        return string.IsNullOrWhiteSpace(clone);
    }
}