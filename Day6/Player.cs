using System.ComponentModel;

public class Player(Map map)
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int StepsCounter { get; set; } = 0;
    public Direction CurrentDirection { get; private set; } = Direction.Up;
    public HashSet<Position> ExploredPositions { get; } = new();
    private void InitializePlayerPosition()
    {
        var startPos = map.InitialPlayerPosition;
        X = startPos.X;
        Y = startPos.Y;
        ExploredPositions.Add(new Position { X = X, Y = Y });
    }

    private bool CheckIfNextStepIsObstacle()
    {
        switch (CurrentDirection)
        {
            case Direction.Up:
                return map.IsPlayerNearObstacle(X, Y - 1);
            case Direction.Right:
                return map.IsPlayerNearObstacle(X + 1, Y);
            case Direction.Down:
                return map.IsPlayerNearObstacle(X, Y + 1);
            case Direction.Left:
                return map.IsPlayerNearObstacle(X - 1, Y);
            default:
                return false;
        }
    }

    public bool StartMovingUntilEdge()
    {
        InitializePlayerPosition();
        while (!map.IsPlayerAtRisk(X, Y, CurrentDirection))
        {
            if (CheckIfNextStepIsObstacle())
            {
                TurnRight();
            }

            Move();
        }

        return true;
    }

    private void TurnRight()
    {
        CurrentDirection = CurrentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private void Move()
    {
        var oldPosition = new Position { X = X, Y = Y };
        switch (CurrentDirection)
        {
            case Direction.Up:
                Y--;
                break;
            case Direction.Right:
                X++;
                break;
            case Direction.Down:
                Y++;
                break;
            case Direction.Left:
                X--;
                break;
        }
        map.UpdatePlayerPosition(oldPosition, new Position { X = X, Y = Y });
        ExploredPositions.Add(new Position { X = X, Y = Y });
        StepsCounter++;
    }
}
