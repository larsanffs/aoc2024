var input = File.ReadAllLines("input.txt");

Part1(input);
Part2(input);

static void Part2(string[] input)
{
    var grid = input.Select(x => x.ToCharArray()).ToArray();
    int rows = grid.Length;
    int cols = grid[0].Length;
    int counter = 0;

    // i need to iterate over the grid but i can skip first and last row and also first and last column
    for (int row = 1; row < rows - 1; row++)
    {
        for (int col = 1; col < cols - 1; col++)
        {
            if (grid[row][col] == 'A')
            {
                var center = grid[row][col];
                var nw = grid[row - 1][col - 1];
                var ne = grid[row - 1][col + 1];
                var sw = grid[row + 1][col - 1];
                var se = grid[row + 1][col + 1];

                string nwToSe = $"{nw}{center}{se}";
                string neToSw = $"{ne}{center}{sw}";

                if ((nwToSe == "SAM" || nwToSe == "MAS") && (neToSw == "SAM" || neToSw == "MAS"))
                {
                    counter++;
                }
            }
        }
    }

    Console.WriteLine($"X-MAS count is: {counter}");
}

static void Part1(string[] input)
{

    var grid = input.Select(x => x.ToCharArray()).ToArray();

    int rows = grid.Length;
    int cols = grid[0].Length;
    int wordCount = 0;

    int[][] directions = new int[][]
    {
    new int[] {-1, 0}, // up
    new int[] {1, 0},  // down
    new int[] {0, -1}, // left
    new int[] {0, 1},  // right
    new int[] {-1, -1}, // up-left
    new int[] {-1, 1},  // up-right
    new int[] {1, -1},  // down-left
    new int[] {1, 1}    // down-right
    };

    List<string> GetCharactersInDirection(int row, int col, int[] direction)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            int newRow = row + i * direction[0];
            int newCol = col + i * direction[1];
            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
            {
                result.Add(grid[newRow][newCol].ToString());
            }
            else
            {
                break;
            }
        }
        return result;
    }

    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            if (grid[row][col] != 'S' && grid[row][col] != 'X')
            {
                continue;
            }

            foreach (var direction in directions)
            {
                var characters = GetCharactersInDirection(row, col, direction);
                if (characters.Count == 4)
                {
                    // Process the characters array as needed
                    // For example, you can check if it matches "XMAS" or "SAMX"
                    string sequence = string.Join("", characters);
                    // if (sequence == "XMAS" || sequence == "SAMX")
                    if (sequence == "XMAS")
                    {
                        wordCount++;
                    }
                }
            }
        }
    }

    Console.WriteLine($"XMAS/SAMX count: {wordCount}");
}
