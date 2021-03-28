using System.Collections.Generic;
using IComparable = System.IComparable;

public static class IListExtensions 
{
    public static void AddRange<T>(this IList<T> list, IList<T> toAdd)
    {
        for (int i = 0; i < toAdd.Count; i++)
        {
            list.Add(toAdd[i]);
        }
    }

    public static void RemoveRange<T>(this IList<T> list, IList<T> toRemove)
    {
        for (int i = 0; i < toRemove.Count; i++)
        {
            list.Remove(toRemove[i]);
        }
    }

    public static void RemoveNulls<T>(this IList<T> list) where T: class
    {
        for (int i = 0; i < list.Count; i++)
        {
            var element = list[i];
            if (element == null)
            {
                list.RemoveAt(i);
                i--;
            }
        }
    }

    public static int Max(this IList<int> list) 
    {
        if (list.Count == 0)
            throw new System.InvalidOperationException("Numeric list doesn't have any elements for Max func to work with");

        int highest = list[0];

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] > highest)
                highest = list[i];
        }

        return highest;
    }

    public static bool ContainsAny<T>(this IList<T> list, IList<T> toCheckFor)
    {
        foreach (var element in toCheckFor)
        {
            if (list.Contains(element))
                return true;
        }

        return false;
    }
}
