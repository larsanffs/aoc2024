var input = await File.ReadAllTextAsync("input.txt");
var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

// split each line into 2 numbers. store left number in a list and the right number in a list
var left = new List<int>();
var right = new List<int>();
foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    left.Add(int.Parse(parts[0]));
    right.Add(int.Parse(parts[1]));
}

// sort both lists in ascending order
left.Sort();
right.Sort();

static void Part1(List<int> left, List<int> right)
{
    // iterate through the left list and also the right list and compute the difference between the two numbers and store it in a separate list
    var diff = new List<int>();
    for (int i = 0; i < left.Count; i++)
    {
        // count the difference between the two numbers. left does not always have to be less than right
        diff.Add(Math.Abs(left[i] - right[i]));
    }

    // count the diff list
    var count = diff.Sum();

    // print the count
    Console.WriteLine($"Anwser to part 1 is: {count}");
}

void Part2(List<int> left, List<int> right)
{
    // iterate through the left list and find how many occurrences of the same number in the right list then calculate the value times the number of occurrences
    var count = 0;
    for (int i = 0; i < left.Count; i++)
    {
        var num = left[i];
        var occurence = right.Count(x => x == num);
        count += num * occurence;
    }
    Console.WriteLine($"Anwser to part 2 is: {count}");
}

Part1(left, right);
Part2(left, right);
