using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CollectionSO<T> : ScriptableObject
{
    [SerializeField] private T[] startingContents = { };
    [SerializeField] private List<T> contents = new List<T>();
    public string Name { get { return name; } }

    public IList<T> Contents
    {
        get { return contents; }
    }

    protected virtual void OnEnable()
    {
#if UNITY_EDITOR
        
        Contents.Clear();
        Contents.AddRange(ValidStartingContents());
        
        
#endif
    }

    protected virtual IList<T> ValidStartingContents()
    {
        IList<T> valids = new List<T>();

        for (int i = 0; i < startingContents.Length; i++)
        {
            T item = startingContents[i];
            if (item != null)
                valids.Add(item);
        }

        return valids;
    }


}
