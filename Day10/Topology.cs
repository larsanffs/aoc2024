// See https://aka.ms/new-console-template for more information
public class Topology
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public TopologyMap Map { get; init; }

    public List<Topology> GetValidNeighbors()
    {
        return Map.GetNeighbors(X, Y)
            .Where(n => n.Height == Height + 1)
            .ToList();
    }
}