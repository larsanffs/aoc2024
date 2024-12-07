var equations = File.ReadAllLines("input.txt");

List<Equation> equationsList = equations.Select(e => e.Split(':', StringSplitOptions.TrimEntries)).Select(e =>
{
    var numbers = e[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                      .Select(long.Parse)
                      .ToList();
    var sum = long.Parse(e[0]);
    return new Equation(numbers, sum);
}).ToList();

Console.WriteLine(equationsList.Count);

var sumOfAllPossible = 0L;
var notPossibleList = new List<Equation>();

foreach (var equation in equationsList)
{
    if (equation.IsPossible())
    {
        sumOfAllPossible += equation.Sum;
    }
    else
    {
        notPossibleList.Add(equation);
    }
}

Console.WriteLine("Answer to part 1: " + sumOfAllPossible);

// Part 2
foreach (var equation in notPossibleList)
{
    var equationDeluxe = new EquationDeluxe(equation.Numbers, equation.Sum);
    if (equationDeluxe.IsPossible())
    {
        sumOfAllPossible += equation.Sum;
    }
}

Console.WriteLine("Answer to part 2: " + sumOfAllPossible);
public class Equation
{
    private readonly List<long> numbers;
    private readonly long targetSum;
    private readonly char[] operators = { '+', '*' };

    public Equation(List<long> numbers, long targetSum)
    {
        this.numbers = numbers;
        this.targetSum = targetSum;
    }

    public long Sum => targetSum;

    public List<long> Numbers => numbers;

    public bool IsPossible()
    {
        return TryAllCombinations(numbers[0], 1);
    }

    private bool TryAllCombinations(double currentResult, int currentIndex)
    {
        if (currentIndex == numbers.Count)
        {
            return Math.Abs(currentResult - targetSum) < 0.0001; // Using epsilon for double comparison
        }

        foreach (char op in operators)
        {
            double nextResult = Calculate(currentResult, numbers[currentIndex], op);

            if (TryAllCombinations(nextResult, currentIndex + 1))
            {
                return true;
            }
        }

        return false;
    }

    private double Calculate(double a, double b, char op)
    {
        return op switch
        {
            '+' => a + b,
            '*' => a * b,
            _ => throw new ArgumentException("Invalid operator")
        };
    }
}

public class EquationDeluxe
{
    private readonly List<long> numbers;
    private readonly long targetSum;
    private readonly char[] operators = { '+', '*', '|' };

    public EquationDeluxe(List<long> numbers, long targetSum)
    {
        this.numbers = numbers;
        this.targetSum = targetSum;
    }

    public long Sum => targetSum;

    public bool IsPossible()
    {
        return TryAllCombinations(numbers[0], 1);
    }

    private bool TryAllCombinations(double currentResult, int currentIndex)
    {
        if (currentIndex == numbers.Count)
        {
            return Math.Abs(currentResult - targetSum) < 0.0001;
        }

        foreach (char op in operators)
        {
            double nextResult = Calculate(currentResult, numbers[currentIndex], op);

            if (TryAllCombinations(nextResult, currentIndex + 1))
            {
                return true;
            }
        }

        return false;
    }

    private double Calculate(double a, double b, char op)
    {
        return op switch
        {
            '+' => a + b,
            '*' => a * b,
            '|' => double.Parse($"{(long)a}{(long)b}"), // Concatenate numbers
            _ => throw new ArgumentException("Invalid operator")
        };
    }
}