using System.Collections.Generic;

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
}
