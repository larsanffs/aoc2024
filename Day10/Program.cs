// See https://aka.ms/new-console-template for more information
using Dumpify;

var input = System.IO.File.ReadAllLines("input.txt");

TopologyMap.Map = TopologyMap.ParseMap(input);

List<Trailhead> trailheads = TopologyMap.Map.Topologies.Cast<Topology>()
                                                       .Where(t => t.Height == 0)
                                                       .Select(t => new Trailhead(t))
                                                       .ToList();

trailheads.ForEach(t => t.FindRoutes());

trailheads.ForEach(t => Console.WriteLine($"Trailhead at {t.Topology.X}, {t.Topology.Y} has a score of {t.Score}"));

Console.WriteLine($"The sum of all scores is {trailheads.Sum(t => t.Score)}");
