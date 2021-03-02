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