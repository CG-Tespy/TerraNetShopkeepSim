using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CollectionSO<T> : ScriptableObject
{
    [SerializeField] private T[] startingContents = { };
    [SerializeField] private List<T> contents = new List<T>();

    public virtual IList<T> Contents
    {
        get { return contents; }
    }

    public virtual string Name { get { return name; } }

    protected virtual void OnEnable()
    {
        Contents.Clear();
        AddNonNullStartingContents();
    }

    protected virtual void AddNonNullStartingContents()
    {
        Contents.AddRange(startingContents);
        contents.RemoveAll((item) => item == null);
    }

    public virtual void Add(T item)
    {
        Contents.Add(item);
    }

    public virtual void AddRange(IList<T> contents)
    {
        for (int i = 0; i < contents.Count; i++)
        {
            T item = contents[i];
            Add(item);
        }
    }


}
