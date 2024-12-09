using System.Text;
using Dumpify;



using var fileStream = new System.IO.StreamReader("input.txt");
var input = fileStream.ReadToEnd();


Console.WriteLine("Number of blocks in input: " + input.Length);

var diskMap = new DiskMap();

var fileId = 0;
for (int i = 0; i < input.Length; i++)
{
    // If i is even, it is a file block. If i is odd, it is a free space block.
    if (i % 2 == 0)
    {
        var file = int.Parse(input[i].ToString());
        for (int j = 0; j < file; j++)
        {
            var block = new File { Index = i + j, End = i + file, Length = file, FileId = fileId };
            diskMap.Blocks.AddLast(block);
        }
        fileId++;
    }
    else
    {
        var freeSpace = int.Parse(input[i].ToString());
        for (int j = 0; j < freeSpace; j++)
        {
            var block = new FreeSpace { Index = i + j, End = i + freeSpace, Length = freeSpace };
            diskMap.Blocks.AddLast(block);
        }
    }
}


diskMap.MoveFileBlocksFromRightToLeft();

Console.WriteLine($"Checksum is: {diskMap.CalculateCheckSum()}");


public abstract class Block
{
    public int Index { get; set; }
    public int End { get; set; }
    public int Length { get; set; }
}

public class File : Block
{
    public int FileId { get; set; } = 0;  // Add FileId to identify files
}

public class FreeSpace : Block
{
}

public class DiskMap
{
    public LinkedList<Block> Blocks { get; } = new();

    public bool MoveFileBlocksFromRightToLeft()
    {
        bool changesMade = false;
        var node = Blocks.Last;
        while (node != null)
        {
            var block = node.Value;
            if (block is File file)
            {
                var nextFreeSpaceNode = GetLeftMostFreeSpaceNode();
                if (nextFreeSpaceNode != null)
                {
                    // Swap places with nextFreeSpaceNode and node
                    Blocks.SwapNodeValue(node, nextFreeSpaceNode);

                    changesMade = true;
                }
            }
            node = node.Previous;
        }
        Visualize().Dump();
        return changesMade;
    }

    private LinkedListNode<Block>? GetLeftMostFreeSpaceNode()
    {
        var node = Blocks.First;
        while (node != null)
        {
            if (node.Value is FreeSpace)
            {
                return node;
            }
            node = node.Next;
        }
        return null;
    }

    public string Visualize()
    {
        var result = new StringBuilder();
        foreach (var block in Blocks)
        {
            if (block is File file)
            {
                result.Append(file.FileId.ToString());
            }
            else if (block is FreeSpace)
            {
                result.Append(".");
            }
        }
        return result.ToString();
    }

    public long CalculateCheckSum()
    {
        var checkSum = 0L;
        var fileBlocks = Blocks.Where(block => block is File).ToList();
        for (int i = 0; i < fileBlocks.Count; i++)
        {
            var fileBlock = (File)fileBlocks[i];
            checkSum += i * fileBlock.FileId;
        }
        return checkSum;
    }
}