using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public static class GenericCollectionExtensions 
{
    /// <summary>
    /// Returns an array with the collection's contents.
    /// </summary>
    public static T[] GetAll<T>(this GenericCollection<T> coll)
    {
        T[] contents = new T[coll.Count];

        for (int i = 0; i < coll.Count; i++)
        {
            contents[i] = coll.GetSafe(i);
        }

        return contents;
    }
}
