using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionSO<T> : ScriptableObject
{
    [SerializeField] private T[] startingContents = { };
    [SerializeField] private List<T> contents = new List<T>();

    public IList<T> Contents
    {
        get { return contents; }
    }

    public string Name { get { return name; } }

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


}
