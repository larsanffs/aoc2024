using System.Data;
using System.Diagnostics;

var input = File.ReadAllText("input.txt");

var inputData = GetInputData(input);

var correctUpdateList = new List<List<int>>();
var incorrectUpdateList = new List<List<int>>();


foreach (var update in inputData.Updates)
{
    var updateCorrect = true;

    foreach (var rule in inputData.Rules)
    {
        bool checkIfBothValuesExist = update.Contains(rule.Left) && update.Contains(rule.Right);
        if (checkIfBothValuesExist)
        {
            bool isBefore = IsLeftBeforeRight(update, rule.Left, rule.Right);
            if (!isBefore)
            {
                // The values are not in the correct order
                updateCorrect = false;
                break;
            }
        }
    }

    if (updateCorrect)
    {
        correctUpdateList.Add(update);
    }
    else
    {
        incorrectUpdateList.Add(update);
    }
}

Console.WriteLine($"Correct updates: {correctUpdateList.Count}");

correctUpdateList.Sum(u => GetMiddlePage(u));

Console.WriteLine($"Sum of middle pages: {correctUpdateList.Sum(u => GetMiddlePage(u))} (Answer to Part 1)");

// PART 2 starts here
List<LinkedList<int>> listOfUpdates = new List<LinkedList<int>>();

foreach (var update in incorrectUpdateList)
{
    var linkedList = new LinkedList<int>(update);
    listOfUpdates.Add(linkedList);
}

foreach (var update in listOfUpdates)
{
    while (ProcessUpdatesUntilAllRulesPass(update, inputData.Rules))
    {
        // Continue processing until no more changes are needed
    }
}

Console.WriteLine($"Sum of middle pages: {listOfUpdates.Sum(u => GetMiddlePage(u.ToList()))} (Answer to Part 2)");

static InputData GetInputData(string input)
{
    var split = input.Split($"\n\n", StringSplitOptions.RemoveEmptyEntries);

    Debug.Assert(split.Length == 2);

    var rules = split[0].Split('\n').Select(r =>
    {
        var parts = r.Split("|");
        return new Rule(int.Parse(parts[0]), int.Parse(parts[1]));
    }).ToList();

    var list = split[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);

    List<List<int>> updateList = list.Select(u =>
    {
        var parts = u.Split(",");
        return parts.Select(int.Parse).ToList();
    }).ToList();

    return new InputData(rules, updateList);
}

static bool ProcessUpdatesUntilAllRulesPass(LinkedList<int> update, List<Rule> rules)
{
    bool madeChanges = false;

    foreach (var rule in rules)
    {
        bool checkIfBothValuesExist = update.Contains(rule.Left) && update.Contains(rule.Right);
        if (!checkIfBothValuesExist)
        {
            continue;
        }

        var nodeLeft = update.Find(rule.Left);
        var nodeRight = update.Find(rule.Right);
        bool isBefore = IsLeftBeforeRight(update.ToList(), nodeLeft.Value, nodeRight.Value);
        if (!isBefore)
        {
            // The values are not in the correct order
            update.Remove(nodeLeft);
            update.AddBefore(nodeRight, nodeLeft);
            madeChanges = true;
        }
    }

    return madeChanges;
}

static int GetMiddlePage(List<int> list)
{
    if (list == null || list.Count == 0)
    {
        throw new ArgumentException("The list cannot be null or empty.");
    }

    int middleIndex = list.Count / 2;
    return list[middleIndex];
}


static bool IsLeftBeforeRight(List<int> list, int value1, int value2)
{
    int index1 = list.IndexOf(value1);
    int index2 = list.IndexOf(value2);

    if (index1 == -1 || index2 == -1)
    {
        // One or both values are not in the list
        return false;
    }
    return index1 < index2;
}


record Rule(int Left, int Right);
record InputData(List<Rule> Rules, List<List<int>> Updates);