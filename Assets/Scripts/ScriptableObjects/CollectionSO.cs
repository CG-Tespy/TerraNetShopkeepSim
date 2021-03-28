using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class CollectionSO<T> : ScriptableObject
{
    public event Action<T> ItemAdded = delegate { };
    public event Action<T> ItemRemoved = delegate { };

    [SerializeField] private T[] startingContents = { };
    [SerializeField] private List<T> contents = new List<T>();
    [Tooltip("Whether this should reset itself to its Starting Contents when this object's OnEnable is called in the Editor")]
    [SerializeField] private bool resetOnEditorEnable = true;

    public virtual IList<T> Contents
    {
        get { return contents; }
    }

    public virtual string Name { get { return name; } }

    protected virtual void OnEnable()
    {
#if UNITY_EDITOR
        bool inEditorOnly = !Application.isPlaying;
        bool shouldReset = inEditorOnly && resetOnEditorEnable;
        if (shouldReset)
        {
            ResetToStartingContents();
        }
#endif
    }

    public virtual void Add(T item)
    {
        Contents.Add(item);
        ItemAdded.Invoke(item);
    }

    public virtual void AddRange(IList<T> contents)
    {
        for (int i = 0; i < contents.Count; i++)
        {
            T item = contents[i];
            Add(item);
        }
    }

    public virtual bool Remove(T item)
    {
        bool removedSucessfully = Contents.Remove(item);
        if (removedSucessfully)
            ItemRemoved.Invoke(item);
        return removedSucessfully;
    }

    public virtual bool RemoveAt(int index)
    {
        var toRemove = Contents[index];
        bool removedSuccessfully = Remove(toRemove);
        return removedSuccessfully;
    }

    /// <summary>
    /// Removes everything in the passed list from this collection
    /// </summary>
    public virtual void RemoveRange(IList<T> toRemove)
    {
        foreach (var item in toRemove)
        {
            Remove(item);
        }
    }

    /// <summary>
    /// Does the same thing as Clear()
    /// </summary>
    public virtual void RemoveAll()
    {
        Contents.Clear();
    }

    public virtual void Clear()
    {
        Contents.Clear();
    }

    public virtual int IndexOf(T item)
    {
        return Contents.IndexOf(item);
    }

    public virtual bool Contains(T item)
    {
        return Contents.Contains(item);
    }

    public virtual bool ContainsRange(IList<T> items)
    {
        foreach (var itemEl in items)
        {
            if (!this.Contains(itemEl))
                return false;
        }

        return true;
    }


    public virtual int Count()
    {
        return Contents.Count;
    }

    public virtual void ResetToStartingContents()
    {
        Contents.Clear();
        Contents.AddRange(startingContents);
        contents.RemoveAll(nullItems);
    }

    protected static Predicate<T> nullItems = (item) => item == null;

}

public static class CollectionSOUtil
{
    /// <summary>
    /// Lets you fetch all elements of a type related to that which this CollectionSO
    /// was made to hold.
    /// </summary>
    public static IList<TSub> GetAllOfSubtype<TBase, TSub>(IList<TBase> collItems) where TSub: TBase
    {
        IList<TSub> result = new List<TSub>();

        foreach (var elem in collItems)
            if (elem is TSub)
                result.Add((TSub) elem);

        return result;
    }
}
