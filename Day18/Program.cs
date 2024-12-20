
using System.Globalization;

var input = File.ReadAllLines("input.txt").Take(1024);

Space[,] spaces = new Space[71, 71];

for (int y = 0; y < 71; y++)
{
    for (int x = 0; x < 71; x++)
    {
        spaces[x, y] = new Space(x, y);
    }
}

foreach (var bytePos in input)
{
    var xy = bytePos.Split(',');
    var x = int.Parse(xy[0]);
    var y = int.Parse(xy[1]);
    spaces[x, y].IsCorrupted = true;
}

List<(int, int)>? GetPath(Space[,] spaces)
{
    int rows = spaces.GetLength(0);
    int cols = spaces.GetLength(1);
    int startX = 0;
    int startY = 0;
    int endX = rows - 1;
    int endY = cols - 1;
    var directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
    var queue = new Queue<(int x, int y, List<(int, int)> path)>();
    var visited = new HashSet<(int, int)>();

    queue.Enqueue((startX, startY, new List<(int, int)> { (startX, startY) }));
    visited.Add((startX, startY));

    while (queue.Count > 0)
    {
        var (x, y, path) = queue.Dequeue();

        if (x == endX && y == endY)
        {
            Console.WriteLine("Path found!");
            return path;
        }

        foreach (var (dx, dy) in directions)
        {
            int newX = x + dx;
            int newY = y + dy;

            if (newX >= 0 && newX < rows && newY >= 0 && newY < cols &&
                !spaces[newX, newY].IsCorrupted && !visited.Contains((newX, newY)))
            {
                var newPath = new List<(int, int)>(path) { (newX, newY) };
                queue.Enqueue((newX, newY, newPath));
                visited.Add((newX, newY));
            }
        }
    }

    Console.WriteLine("No path found.");
    return default;
}


var path = GetPath(spaces);
Console.WriteLine("Number of steps: " + (path?.Count - 1));

for (int y = 0; y < spaces.GetLength(1); y++)
{
    for (int x = 0; x < spaces.GetLength(0); x++)
    {
        var space = spaces[x, y];
        if (space.IsCorrupted)
        {
            Console.Write("#");
        }
        else if (path?.Contains((space.X, space.Y)) == true)
        {
            Console.Write("O");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}

record Space(int X, int Y)
{
    public bool IsCorrupted { get; set; } = false;
}