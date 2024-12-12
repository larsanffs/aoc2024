
using Dumpify;

var input = File.ReadAllLines("exampledata.txt");

var garden = new Garden(input);
var plantGroups = garden.Plots.Cast<Plot>().GroupBy(p => p.Plant);

foreach (var group in plantGroups)
{
    // lets find the plants that are close to each other forming a group

    Console.WriteLine($"Plant: {group.Key}, Count: {group.Count()}");
}


internal class Garden
{
    private Plot[,] plots;

    public Plot[,] Plots => plots;

    public Garden(string[] input)
    {
        plots = new Plot[input.Length, input[0].Length];

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                plots[i, j] = new Plot(i, j, input[i][j]);
            }
        }
    }
}

record Plot(int X, int Y, char Plant);
