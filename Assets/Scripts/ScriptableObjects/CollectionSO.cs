﻿using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class CollectionSO<T> : ScriptableObject
{
    public event Action<T> ItemAdded = delegate { };
    public event Action<T> ItemRemoved = delegate { };

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

<<<<<<< HEAD
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

    public virtual void RemoveAll()
    {
        while (Contents.Count > 0)
            Remove(0);
    }

=======
>>>>>>> parent of ae529f8... More progress on crafting system.

}
