
using System.ComponentModel;
using System.Text;

public class Map(string[] input)
{
    public char[][] Grid { get; set; } = input.Select(x => x.ToCharArray()).ToArray();
    public bool[][] ObstacleMap => Grid.Select(x => x.Select(y => y == '#').ToArray()).ToArray();

    public Position InitialPlayerPosition
    {
        get
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    if (Grid[i][j] == '^')
                    {
                        return new Position { X = j, Y = i };
                    }
                }
            }

            return new Position();
        }
    }

    public Position MovePlayerPosition(Position position, Direction direction)
    {
        var newPosition = direction switch
        {
            Direction.Up => new Position { X = position.X, Y = position.Y - 1 },
            Direction.Right => new Position { X = position.X + 1, Y = position.Y },
            Direction.Down => new Position { X = position.X, Y = position.Y + 1 },
            Direction.Left => new Position { X = position.X - 1, Y = position.Y },
            _ => throw new InvalidEnumArgumentException()
        };

        UpdatePlayerPosition(position, newPosition);
        return newPosition;
    }

    public void UpdatePlayerPosition(Position oldPosition, Position newPosition)
    {
        // Clear old position
        Grid[oldPosition.Y][oldPosition.X] = '.';

        // Set new position
        Grid[newPosition.Y][newPosition.X] = '^';
    }

    public bool IsPlayerNearObstacle(int x, int y)
    {
        return ObstacleMap[y][x];
    }

    public bool IsPlayerOnEdge(int x, int y)
    {
        // Check if player is on or 1 step away from edges
        return x <= 1 || // Left edge
               y <= 1 || // Top edge
               x >= Grid[0].Length - 2 || // Right edge 
               y >= Grid.Length - 2; // Bottom edge
    }

    public bool IsPlayerAtRisk(int x, int y, Direction direction)
    {
        // Calculate next position based on direction
        int nextX = x;
        int nextY = y;

        switch (direction)
        {
            case Direction.Up:
                nextY--;
                break;
            case Direction.Down:
                nextY++;
                break;
            case Direction.Left:
                nextX--;
                break;
            case Direction.Right:
                nextX++;
                break;
        }

        // Check if next position would be out of bounds
        return nextX < 0 || nextX >= Grid[0].Length ||
               nextY < 0 || nextY >= Grid.Length;
    }

    // override ToString() method to print the map
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var row in Grid)
        {
            sb.AppendLine(new string(row));
        }

        return sb.ToString();
    }
}
