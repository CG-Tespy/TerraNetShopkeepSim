using System.Collections.Generic;

public static class ISetExtensions 
{
    public static void AddRange<T>(this ISet<T> set, ICollection<T> collection)
    {
        foreach (var item in collection)
        {
            set.Add(item);
        }
    }
    
}
