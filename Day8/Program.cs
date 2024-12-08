var input = File.ReadAllLines("input.txt");
// store its character in a 2d array
var charArray = new char[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        charArray[i, j] = input[i][j];
    }
}

Map map = new Map(charArray);
map.GenerateAntiNodes();

var antiNodes = map.GetUniqueAntiNodes();
Console.WriteLine("Number of anti nodes: " + antiNodes.Count);

public class Map
{
    public INode<NodeType>[,] NodeArray { get; private set; }
    public Map(char[,] charArray)
    {
        NodeArray = new INode<NodeType>[charArray.GetLength(0), charArray.GetLength(1)];
        for (int i = 0; i < charArray.GetLength(0); i++)
        {
            for (int j = 0; j < charArray.GetLength(1); j++)
            {
                if (charArray[i, j] == '.')
                {
                    NodeArray[i, j] = new EmptyNode();
                }
                else
                {
                    NodeArray[i, j] = new Antenna(charArray[i, j]);
                }
                NodeArray[i, j].X = i;
                NodeArray[i, j].Y = j;
            }
        }
    }

    public void GenerateAntiNodes()
    {
        var antennaDictionary = NodeArray.Cast<INode<NodeType>>()
            .Where(node => node is Antenna)
            .GroupBy(node => (node as Antenna).Frequency)
            .ToDictionary(group => group.Key, group => group.Cast<Antenna>().ToList());

        foreach (var frequencyGroup in antennaDictionary)
        {
            var antennas = frequencyGroup.Value;
            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var antenna1 = antennas[i];
                    var antenna2 = antennas[j];

                    // Calculate distance and direction
                    int dx = antenna2.X - antenna1.X;
                    int dy = antenna2.Y - antenna1.Y;

                    // Calculate unit vector for direction
                    int distance = Math.Max(Math.Abs(dx), Math.Abs(dy));
                    double unitX = dx / (double)distance;
                    double unitY = dy / (double)distance;

                    // Place AntiNode after antenna2 at same distance
                    int antiNodeX = antenna2.X + (int)Math.Round(unitX * distance);
                    int antiNodeY = antenna2.Y + (int)Math.Round(unitY * distance);

                    if (!IsPositionOutOfBounds(antiNodeX, antiNodeY))
                    {
                        ChangeToAntiNode(antiNodeX, antiNodeY);
                    }

                    // Place AntiNode before antenna1 at same distance
                    antiNodeX = antenna1.X - (int)Math.Round(unitX * distance);
                    antiNodeY = antenna1.Y - (int)Math.Round(unitY * distance);

                    if (!IsPositionOutOfBounds(antiNodeX, antiNodeY))
                    {
                        ChangeToAntiNode(antiNodeX, antiNodeY);
                    }
                }
            }
        }
    }

    public void ChangeToAntiNode(int x, int y)
    {
        if (IsPositionOutOfBounds(x, y))
        {
            return;
        }
        if (NodeArray[x, y] is Antenna antenna)
        {
            antenna.HasAntiNode = true;
        }
        if (NodeArray[x, y] is EmptyNode)
        {
            NodeArray[x, y] = new AntiNode { X = x, Y = y };
        }
    }

    public bool IsPositionOutOfBounds(int x, int y)
    {
        return x < 0 || x >= NodeArray.GetLength(0) || y < 0 || y >= NodeArray.GetLength(1);
    }

    public List<AntiNode> GetUniqueAntiNodes()
    {
        List<AntiNode> antiNodes = new List<AntiNode>();
        for (int i = 0; i < NodeArray.GetLength(0); i++)
        {
            for (int j = 0; j < NodeArray.GetLength(1); j++)
            {
                if (NodeArray[i, j] is AntiNode antiNode)
                {
                    antiNodes.Add(antiNode);
                }
                if (NodeArray[i, j] is Antenna antenna && antenna.HasAntiNode)
                {
                    antiNodes.Add(new AntiNode { X = i, Y = j });
                }
            }
        }
        return antiNodes;
    }
}

public enum NodeType
{
    Empty,
    Anti,
    Antenna
}

public interface INode<T> where T : Enum
{
    int X { get; set; }
    int Y { get; set; }
    T Type { get; }
}

public abstract class Node<T> : INode<T> where T : Enum
{
    public int X { get; set; }
    public int Y { get; set; }
    public abstract T Type { get; }
}

public class AntiNode : Node<NodeType>
{
    public override NodeType Type => NodeType.Anti;
}

public class EmptyNode : Node<NodeType>
{
    public override NodeType Type => NodeType.Empty;
}

public class Antenna : Node<NodeType>
{
    public bool HasAntiNode { get; set; } = false;
    public char Frequency { get; set; }

    public Antenna(char frequency)
    {
        Frequency = frequency;
    }

    public override NodeType Type => NodeType.Antenna;
}