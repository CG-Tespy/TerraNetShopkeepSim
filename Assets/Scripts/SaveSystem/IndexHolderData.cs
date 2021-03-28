using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGTUnity.Fungus.SaveSystem;

/// <summary>
/// Save data meant to hold indexes
/// </summary>
public class IndexHolderData : SaveData
{
    
}

public interface ICollectionSOData: ISaveData
{
    IList<int> ContentIndexes { get; set; }
    string CollectionName { get; set; }
    /// <summary>
    /// Index showing where the collection is in some particular database
    /// </summary>
    int CollectionIndex { get; set; }
}

public interface IIndexSaveData
{
    int Index { get; }
    string Name { get; }
}

/// <summary>
/// Save data that mainly holds index data
/// </summary>
public class IndexSaveData : SaveData, IIndexSaveData
{
    [SerializeField] protected int index = -1;
    [SerializeField] protected string name = "";

    public virtual int Index 
    { 
        get { return index; } 
        set { index = value; }
    }

    public virtual string Name 
    {
        get { return name; } 
        set { name = value; }
    }
}