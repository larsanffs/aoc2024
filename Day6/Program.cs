var input = File.ReadAllLines("input.txt");

var map = new Map(input);
var player = new Player(map);
player.StartMovingUntilEdge();

Console.WriteLine("Player step counter is: " + player.StepsCounter);
// print visited positions
Console.WriteLine("Visited positions: " + player.ExploredPositions.Count);
