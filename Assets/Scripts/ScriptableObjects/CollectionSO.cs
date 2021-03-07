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
        bool inEditorOnly = !Application.isPlaying;
        bool shouldReset = inEditorOnly && resetOnEditorEnable;
        if (shouldReset)
        {
            Contents.Clear();
            AddNonNullStartingContents();
        }
    }

    protected virtual void AddNonNullStartingContents()
    {
        Contents.AddRange(startingContents);
        contents.RemoveAll((item) => item == null);
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

    public virtual void Remove(T item)
    {
        Contents.Remove(item);
        ItemRemoved.Invoke(item);
    }

    public virtual void Remove(int index)
    {
        var toRemove = Contents[index];
        Remove(toRemove);
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

    public virtual int Count()
    {
        return Contents.Count;
    }

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
