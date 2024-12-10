// See https://aka.ms/new-console-template for more information
public class Topology
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }

    public List<Topology> GetValidNeighbors()
    {
        var map = TopologyMap.Map; // You'll need to make 'map' accessible
        return map.GetNeighbors(X, Y)
            .Where(n => n.Height == Height + 1)
            .ToList();
    }
}