// See https://aka.ms/new-console-template for more information
public class TopologyMap
{
    public Topology[,] Topologies { get; set; }
    public int Width => Topologies.GetLength(1);
    public int Height => Topologies.GetLength(0);
    public TopologyMap(Topology[,] topologies)
    {
        Topologies = topologies;
    }

    public static TopologyMap Map { get; set; }

    public static TopologyMap ParseMap(string[] lines)
    {
        var height = lines.Length;
        var width = lines[0].Length;
        var topologies = new Topology[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                topologies[y, x] = new Topology
                {
                    X = x,
                    Y = y,
                    Height = int.Parse(lines[y][x].ToString())
                };
            }
        }

        return new TopologyMap(topologies);
    }

    public Topology GetTopology(int x, int y)
    {
        if (x < 0 || x >= Height || y < 0 || y >= Width)
        {
            return null;
        }
        return Topologies[x, y];
    }

    public List<Topology> GetNeighbors(int x, int y)
    {
        var neighbors = new List<Topology>();
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];
            var topology = GetTopology(newY, newX);
            if (topology != null)
            {
                neighbors.Add(topology);
            }
        }
        return neighbors;
    }
}
