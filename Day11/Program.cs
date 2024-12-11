using Dumpify;

var input = File.ReadAllText("input.txt");

LinkedList<Stone> stones = new LinkedList<Stone>();
input.Split(' ').ToList().ForEach(x => stones.AddLast(new Stone { Engraving = int.Parse(x) }));

Console.WriteLine("Number of stones: " + stones.Count);

int blinkNumber = 1;
int blinkMax = 75;

while (blinkNumber <= blinkMax)
{
    var currentNode = stones.First;
    while (currentNode != null)
    {
        var nextNode = currentNode.Next; // Store next node before modifications
        var stone = currentNode.Value;

        if (stone.Engraving == 0)
            stone.Engraving = 1;
        else if (IsEvenDigits(stone.Engraving))
        {
            // Convert to string once and use ReadOnlySpan<char>
            string engravingStr = stone.ToString();
            ReadOnlySpan<char> engravingSpan = engravingStr.AsSpan();
            int halfLength = engravingSpan.Length / 2;

            // Split into left and right spans
            ReadOnlySpan<char> leftSpan = engravingSpan[..halfLength];
            ReadOnlySpan<char> rightSpan = engravingSpan[halfLength..];

            // Parse directly from spans
            stones.AddBefore(currentNode, new Stone { Engraving = long.Parse(leftSpan) });
            stones.AddAfter(currentNode, new Stone { Engraving = long.Parse(rightSpan) });
            stones.Remove(currentNode);
        }
        else
        {
            stone.Engraving *= 2024;
        }

        currentNode = nextNode; // Move to the next node
    }
    blinkNumber++;

    if (blinkNumber % 5 == 0)
        Console.WriteLine($"Blink {blinkNumber} Stones: {stones.Count}");
}


Console.WriteLine("Number of stones: " + stones.Count);

// Helper methods
static bool IsEvenDigits(long number)
{
    return number.ToString().Length % 2 == 0;
}

static (long Left, long Right) SplitNumber(long number)
{
    var str = number.ToString();
    int mid = str.Length / 2;
    return (long.Parse(str[..mid]), long.Parse(str[mid..]));
}

public record Stone
{
    public long Engraving { get; set; } = 0;
    public override string ToString() => Engraving.ToString();
}