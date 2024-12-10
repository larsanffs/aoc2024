var input = System.IO.File.ReadAllLines("input.txt");

var map = TopologyMap.ParseMap(input);

List<Trailhead> trailheads = map.Topologies.Cast<Topology>()
                                           .Where(t => t.Height == 0)
                                           .Select(t => new Trailhead(t))
                                           .ToList();

trailheads.ForEach(t => t.FindRoutes());

trailheads.ForEach(t => Console.WriteLine($"Trailhead at {t.Topology.X}, {t.Topology.Y} has a score of {t.Score}"));

Console.WriteLine($"The sum of all trail scores is {trailheads.Sum(t => t.Score)}");
