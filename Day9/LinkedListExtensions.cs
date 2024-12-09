using System;
using System.Linq.Expressions;


public static class LinkedListExtensions
{
    public static void SwapNodeValue(this LinkedList<Block> blocks, LinkedListNode<Block> node1, LinkedListNode<Block> node2)
    {
        if (node1.List != blocks || node2.List != blocks)
        {
            throw new InvalidOperationException("Both nodes must be in the same list");
        }

        var temp = node1.Value;
        node1.Value = node2.Value;
        node2.Value = temp;

    }
}

public static class LinkedListNodeExtensions
{
    public static void SwapValue<T>(this LinkedListNode<T> node1, LinkedListNode<T> node2)
    {
        if (node1.List != node2.List)
        {
            throw new InvalidOperationException("Both nodes must be in the same list");
        }

        var temp = node1.Value;
        node1.Value = node2.Value;
        node2.Value = temp;
    }
}
