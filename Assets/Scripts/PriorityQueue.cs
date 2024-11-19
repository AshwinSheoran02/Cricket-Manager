using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<T>
{
    private SortedDictionary<int, Queue<T>> dict = new SortedDictionary<int, Queue<T>>();

    public void Enqueue(T item, int priority)
    {
        if (!dict.ContainsKey(priority))
        {
            dict[priority] = new Queue<T>();
        }
        dict[priority].Enqueue(item);
    }

    public T Dequeue()
    {
        if (dict.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        var firstKey = dict.Keys.Min();
        var queue = dict[firstKey];
        var item = queue.Dequeue();

        if (queue.Count == 0)
        {
            dict.Remove(firstKey);
        }

        return item;
    }

    public bool IsEmpty()
    {
        return dict.Count == 0;
    }
}

