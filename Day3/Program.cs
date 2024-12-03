using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

// PART 1
var pattern1 = new Regex(@"mul\((\d+),(\d+)\)");
var matches1 = pattern1.Matches(input);
var sum1 = matches1.Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
Console.WriteLine("Answer to Part 1 is: " + sum1);


// PART 2
var pattern2 = new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");
var matches2 = pattern2.Matches(input);
bool isDo = true;
int sum2 = 0;

foreach (Match match in matches2)
{
    if (match.Groups[1].Success && match.Groups[2].Success)
    {
        if (isDo)
        {
            sum2 += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }
    }
    else
    {
        if (match.Value == "do()")
        {
            isDo = true;
        }
        else
        {
            isDo = false;
        }
    }
}

Console.WriteLine($"Answer to Part 2 is: {sum2}");