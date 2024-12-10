// See https://aka.ms/new-console-template for more information
public class Trailhead
{
    public Topology Topology { get; set; }

    public int Score { get; set; }
    private HashSet<Topology> VisitedPeaks { get; set; } = new();
    public Trailhead(Topology topology)
    {
        Topology = topology;
    }

    public void FindRoutes()
    {
        var visited = new HashSet<Topology> { Topology };
        FindRoutesToPeaks(Topology, visited);
        Score = VisitedPeaks.Count;
    }

    private void FindRoutesToPeaks(Topology current, HashSet<Topology> visited)
    {
        if (current.Height == 9)
        {
            VisitedPeaks.Add(current);
            return;
        }

        foreach (var next in current.GetValidNeighbors())
        {
            if (!visited.Contains(next))
            {
                visited.Add(next);
                FindRoutesToPeaks(next, visited);
                visited.Remove(next);
            }
        }
    }
}
